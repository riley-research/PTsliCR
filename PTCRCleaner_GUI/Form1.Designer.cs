namespace PTCRCleaner_GUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            label_input = new Label();
            inputBox = new TextBox();
            button_select = new Button();
            openFileDialog_Input = new OpenFileDialog();
            label1 = new Label();
            selectOutput = new Button();
            outputBox = new TextBox();
            outputFolderBrowserDialog = new FolderBrowserDialog();
            labelChargeRange = new Label();
            labelDash = new Label();
            labelIsolationWidth = new Label();
            labelPPMTol = new Label();
            labelMassRange = new Label();
            labelIntThreshold = new Label();
            labelExtraMz = new Label();
            checkBoxPrecZOnly = new CheckBox();
            numericMinCharge = new NumericUpDown();
            numericMaxCharge = new NumericUpDown();
            buttonStart = new Button();
            numericIsolWidth = new NumericUpDown();
            numericPPMTol = new NumericUpDown();
            numericMinMass = new NumericUpDown();
            numericMaxMass = new NumericUpDown();
            labelDash2 = new Label();
            numericIntThreshold = new NumericUpDown();
            numericMzMargMin = new NumericUpDown();
            numericMzMargMax = new NumericUpDown();
            labelDash3 = new Label();
            labelStatus = new Label();
            toolTipCharge = new ToolTip(components);
            toolTipIsolWidth = new ToolTip(components);
            toolTipPPMTol = new ToolTip(components);
            toolTipPrecZ = new ToolTip(components);
            toolTipMassRange = new ToolTip(components);
            toolTipIntThresh = new ToolTip(components);
            toolTipAddMZ = new ToolTip(components);
            ((System.ComponentModel.ISupportInitialize)numericMinCharge).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericMaxCharge).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericIsolWidth).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPPMTol).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericMinMass).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericMaxMass).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericIntThreshold).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericMzMargMin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericMzMargMax).BeginInit();
            SuspendLayout();
            // 
            // label_input
            // 
            label_input.AutoSize = true;
            label_input.Location = new Point(56, 45);
            label_input.Name = "label_input";
            label_input.Size = new Size(35, 15);
            label_input.TabIndex = 0;
            label_input.Text = "Input";
            label_input.Click += label1_Click;
            // 
            // inputBox
            // 
            inputBox.Location = new Point(97, 42);
            inputBox.Name = "inputBox";
            inputBox.Size = new Size(558, 23);
            inputBox.TabIndex = 1;
            inputBox.TextChanged += inputBox_TextChanged;
            // 
            // button_select
            // 
            button_select.Location = new Point(675, 42);
            button_select.Name = "button_select";
            button_select.Size = new Size(75, 23);
            button_select.TabIndex = 2;
            button_select.Text = "Select";
            button_select.UseVisualStyleBackColor = true;
            button_select.Click += button_select_Click;
            // 
            // openFileDialog_Input
            // 
            openFileDialog_Input.DefaultExt = "raw";
            openFileDialog_Input.FileName = "openFileDialog_Input";
            openFileDialog_Input.Filter = "RAW files (*.raw)|*.raw";
            openFileDialog_Input.Multiselect = true;
            openFileDialog_Input.FileOk += openFileDialog1_FileOk;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(46, 100);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 3;
            label1.Text = "Output";
            label1.Click += label1_Click_1;
            // 
            // selectOutput
            // 
            selectOutput.Location = new Point(675, 97);
            selectOutput.Name = "selectOutput";
            selectOutput.Size = new Size(75, 23);
            selectOutput.TabIndex = 4;
            selectOutput.Text = "Select";
            selectOutput.UseVisualStyleBackColor = true;
            selectOutput.Click += selectOutput_Click;
            // 
            // outputBox
            // 
            outputBox.Location = new Point(97, 97);
            outputBox.Name = "outputBox";
            outputBox.Size = new Size(558, 23);
            outputBox.TabIndex = 5;
            outputBox.TextChanged += outputBox_TextChanged;
            // 
            // outputFolderBrowserDialog
            // 
            outputFolderBrowserDialog.HelpRequest += outputfolderBrowserDialog_HelpRequest;
            // 
            // labelChargeRange
            // 
            labelChargeRange.AutoSize = true;
            labelChargeRange.Location = new Point(46, 166);
            labelChargeRange.Name = "labelChargeRange";
            labelChargeRange.Size = new Size(129, 15);
            labelChargeRange.TabIndex = 6;
            labelChargeRange.Text = "Precursor charge range";
            labelChargeRange.Click += labelMinCharge_Click;
            // 
            // labelDash
            // 
            labelDash.AutoSize = true;
            labelDash.Location = new Point(107, 199);
            labelDash.Name = "labelDash";
            labelDash.Size = new Size(12, 15);
            labelDash.TabIndex = 7;
            labelDash.Text = "-";
            labelDash.Click += label2_Click;
            // 
            // labelIsolationWidth
            // 
            labelIsolationWidth.AutoSize = true;
            labelIsolationWidth.Location = new Point(229, 166);
            labelIsolationWidth.Name = "labelIsolationWidth";
            labelIsolationWidth.Size = new Size(110, 15);
            labelIsolationWidth.TabIndex = 8;
            labelIsolationWidth.Text = "Isolation width (Th)";
            labelIsolationWidth.Click += label2_Click_1;
            // 
            // labelPPMTol
            // 
            labelPPMTol.AutoSize = true;
            labelPPMTol.Location = new Point(413, 166);
            labelPPMTol.Name = "labelPPMTol";
            labelPPMTol.Size = new Size(84, 15);
            labelPPMTol.TabIndex = 9;
            labelPPMTol.Text = "PPM tolerance";
            // 
            // labelMassRange
            // 
            labelMassRange.AutoSize = true;
            labelMassRange.Location = new Point(72, 266);
            labelMassRange.Name = "labelMassRange";
            labelMassRange.Size = new Size(98, 15);
            labelMassRange.TabIndex = 10;
            labelMassRange.Text = "Mass range (kDa)";
            labelMassRange.Click += labelMassRange_Click;
            // 
            // labelIntThreshold
            // 
            labelIntThreshold.AutoSize = true;
            labelIntThreshold.Location = new Point(234, 266);
            labelIntThreshold.Name = "labelIntThreshold";
            labelIntThreshold.Size = new Size(105, 15);
            labelIntThreshold.TabIndex = 11;
            labelIntThreshold.Text = "Intensity threshold";
            labelIntThreshold.Click += labelIntThreshold_Click;
            // 
            // labelExtraMz
            // 
            labelExtraMz.AutoSize = true;
            labelExtraMz.Location = new Point(413, 266);
            labelExtraMz.Name = "labelExtraMz";
            labelExtraMz.Size = new Size(94, 15);
            labelExtraMz.TabIndex = 12;
            labelExtraMz.Text = "Add m/z margin";
            labelExtraMz.Click += labelExtraMz_Click;
            // 
            // checkBoxPrecZOnly
            // 
            checkBoxPrecZOnly.AutoSize = true;
            checkBoxPrecZOnly.Location = new Point(583, 201);
            checkBoxPrecZOnly.Name = "checkBoxPrecZOnly";
            checkBoxPrecZOnly.Size = new Size(163, 19);
            checkBoxPrecZOnly.TabIndex = 14;
            checkBoxPrecZOnly.Text = "Use precursor charge only";
            checkBoxPrecZOnly.UseVisualStyleBackColor = true;
            checkBoxPrecZOnly.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // numericMinCharge
            // 
            numericMinCharge.Location = new Point(56, 197);
            numericMinCharge.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numericMinCharge.Name = "numericMinCharge";
            numericMinCharge.Size = new Size(45, 23);
            numericMinCharge.TabIndex = 15;
            numericMinCharge.Value = new decimal(new int[] { 2, 0, 0, 0 });
            numericMinCharge.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // numericMaxCharge
            // 
            numericMaxCharge.Location = new Point(125, 197);
            numericMaxCharge.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            numericMaxCharge.Name = "numericMaxCharge";
            numericMaxCharge.Size = new Size(45, 23);
            numericMaxCharge.TabIndex = 16;
            numericMaxCharge.Value = new decimal(new int[] { 10, 0, 0, 0 });
            numericMaxCharge.ValueChanged += numericUpDown2_ValueChanged;
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(596, 278);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(137, 41);
            buttonStart.TabIndex = 17;
            buttonStart.Text = "Start";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += button1_Click;
            // 
            // numericIsolWidth
            // 
            numericIsolWidth.DecimalPlaces = 2;
            numericIsolWidth.Location = new Point(230, 197);
            numericIsolWidth.Name = "numericIsolWidth";
            numericIsolWidth.Size = new Size(120, 23);
            numericIsolWidth.TabIndex = 18;
            numericIsolWidth.TextAlign = HorizontalAlignment.Center;
            numericIsolWidth.Value = new decimal(new int[] { 5, 0, 0, 0 });
            numericIsolWidth.ValueChanged += numericUpDown1_ValueChanged_1;
            // 
            // numericPPMTol
            // 
            numericPPMTol.Location = new Point(400, 197);
            numericPPMTol.Name = "numericPPMTol";
            numericPPMTol.Size = new Size(120, 23);
            numericPPMTol.TabIndex = 19;
            numericPPMTol.TextAlign = HorizontalAlignment.Center;
            numericPPMTol.Value = new decimal(new int[] { 20, 0, 0, 0 });
            numericPPMTol.ValueChanged += numericPPMTol_ValueChanged;
            // 
            // numericMinMass
            // 
            numericMinMass.Location = new Point(56, 296);
            numericMinMass.Name = "numericMinMass";
            numericMinMass.Size = new Size(45, 23);
            numericMinMass.TabIndex = 20;
            numericMinMass.TextAlign = HorizontalAlignment.Center;
            numericMinMass.ValueChanged += numericMinMass_ValueChanged;
            // 
            // numericMaxMass
            // 
            numericMaxMass.Location = new Point(125, 296);
            numericMaxMass.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            numericMaxMass.Name = "numericMaxMass";
            numericMaxMass.RightToLeft = RightToLeft.Yes;
            numericMaxMass.Size = new Size(45, 23);
            numericMaxMass.TabIndex = 21;
            numericMaxMass.TextAlign = HorizontalAlignment.Center;
            numericMaxMass.UpDownAlign = LeftRightAlignment.Left;
            numericMaxMass.Value = new decimal(new int[] { 100, 0, 0, 0 });
            numericMaxMass.ValueChanged += numericMaxMass_ValueChanged;
            // 
            // labelDash2
            // 
            labelDash2.AutoSize = true;
            labelDash2.Location = new Point(106, 298);
            labelDash2.Name = "labelDash2";
            labelDash2.Size = new Size(12, 15);
            labelDash2.TabIndex = 22;
            labelDash2.Text = "-";
            // 
            // numericIntThreshold
            // 
            numericIntThreshold.Location = new Point(230, 296);
            numericIntThreshold.Maximum = new decimal(new int[] { -727379968, 232, 0, 0 });
            numericIntThreshold.Name = "numericIntThreshold";
            numericIntThreshold.Size = new Size(120, 23);
            numericIntThreshold.TabIndex = 23;
            numericIntThreshold.TextAlign = HorizontalAlignment.Center;
            numericIntThreshold.ValueChanged += numericIntThreshold_ValueChanged;
            // 
            // numericMzMargMin
            // 
            numericMzMargMin.DecimalPlaces = 2;
            numericMzMargMin.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numericMzMargMin.Location = new Point(406, 296);
            numericMzMargMin.Name = "numericMzMargMin";
            numericMzMargMin.Size = new Size(45, 23);
            numericMzMargMin.TabIndex = 24;
            numericMzMargMin.TextAlign = HorizontalAlignment.Center;
            numericMzMargMin.ValueChanged += numericMzMargMin_ValueChanged;
            // 
            // numericMzMargMax
            // 
            numericMzMargMax.DecimalPlaces = 2;
            numericMzMargMax.Increment = new decimal(new int[] { 1, 0, 0, 131072 });
            numericMzMargMax.Location = new Point(475, 296);
            numericMzMargMax.Name = "numericMzMargMax";
            numericMzMargMax.Size = new Size(45, 23);
            numericMzMargMax.TabIndex = 25;
            numericMzMargMax.TextAlign = HorizontalAlignment.Center;
            numericMzMargMax.ValueChanged += numericMzMargMax_ValueChanged;
            // 
            // labelDash3
            // 
            labelDash3.AutoSize = true;
            labelDash3.Location = new Point(457, 298);
            labelDash3.Name = "labelDash3";
            labelDash3.Size = new Size(12, 15);
            labelDash3.TabIndex = 26;
            labelDash3.Text = "-";
            // 
            // labelStatus
            // 
            labelStatus.AutoSize = true;
            labelStatus.Location = new Point(12, 360);
            labelStatus.Name = "labelStatus";
            labelStatus.Size = new Size(64, 15);
            labelStatus.TabIndex = 27;
            labelStatus.Text = "Status: Idle";
            labelStatus.Click += labelStatus_Click;
            // 
            // toolTipCharge
            // 
            toolTipCharge.Popup += toolTipCharge_Popup;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 384);
            Controls.Add(labelStatus);
            Controls.Add(labelDash3);
            Controls.Add(numericMzMargMax);
            Controls.Add(numericMzMargMin);
            Controls.Add(numericIntThreshold);
            Controls.Add(labelDash2);
            Controls.Add(numericMaxMass);
            Controls.Add(numericMinMass);
            Controls.Add(numericPPMTol);
            Controls.Add(numericIsolWidth);
            Controls.Add(buttonStart);
            Controls.Add(numericMaxCharge);
            Controls.Add(numericMinCharge);
            Controls.Add(checkBoxPrecZOnly);
            Controls.Add(labelExtraMz);
            Controls.Add(labelIntThreshold);
            Controls.Add(labelMassRange);
            Controls.Add(labelPPMTol);
            Controls.Add(labelIsolationWidth);
            Controls.Add(labelDash);
            Controls.Add(labelChargeRange);
            Controls.Add(outputBox);
            Controls.Add(selectOutput);
            Controls.Add(label1);
            Controls.Add(button_select);
            Controls.Add(inputBox);
            Controls.Add(label_input);
            Name = "Form1";
            Text = "PTCR Cleaner GUI";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)numericMinCharge).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericMaxCharge).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericIsolWidth).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericPPMTol).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericMinMass).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericMaxMass).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericIntThreshold).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericMzMargMin).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericMzMargMax).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label_input;
        private TextBox inputBox;
        private Button button_select;
        private OpenFileDialog openFileDialog_Input;
        private Label label1;
        private Button selectOutput;
        private TextBox outputBox;
        private FolderBrowserDialog outputFolderBrowserDialog;
        private Label labelChargeRange;
        private Label labelDash;
        private Label labelIsolationWidth;
        private Label labelPPMTol;
        private Label labelMassRange;
        private Label labelIntThreshold;
        private Label labelExtraMz;
        private CheckBox checkBoxPrecZOnly;
        private NumericUpDown numericMinCharge;
        private NumericUpDown numericMaxCharge;
        private Button buttonStart;
        private NumericUpDown numericIsolWidth;
        private NumericUpDown numericPPMTol;
        private NumericUpDown numericMinMass;
        private NumericUpDown numericMaxMass;
        private Label labelDash2;
        private NumericUpDown numericIntThreshold;
        private NumericUpDown numericMzMargMin;
        private NumericUpDown numericMzMargMax;
        private Label labelDash3;
        private Label labelStatus;
        private ToolTip toolTipCharge;
        private ToolTip toolTipIsolWidth;
        private ToolTip toolTipPPMTol;
        private ToolTip toolTipPrecZ;
        private ToolTip toolTipMassRange;
        private ToolTip toolTipIntThresh;
        private ToolTip toolTipAddMZ;
    }
}
