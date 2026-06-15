using CSMSL;
using CSMSL.IO.Thermo;
using CSMSL.Spectral;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System;
using CSMSL.Chemistry;
using ScottPlot;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Image = iTextSharp.text.Image;
using Element = iTextSharp.text.Element;
using static iText.Svg.SvgConstants;
using System.Diagnostics;


namespace PTCRCleaner_GUI
{
    internal class PTCRCleaner
    {
        public string rawFilePath { get; set; }
        public string txtExportPath { get; set; }
        public string pdfPath { get; set; }
        public int[] possibleCharges { get; set; }
        public double isolationWidth { get; set; }
        public int PPMTolerance { get; set; }
        public bool PrecurChargeOnly { get; set; }
        public double minimumMass { get; set; }
        public double maximumMass { get; set; }
        public double intensityThreshold { get; set; }
        public double ExtraMzMax { get; set; }
        public double ExtraMzMin { get; set; }
        public string fileName { get; set; }

        public event Action<string> StatusChanged;

        public void PTCR_Cleaner()
        {
            var rawFile = new ThermoRawFile(rawFilePath);
            rawFile.Open();
            List<int[]> allChargeCombinations = GetAllCombinations(possibleCharges);

            foreach(var a in allChargeCombinations)
            {
                Debug.WriteLine("Charge combination: " + a[0] + " to " + a[1]);
            }

            DIAPTCRFunctionCaller(rawFile, txtExportPath, allChargeCombinations, isolationWidth, PPMTolerance,
                    pdfPath, PrecurChargeOnly, minimumMass, maximumMass, intensityThreshold, ExtraMzMax, fileName, ExtraMzMin); // Method that does all the heavy lifting
        }
        public void DIAPTCRFunctionCaller(ThermoRawFile thisRaw, string txtExportPath, List<int[]> allChargeCombinations,
        double isolationWidth, int PPMTolerance, string pdfPath, bool PrecurChargeOnly,
        double minimumMass, double maximumMass, double intensityThreshold,
        double ExtraMzMax, string fileName, double ExtraMzMin)
        {
            /*Steps to take:
                * 1. Open a connection to a txt file
                * 2. Open a scan and raw data for MGF file
                * 3. Clean the intensities using PTCR approach
                *  3a. Calculate what m/z values are within the windows of interest
                *  3b. Change intensity to 0 of values outside windows of interest
                * 4. Plot the before and after intensities
                * 5. Export the scan to the text file (MS1 scans are not supported
                * 6. Continue to the next MS/MS scan
            */

            List<double> allMS1IntensityValues = new List<double>();
            List<double> allIntactMasses = new List<double>();
            List<double> allCorrectedIntensities = new List<double>();
            List<int> spectrumNumber = new List<int>();
            StreamWriter writer = new StreamWriter(txtExportPath);
            string basePath = Path.GetDirectoryName(txtExportPath);
            string writerDigestPath = Path.Combine(basePath, "PTCR_cleaned_for_extraction.txt");
            StreamWriter writerDigest = new StreamWriter(writerDigestPath);
            initializeWriterDigest(writerDigest);
            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(1200, 900);

            using (var document = new Document(pageSize))
            {
                if (pageSize == null)
                {
                    throw new NullReferenceException("pageSize is null.");
                }
                if (document == null)
                {
                    throw new NullReferenceException("document is null.");
                }
                FileStream str = new FileStream(pdfPath, FileMode.Create);
                PdfWriter.GetInstance(document, str); //If this throws an error: Debug -> Exceptions -> Remove the checkbox in the column "Thrown" of "Common Language Runtime Exceptions
                document.Open();

                for (int i = thisRaw.FirstSpectrumNumber; i <= thisRaw.LastSpectrumNumber; i++)
                {
                    string TITLE;
                    double RTINSECONDS;
                    double precursorMass;
                    string CHARGE;
                    double[] INTENSITIES;
                    double[] MASSES;
                    string PEPMASS;
                    List<int[]> allChargeCombinationsSet = new List<int[]>();
                    
                StatusChanged?.Invoke($"Extracting PTCR scans from {fileName}, now at scan: {i}");

                if (thisRaw.GetDissociationType(i).ToString() == "PTR")
                    {
                        ThermoSpectrum thisSpectrum = thisRaw.GetSpectrum(i, profileIfAvailable: true);

                        precursorMass = thisRaw.GetPrecursorMz(i);
                        CHARGE = thisRaw.GetPrecusorCharge(i).ToString() + "+";
                        INTENSITIES = thisSpectrum.GetIntensities();
                        MASSES = thisSpectrum.GetMasses();
                        TITLE = thisRaw.Name + " Spectrum" + i;
                        RTINSECONDS = thisRaw.GetRetentionTime(i) * 60;

                        allChargeCombinationsSet = allChargeCombinations
                            .Select(arr => (int[])arr.Clone())
                            .ToList();

                        //Console.WriteLine("Length Intensities = " + INTENSITIES.Length + ". Length masses = " +  MASSES.Length + ".");

                        //Console.WriteLine(TITLE + "---" + RTINSECONDS + "---" + PEPMASS + "---" + CHARGE);

                        //Writer(writer, TITLE, RTINSECONDS.ToString(), PEPMASS, CHARGE, MASSES, INTENSITIES);

                        double[] correctedINTENSITIES = GetCleanSpectra(precursorMass, isolationWidth, allChargeCombinationsSet, MASSES, INTENSITIES, ExtraMzMax, ExtraMzMin);

                        if (correctedINTENSITIES.Sum() < intensityThreshold)
                        {
                            continue;
                        }

                        allCorrectedIntensities.Add(correctedINTENSITIES.Sum());
                        spectrumNumber.Add(i);
                        int[] peakGroups = GetPeakGroup(correctedINTENSITIES);

                        Writer(writer, TITLE, RTINSECONDS.ToString(), PEPMASS = "DIAPTCR", CHARGE, MASSES, correctedINTENSITIES);
                        WriterDigest(writerDigest, MASSES, correctedINTENSITIES, i, peakGroups, keepZeros: 1);

                        PlotQCLinePlots(INTENSITIES, correctedINTENSITIES, MASSES, precursorMass, isolationWidth, allChargeCombinationsSet, TITLE, document, pdfPath);
                    }
                }
                document.Close();
            }

            writer.Close();
            writerDigest.Close();

            if(allCorrectedIntensities.Count > 1)
            {
                allCorrectedIntensitiesHistogrem(pdfPath, allCorrectedIntensities);
            }
            
            writeToCsv(pdfPath, fileName: @"_summed_PTCR_slice_intensities", spectrumNumber, allCorrectedIntensities);

        }

        static void initializeWriterDigest(StreamWriter writerDigest)
        {
            writerDigest.WriteLine("mz,intensity,id,peakgroup");
        }

        static void WriterDigest(
    StreamWriter writerDigest,
    double[] masses,
    double[] correctedINTENSITIES,
    int id,
    int[] peakgroup,
    int keepZeros)
        {
            string formattedID = $"Spectrum{id}";
            int n = correctedINTENSITIES.Length;

            int lastPeak = -1000000;

            for (int i = 0; i < n; i++)
            {
                if (correctedINTENSITIES[i] > 0)
                    lastPeak = i;

                bool keep = Math.Abs(i - lastPeak) <= keepZeros;

                if (!keep)
                {
                    // look ahead for future peak window
                    for (int j = i + 1; j <= Math.Min(n - 1, i + keepZeros); j++)
                    {
                        if (correctedINTENSITIES[j] > 0)
                        {
                            keep = true;
                            break;
                        }
                    }
                }

                if (!keep) continue;

                if (peakgroup[i] == 0)
                {
                    int nextVal = 0;

                    // look ahead within keepZeros window
                    for (int j = i + 1; j <= Math.Min(n - 1, i + keepZeros); j++)
                    {
                        if (peakgroup[j] > 0)
                        {
                            nextVal = peakgroup[j];
                            break;
                        }
                    }

                    if (nextVal > 0)
                    {
                        peakgroup[i] = nextVal; // assign next non-zero value
                    }
                    else if (i > 0)
                    {
                        peakgroup[i] = peakgroup[i - 1]; // fallback to previous
                    }
                    else
                    {
                        peakgroup[i] = 0; // first element edge case
                    }
                }

                writerDigest.WriteLine(
                    "{0},{1},{2},{3}",
                    masses[i],
                    correctedINTENSITIES[i],
                    formattedID,
                    peakgroup[i]);
            }
        }

        static void Writer(StreamWriter writer, string TITLE, string RTINSECONDS, string PEPMASS, string CHARGE, double[] MASSES, double[] INTENSITIES)
        {
            writer.WriteLine("BEGIN IONS");
            writer.WriteLine("TITLE=" + TITLE);
            writer.WriteLine("RTINSECONDS=" + RTINSECONDS);
            writer.WriteLine("PEPMASS=" + PEPMASS);
            writer.WriteLine("CHARGE=" + CHARGE);

            for (int i = 0; i < MASSES.Length; i++)
            {
                writer.WriteLine(MASSES[i] + " " + INTENSITIES[i]);
            }

            writer.WriteLine("END IONS");
        }

        static List<int[]> GetAllCombinations(int[] input)
        {
            int n = input.Length;
            List<int[]> result = new List<int[]>();

            /* This is for setting a lower and upper charge bound
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (input[i] > input[j])
                    {
                        List<int> subset = new List<int> { input[i], input[j] };
                        result.Add(subset.ToArray());
                    }
                }
            }
            */

            for (int i = 0; i < n; i++)
            {
                int[] chargesInRange = Enumerable.Range(1, input[i] - 1 + 1).ToArray();
                var numChargeInRange = chargesInRange.Length;

                for (int j = 0; j < numChargeInRange; j++)
                {

                    if (chargesInRange[j] == input[i])
                        continue;

                    List<int> subset = new List<int> { input[i], chargesInRange[j] };
                    result.Add(subset.ToArray());
                }
            }


                return result;
        }

        static double[] GetCleanSpectra(double thisPrecursorMZ, double isolationWidth, List<int[]> allChargeCombinations, 
            double[] MASSES, double[] INTENSITIES, double ExtraMzMax, double ExtraMzMin)
        {
            double protonMass = 1.007825032241;
            double lowerBound = thisPrecursorMZ - 0.5 * isolationWidth - ExtraMzMin;
            double upperBound = (thisPrecursorMZ + 0.5 * isolationWidth) + ExtraMzMax;
            double LBChargeReduced;
            double UBChargeReduced;
            List<(double Min, double Max)> acceptedRanges = new List<(double, double)>();
            List<int> indicesInRange = new List<int>();

            //Console.WriteLine("The precursor is: {0}, the lowerBound is: {1}, the upperBound is: {2}.", thisPrecursorMZ, lowerBound, upperBound);

            foreach (var chargeCombination in allChargeCombinations)
            {
                LBChargeReduced = ((lowerBound * chargeCombination[0] - chargeCombination[0] * protonMass) + (protonMass * chargeCombination[1])) / chargeCombination[1];
                UBChargeReduced = ((upperBound * chargeCombination[0] - chargeCombination[0] * protonMass) + (protonMass * chargeCombination[1])) / chargeCombination[1];

                acceptedRanges.Add((LBChargeReduced, UBChargeReduced));
                //Console.WriteLine("LB = {0}. UB = {1}. This was calculated for charge {2} to charge {3}.", LBChargeReduced, UBChargeReduced, chargeCombination[0], chargeCombination[1]);
            }

            indicesInRange = GetIndicesOfValuesInsideRanges(MASSES, acceptedRanges);

            var cleanINTENSITIES = SetSelectedValuesToZero(MASSES, indicesInRange, INTENSITIES);

            /*for (int i = 0; i < MASSES.Length; i++)
            {
                Console.WriteLine("Old mass: {0} to new mass: {1}.", MASSES[i], masses2[i]);
            }*/

            return cleanINTENSITIES;
        }

        static List<int> GetIndicesOfValuesInsideRanges(double[] values, List<(double Min, double Max)> acceptedRanges)
        {
            List<int> indices = new List<int>();

            for (int i = 0; i < values.Length; i++)
            {
                double value = values[i];

                bool isInsideRange = acceptedRanges.Any(range => value > range.Min & value < range.Max);

                if (isInsideRange)
                {
                    indices.Add(i);
                }
            }
            return indices;
        }

        static double[] SetSelectedValuesToZero(double[] masses, List<int> indices, double[] INTENSITIES)
        {
            double[] cleanedData = new double[masses.Length];

            foreach (int index in indices)
            {
                if (index >= 0 && index < masses.Length)
                {
                    cleanedData[index] = INTENSITIES[index];
                }
                else
                {
                    cleanedData[index] = 0.0;
                }
            }
            return cleanedData;
        }

        static void PlotQCLinePlots(double[] intensities, double[] correctedIntensities, double[] Masses, double thisPrecursorMZ, double isolationWidth, List<int[]> allChargeCombinations, string TITLE, Document document, string pdfPath)
        {
            //Console.WriteLine("Now exporting: {0}", TITLE);
            string tempFilePath = Path.ChangeExtension(pdfPath, ".PNG");
            List<(double Min, double Max)> ranges = GetWindowsOfInterest(thisPrecursorMZ, isolationWidth, allChargeCombinations);

            ScottPlot.Plot myPlot = new();

            foreach (var range in ranges)
            {
                //Console.WriteLine("The range is {0}, and the max is {1}.", range, Masses.Max());
                if (range.Min < Masses.Max())
                {
                    var hs = myPlot.Add.HorizontalSpan(range.Min, range.Max);
                    hs.FillStyle.Color = Colors.Magenta.WithAlpha(.1);
                }
            }

            var scat = myPlot.Add.Scatter(Masses, intensities);

            double[] mirroredYs = new double[correctedIntensities.Length];
            for (int i = 0; i < correctedIntensities.Length; i++)
                if (correctedIntensities[i] == 0)
                {
                    mirroredYs[i] = 0;
                }
                else
                {
                    mirroredYs[i] = -correctedIntensities[i];
                }

            var scat2 = myPlot.Add.Scatter(Masses, mirroredYs);

            scat.MarkerSize = 0;
            scat2.MarkerSize = 0;

            myPlot.Axes.Title.Label.Text = TITLE;
            myPlot.HideGrid();
            myPlot.XLabel("m/z");
            myPlot.YLabel("Intensity (a.u.)");

            myPlot.SavePng(tempFilePath, 1200, 800);

            var img = Image.GetInstance(tempFilePath);
            img.ScaleToFit(1200, 800);
            img.Alignment = Element.ALIGN_CENTER;

            document.Add(img);
            document.NewPage();

            File.Delete(tempFilePath);

            /*
            //Export the raw data for the plots
            try
            {
                // 1. Clean the TITLE to make it safe for a Windows filename
                string safeTitle = string.Join("_", TITLE.Split(Path.GetInvalidFileNameChars()));
                string baseDir = Path.GetDirectoryName(pdfPath);

                // 2. Export the Raw and Mirrored Peak Data
                string csvDataPath = Path.Combine(baseDir, $"{safeTitle}_peaks.csv");
                using (StreamWriter sw = new StreamWriter(csvDataPath))
                {
                    sw.WriteLine("MZ,Intensity,CorrectedIntensity,MirroredIntensity");
                    for (int i = 0; i < Masses.Length; i++)
                    {
                        // We export mirroredYs[i] as well so R doesn't have to recalculate the mirror logic
                        sw.WriteLine($"{Masses[i]},{intensities[i]},{correctedIntensities[i]},{mirroredYs[i]}");
                    }
                }

                // 3. Export the Rectangle (Horizontal Span) Ranges
                string csvRangePath = Path.Combine(baseDir, $"{safeTitle}_ranges.csv");
                using (StreamWriter sw = new StreamWriter(csvRangePath))
                {
                    sw.WriteLine("Min,Max");
                    foreach (var range in ranges)
                    {
                        if (range.Min < Masses.Max())
                        {
                            sw.WriteLine($"{range.Min},{range.Max}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Data export failed for {TITLE}: {ex.Message}");
            }
            */
        }

        static List<(double Min, double Max)> GetWindowsOfInterest(double thisPrecursorMZ, double isolationWidth, List<int[]> allChargeCombinations)
        {
            double protonMass = 1.007825032241;
            double lowerBound = thisPrecursorMZ - 0.5 * isolationWidth;
            double upperBound = thisPrecursorMZ + 0.5 * isolationWidth;
            double LBChargeReduced;
            double UBChargeReduced;
            List<(double Min, double Max)> acceptedRanges = new List<(double, double)>();

            //Console.WriteLine("The precursor is: {0}, the lowerBound is: {1}, the upperBound is: {2}.", thisPrecursorMZ, lowerBound, upperBound);

            foreach (var chargeCombination in allChargeCombinations)
            {
                LBChargeReduced = ((lowerBound * chargeCombination[0] - chargeCombination[0] * protonMass) + (protonMass * chargeCombination[1])) / chargeCombination[1];
                UBChargeReduced = ((upperBound * chargeCombination[0] - chargeCombination[0] * protonMass) + (protonMass * chargeCombination[1])) / chargeCombination[1];

                acceptedRanges.Add((LBChargeReduced, UBChargeReduced));
                //Console.WriteLine("LB = {0}. UB = {1}. This was calculated for charge {2} to charge {3}.", LBChargeReduced, UBChargeReduced, chargeCombination[0], chargeCombination[1]);
            }

            return acceptedRanges;
        }

        static void allCorrectedIntensitiesHistogrem(string pdfPath, List<double> allCorrectedIntensities)
        {
            string tempFilePath = Path.ChangeExtension(pdfPath, ".PNG");
            tempFilePath = tempFilePath.Replace(@".PNG", @"_summed_PTCR_slice_intensities.png");
            List<double> logValues = allCorrectedIntensities.Select(v => Math.Log10(v)).ToList();

            for (int iv = 0; iv < logValues.Count; iv++)
            {
                if (logValues[iv] < 0)
                {
                    logValues[iv] = 0.0;
                }
            }

            ScottPlot.Plot myPlot = new();

            var hist = ScottPlot.Statistics.Histogram.WithBinCount(50, logValues);

            var barPlot = myPlot.Add.Bars(hist.Bins, hist.Counts);

            foreach (var bar in barPlot.Bars)
            {
                bar.Size = hist.FirstBinSize * .8;
            }

            myPlot.YLabel("Number of MS/MS scans");
            myPlot.XLabel("Summed intensity (log10)");

            myPlot.SavePng(tempFilePath, 1200, 1000);
        }

        static void writeToCsv(string pdfPath, string fileName, List<int> specNr, List<double> exportList)
        {
            string tempFilePath = Path.ChangeExtension(pdfPath, ".txt");
            string id = fileName + ".txt";
            tempFilePath = tempFilePath.Replace(@".txt", id);
            StreamWriter exportTxt = new StreamWriter(tempFilePath);

            exportTxt.WriteLine("Spectrum number, Summed PTCR slice intensity");

            for (int i = 0; i < exportList.Count; i++)
            {
                string export = specNr[i] + "," + exportList[i];
                exportTxt.WriteLine(export.ToString());
            }

            exportTxt.Close();
        }

        public static int[] GetPeakGroup(double[] _correctedINTENSITIES)
        {
            int n = _correctedINTENSITIES.Length;
            int[] peakGroup = new int[n];

            int count = 0;
            double currentIntensity = 0.0;
            bool inPeak = false;
            bool decreasingPeak = false;

            for (int i = 0; i < n; i++)
            {
                double intensity = _correctedINTENSITIES[i];

                if (intensity == 0 && !inPeak)
                {
                    peakGroup[i] = 0;
                    currentIntensity = 0.1;
                    inPeak = false;
                }
                else if (intensity != 0 && !inPeak)
                {
                    count++;
                    inPeak = true;
                    decreasingPeak = false;
                    currentIntensity = intensity;
                    peakGroup[i] = count;
                }
                else if (intensity == 0 && inPeak)
                {
                    inPeak = false;
                    peakGroup[i] = 0;
                    currentIntensity = 0.1;
                    inPeak = false;
                }
                else if (intensity != 0 && inPeak)
                {
                    if (intensity / currentIntensity > 1.05 && decreasingPeak)
                    {
                        decreasingPeak = false;
                        count++;
                        inPeak = true;
                        currentIntensity = intensity;
                        peakGroup[i] = count;
                    }
                    else if (intensity / currentIntensity < 1.05 && !decreasingPeak)
                    {
                        peakGroup[i] = count;
                        decreasingPeak = true;
                        currentIntensity = intensity;
                    }
                    else
                    {
                        peakGroup[i] = count;
                        currentIntensity = intensity;
                    }
                }
                else
                {
                    throw new InvalidOperationException("Unexpected intensity state at index " + i);
                }
            }

            return peakGroup;
        }

    }

    }
