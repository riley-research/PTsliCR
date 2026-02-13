using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PTCRCleaner_GUI
{
    public partial class Form1 : Form
    {

        public string[] selectedFiles;
        public string outputFolder;
        public int minCharge;
        public int maxCharge;
        public double isolationWidth;
        public int PPMTolerance;
        double minimumMass;
        double maximumMass;
        double intensityThreshold;
        double ExtraMzMin;
        double ExtraMzMax;
        bool PrecurChargeOnly;

        public Form1()
        {
            InitializeComponent();
            minCharge = (int)numericMinCharge.Value;
            maxCharge = (int)numericMaxCharge.Value;
            isolationWidth = (double)numericIsolWidth.Value;
            PPMTolerance = (int)numericPPMTol.Value;
            minimumMass = (double)numericMinMass.Value;
            maximumMass = (double)numericMaxMass.Value;
            intensityThreshold = (double)numericIntThreshold.Value;
            ExtraMzMin = (double)numericMzMargMin.Value;
            ExtraMzMax = (double)numericMzMargMax.Value;
            PrecurChargeOnly = checkBoxPrecZOnly.Checked;
        }

        private bool dataIsValid()
        {
            Debug.WriteLine(selectedFiles);
            if (selectedFiles == null || selectedFiles.Length == 0)
            {
                MessageBox.Show("Please select at least one .raw file.",
                    "Invalid Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(outputFolder))
            {
                MessageBox.Show("Please select an output folder.",
                    "Invalid Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            if (minCharge < 0 | minCharge > maxCharge)
            {
                MessageBox.Show("Minimum charge must be greater than 0 and smaller or equal to the maximum charge.",
                    "Invalid Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            if (isolationWidth <= 0)
            {
                MessageBox.Show("Isolationwidth with must be > 0",
                    "Invalid Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            if (PPMTolerance <= 0)
            {
                MessageBox.Show("Isolationd with must be > 0",
                    "Invalid Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            if (minimumMass < 0 | minimumMass >= maximumMass)
            {
                MessageBox.Show("Minimum mass must be greater than 0 and smaller than the maximum charge.",
                    "Invalid Input",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // The designer-configured OpenFileDialog is `openFileDialog_Input`.
            // Ensure the selected file has a .raw extension; if not, cancel the selection.
            var dlg = sender as OpenFileDialog ?? openFileDialog_Input;

            if (dlg != null)
            {
                selectedFiles = dlg.FileNames;

                foreach (var f in selectedFiles)
                {
                    if (!string.IsNullOrEmpty(f) && Path.GetExtension(f).Equals(".raw", StringComparison.OrdinalIgnoreCase))
                    {
                        //Debug.WriteLine($"Selected file: {f}");
                    }
                    else
                    {
                        MessageBox.Show("Please select a .raw file.", "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true; // prevent dialog from closing with an invalid selection
                    }
                }

                if (selectedFiles.Length == 1)
                {
                    inputBox.Text = selectedFiles[0];
                }
                else
                {
                    inputBox.Text = $"{selectedFiles.Length} files selected";
                }
            }
        }

        private void button_select_Click(object sender, EventArgs e)
        {
            DialogResult result = this.openFileDialog_Input.ShowDialog();

            if (result == DialogResult.OK)
            {
                inputBox.Text = inputBox.Text;
            }
            else
            {
                inputBox.Text = "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void inputBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void selectOutput_Click(object sender, EventArgs e)
        {
            DialogResult dlg = outputFolderBrowserDialog.ShowDialog();


            if (dlg == DialogResult.OK)
            {
                outputFolder = outputFolderBrowserDialog.SelectedPath;

                outputBox.Text = outputFolder;
            }

        }

        private void outputfolderBrowserDialog_HelpRequest(object sender, EventArgs e)
        {

        }

        private void outputBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void labelMinCharge_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            PrecurChargeOnly = checkBoxPrecZOnly.Checked;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            minCharge = (int)numericMinCharge.Value;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            maxCharge = (int)numericMaxCharge.Value;
        }

        private void numericUpDown1_ValueChanged_1(object sender, EventArgs e)
        {
            isolationWidth = (double)numericIsolWidth.Value;
        }

        private void numericPPMTol_ValueChanged(object sender, EventArgs e)
        {
            PPMTolerance = (int)numericPPMTol.Value;
        }

        private void labelMassRange_Click(object sender, EventArgs e)
        {

        }

        private void numericMinMass_ValueChanged(object sender, EventArgs e)
        {
            minimumMass = (double)numericMinMass.Value;
        }

        private void numericMaxMass_ValueChanged(object sender, EventArgs e)
        {
            maximumMass = (double)numericMaxMass.Value;
        }

        private void numericIntThreshold_ValueChanged(object sender, EventArgs e)
        {
            intensityThreshold = (double)numericIntThreshold.Value;
        }

        private void labelExtraMz_Click(object sender, EventArgs e)
        {

        }

        private void numericMzMargMin_ValueChanged(object sender, EventArgs e)
        {
            ExtraMzMin = (double)numericMzMargMin.Value;
        }

        private void numericMzMargMax_ValueChanged(object sender, EventArgs e)
        {
            ExtraMzMax = (double)numericMzMargMax.Value;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Debug.WriteLine($"This is it {PrecurChargeOnly}");
            if (dataIsValid())
            {
                buttonStart.Enabled = false;
                buttonStart.Text = "Running";

                Runner runner = new Runner
                {
                    SelectedFiles = selectedFiles,
                    OutputFolder = outputFolder,
                    MinCharge = minCharge,
                    MaxCharge = maxCharge,
                    IsolationWidth = isolationWidth,
                    PPMTolerance = PPMTolerance,
                    MinimumMass = minimumMass,
                    MaximumMass = maximumMass,
                    IntensityThreshold = intensityThreshold,
                    ExtraMzMin = ExtraMzMin,
                    ExtraMzMax = ExtraMzMax,
                    PrecurChargeOnly = PrecurChargeOnly
                };

                runner.StatusChanged += Runner_StatusChanged;
                runner.RunTask();

                buttonStart.Enabled = true;
                buttonStart.Text = "Start";

            }
        }

        private void Runner_StatusChanged(string msg)
        {
            if (labelStatus.InvokeRequired)
            {
                // We are on a background thread, marshal to UI thread
                labelStatus.Invoke(new Action(() => labelStatus.Text = msg));
            }
            else
            {
                // We are on the UI thread already
                labelStatus.Text = msg;
            }
        }


        private void labelStatus_Click(object sender, EventArgs e)
        {

        }
    }
}
