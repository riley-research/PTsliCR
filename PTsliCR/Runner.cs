using iText.StyledXmlParser.Jsoup.Safety;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace PTsliCR
{
    internal class Runner
    {
        // Store all parameters
        public string[] SelectedFiles { get; set; }
        public string OutputFolder { get; set; }
        public int MinCharge { get; set; }
        public int MaxCharge { get; set; }
        public double IsolationWidth { get; set; }
        public int PPMTolerance { get; set; }
        public double MinimumMass { get; set; }
        public double MaximumMass { get; set; }
        public double IntensityThreshold { get; set; }
        public double ExtraMzMin { get; set; }
        public double ExtraMzMax { get; set; }
        public bool PrecurChargeOnly { get; set; }

        public event Action<string> StatusChanged;

        // Run the actual task
        public async Task RunTask()
        {
            StatusChanged?.Invoke("Starting...");
            
            foreach (var file in SelectedFiles) {
                string fileName = Path.GetFileName(file);
                string fileNameNoExt = Path.GetFileNameWithoutExtension(file);
                // Check and create necessary folders for each file
                StatusChanged?.Invoke($"Checking folder for {fileName}");
                CheckFolders(OutputFolder, file);

                // Clean the parameters
                // Use Path.Combine to build paths reliably
                string txtExportPath = Path.Combine(OutputFolder, fileNameNoExt);
                string pdfPath = Path.Combine(OutputFolder, fileNameNoExt);
                int[] possibleCharges = Enumerable.Range(MinCharge, MaxCharge - MinCharge + 1).ToArray();
                txtExportPath = txtExportPath + @"\PTCR_cleaned_mgf.txt";
                pdfPath = pdfPath + @"\PTCR_cleaned_spectra.pdf";

                StatusChanged?.Invoke($"Extracting PTCR scans from {fileName}");
                // Fire up the engine
                PTsliCR ptcrCleaner = new PTsliCR
                {
                    rawFilePath = file,
                    txtExportPath = txtExportPath,
                    pdfPath = pdfPath,
                    possibleCharges = possibleCharges,
                    isolationWidth = IsolationWidth,
                    PPMTolerance = PPMTolerance,
                    PrecurChargeOnly = PrecurChargeOnly,
                    minimumMass = MinimumMass,
                    maximumMass = MaximumMass,
                    intensityThreshold = IntensityThreshold,
                    ExtraMzMax = ExtraMzMax,
                    ExtraMzMin = ExtraMzMin,
                    fileName = fileName,
                };

                ptcrCleaner.StatusChanged += (msg) => StatusChanged?.Invoke(msg);

                await Task.Run(() => ptcrCleaner.PTCR_Cleaner());

            }

            StatusChanged?.Invoke("Done!");
        }

        private void CheckFolders(string OutFolder, string thisFile) {
            // Build a proper path for the extraction folder using Path.Combine
            string fileName = Path.GetFileNameWithoutExtension(thisFile);
            string fullPath = Path.Combine(OutFolder, fileName);

            // If the provided OutFolder points to a file instead of a directory, bail out
            if (File.Exists(OutFolder))
            {
                StatusChanged?.Invoke("Output path points to a file, not a folder. Please select a folder.");
                return;
            }

            int length = fullPath.Length;
            if (length > 260)
            {
                StatusChanged?.Invoke("Path length too long, please move to a closer folder.");
                System.Threading.Thread.Sleep(20000);
                return;
            }

            // Directory.CreateDirectory will create all necessary parent directories
            try
            {
                Directory.CreateDirectory(fullPath);
                StatusChanged?.Invoke($"Created or verified folder: {fullPath}");
            }
            catch (Exception ex)
            {
                StatusChanged?.Invoke($"Failed to create folder {fullPath}: {ex.Message}");
            }
        }
     }
}
