using geneticInformationSystem;

namespace booleanTestInterface {
    partial class gaTestInterface {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.inputGB = new System.Windows.Forms.GroupBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.manualTab = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.postfixTB = new System.Windows.Forms.TextBox();
            this.pfConvert = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.infixTB = new System.Windows.Forms.TextBox();
            this.autoTab = new System.Windows.Forms.TabPage();
            this.generatedDNATB = new System.Windows.Forms.TextBox();
            this.generate = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.thresholdTB = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numInputsTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lengthTB = new System.Windows.Forms.TextBox();
            this.statusBar = new System.Windows.Forms.Label();
            this.interfaceErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.processTab = new System.Windows.Forms.TabControl();
            this.lexTab = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.lexTB = new System.Windows.Forms.TextBox();
            this.lexButton = new System.Windows.Forms.Button();
            this.parseTab = new System.Windows.Forms.TabPage();
            this.astLB = new System.Windows.Forms.ListBox();
            this.treePB = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.parseButton = new System.Windows.Forms.Button();
            this.evaluateTab = new System.Windows.Forms.TabPage();
            this.inputGB.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.manualTab.SuspendLayout();
            this.autoTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.interfaceErrorProvider)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.processTab.SuspendLayout();
            this.lexTab.SuspendLayout();
            this.parseTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treePB)).BeginInit();
            this.SuspendLayout();
            // 
            // inputGB
            // 
            this.inputGB.Controls.Add(this.clearButton);
            this.inputGB.Controls.Add(this.tabControl1);
            this.inputGB.Location = new System.Drawing.Point(12, 12);
            this.inputGB.Name = "inputGB";
            this.inputGB.Size = new System.Drawing.Size(776, 167);
            this.inputGB.TabIndex = 0;
            this.inputGB.TabStop = false;
            this.inputGB.Text = "Input";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(685, 140);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 1;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.manualTab);
            this.tabControl1.Controls.Add(this.autoTab);
            this.tabControl1.Location = new System.Drawing.Point(6, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(764, 119);
            this.tabControl1.TabIndex = 0;
            // 
            // manualTab
            // 
            this.manualTab.Controls.Add(this.label2);
            this.manualTab.Controls.Add(this.postfixTB);
            this.manualTab.Controls.Add(this.pfConvert);
            this.manualTab.Controls.Add(this.label1);
            this.manualTab.Controls.Add(this.infixTB);
            this.manualTab.Location = new System.Drawing.Point(4, 22);
            this.manualTab.Name = "manualTab";
            this.manualTab.Padding = new System.Windows.Forms.Padding(3);
            this.manualTab.Size = new System.Drawing.Size(756, 93);
            this.manualTab.TabIndex = 0;
            this.manualTab.Text = "Manual";
            this.manualTab.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "PostFix Expression";
            // 
            // postfixTB
            // 
            this.postfixTB.Location = new System.Drawing.Point(6, 61);
            this.postfixTB.Multiline = true;
            this.postfixTB.Name = "postfixTB";
            this.postfixTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.postfixTB.Size = new System.Drawing.Size(744, 27);
            this.postfixTB.TabIndex = 3;
            // 
            // pfConvert
            // 
            this.pfConvert.Location = new System.Drawing.Point(6, 20);
            this.pfConvert.Name = "pfConvert";
            this.pfConvert.Size = new System.Drawing.Size(106, 23);
            this.pfConvert.TabIndex = 2;
            this.pfConvert.Text = "Convert to PostFix";
            this.pfConvert.UseVisualStyleBackColor = true;
            this.pfConvert.Click += new System.EventHandler(this.pfConvert_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "InFix Expression";
            // 
            // infixTB
            // 
            this.infixTB.Location = new System.Drawing.Point(118, 4);
            this.infixTB.Multiline = true;
            this.infixTB.Name = "infixTB";
            this.infixTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.infixTB.Size = new System.Drawing.Size(632, 35);
            this.infixTB.TabIndex = 0;
            // 
            // autoTab
            // 
            this.autoTab.Controls.Add(this.generatedDNATB);
            this.autoTab.Controls.Add(this.generate);
            this.autoTab.Controls.Add(this.label4);
            this.autoTab.Controls.Add(this.thresholdTB);
            this.autoTab.Controls.Add(this.label5);
            this.autoTab.Controls.Add(this.numInputsTB);
            this.autoTab.Controls.Add(this.label3);
            this.autoTab.Controls.Add(this.lengthTB);
            this.autoTab.Location = new System.Drawing.Point(4, 22);
            this.autoTab.Name = "autoTab";
            this.autoTab.Padding = new System.Windows.Forms.Padding(3);
            this.autoTab.Size = new System.Drawing.Size(756, 93);
            this.autoTab.TabIndex = 1;
            this.autoTab.Text = "Automatic";
            this.autoTab.UseVisualStyleBackColor = true;
            // 
            // generatedDNATB
            // 
            this.generatedDNATB.Location = new System.Drawing.Point(186, 34);
            this.generatedDNATB.Multiline = true;
            this.generatedDNATB.Name = "generatedDNATB";
            this.generatedDNATB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.generatedDNATB.Size = new System.Drawing.Size(564, 48);
            this.generatedDNATB.TabIndex = 3;
            // 
            // generate
            // 
            this.generate.Location = new System.Drawing.Point(186, 8);
            this.generate.Name = "generate";
            this.generate.Size = new System.Drawing.Size(93, 23);
            this.generate.TabIndex = 2;
            this.generate.Text = "Generate DNA";
            this.generate.UseVisualStyleBackColor = true;
            this.generate.Click += new System.EventHandler(this.generate_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Input Operator Threshold";
            // 
            // thresholdTB
            // 
            this.thresholdTB.Location = new System.Drawing.Point(135, 36);
            this.thresholdTB.Name = "thresholdTB";
            this.thresholdTB.Size = new System.Drawing.Size(32, 20);
            this.thresholdTB.TabIndex = 0;
            this.thresholdTB.Text = "50";
            this.thresholdTB.Validating += new System.ComponentModel.CancelEventHandler(this.thresholdTB_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Number of Inputs";
            // 
            // numInputsTB
            // 
            this.numInputsTB.Location = new System.Drawing.Point(135, 62);
            this.numInputsTB.Name = "numInputsTB";
            this.numInputsTB.Size = new System.Drawing.Size(32, 20);
            this.numInputsTB.TabIndex = 0;
            this.numInputsTB.Text = "3";
            this.numInputsTB.Validating += new System.ComponentModel.CancelEventHandler(this.numInputsTB_Validating);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Expression Length";
            // 
            // lengthTB
            // 
            this.lengthTB.Location = new System.Drawing.Point(135, 10);
            this.lengthTB.Name = "lengthTB";
            this.lengthTB.Size = new System.Drawing.Size(32, 20);
            this.lengthTB.TabIndex = 0;
            this.lengthTB.Text = "50";
            this.lengthTB.Validating += new System.ComponentModel.CancelEventHandler(this.lengthTB_Validating);
            // 
            // statusBar
            // 
            this.statusBar.AutoSize = true;
            this.statusBar.Location = new System.Drawing.Point(9, 428);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(0, 13);
            this.statusBar.TabIndex = 1;
            // 
            // interfaceErrorProvider
            // 
            this.interfaceErrorProvider.ContainerControl = this;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.processTab);
            this.groupBox1.Location = new System.Drawing.Point(13, 184);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(930, 440);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Process";
            // 
            // processTab
            // 
            this.processTab.Controls.Add(this.lexTab);
            this.processTab.Controls.Add(this.parseTab);
            this.processTab.Controls.Add(this.evaluateTab);
            this.processTab.Location = new System.Drawing.Point(9, 20);
            this.processTab.Name = "processTab";
            this.processTab.SelectedIndex = 0;
            this.processTab.Size = new System.Drawing.Size(913, 420);
            this.processTab.TabIndex = 0;
            // 
            // lexTab
            // 
            this.lexTab.Controls.Add(this.label6);
            this.lexTab.Controls.Add(this.lexTB);
            this.lexTab.Controls.Add(this.lexButton);
            this.lexTab.Location = new System.Drawing.Point(4, 22);
            this.lexTab.Name = "lexTab";
            this.lexTab.Padding = new System.Windows.Forms.Padding(3);
            this.lexTab.Size = new System.Drawing.Size(905, 394);
            this.lexTab.TabIndex = 0;
            this.lexTab.Text = "Lex";
            this.lexTab.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(87, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Tokens";
            // 
            // lexTB
            // 
            this.lexTB.Location = new System.Drawing.Point(87, 31);
            this.lexTB.Multiline = true;
            this.lexTB.Name = "lexTB";
            this.lexTB.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.lexTB.Size = new System.Drawing.Size(649, 165);
            this.lexTB.TabIndex = 1;
            // 
            // lexButton
            // 
            this.lexButton.Location = new System.Drawing.Point(6, 6);
            this.lexButton.Name = "lexButton";
            this.lexButton.Size = new System.Drawing.Size(75, 23);
            this.lexButton.TabIndex = 0;
            this.lexButton.Text = "Lex DNA";
            this.lexButton.UseVisualStyleBackColor = true;
            this.lexButton.Click += new System.EventHandler(this.lexButton_Click);
            // 
            // parseTab
            // 
            this.parseTab.Controls.Add(this.astLB);
            this.parseTab.Controls.Add(this.treePB);
            this.parseTab.Controls.Add(this.label7);
            this.parseTab.Controls.Add(this.parseButton);
            this.parseTab.Location = new System.Drawing.Point(4, 22);
            this.parseTab.Name = "parseTab";
            this.parseTab.Padding = new System.Windows.Forms.Padding(3);
            this.parseTab.Size = new System.Drawing.Size(905, 394);
            this.parseTab.TabIndex = 1;
            this.parseTab.Text = "Parse";
            this.parseTab.UseVisualStyleBackColor = true;
            // 
            // astLB
            // 
            this.astLB.FormattingEnabled = true;
            this.astLB.Location = new System.Drawing.Point(7, 34);
            this.astLB.Name = "astLB";
            this.astLB.Size = new System.Drawing.Size(208, 173);
            this.astLB.TabIndex = 6;
            this.astLB.SelectedIndexChanged += new System.EventHandler(this.astLB_SelectedIndexChanged);
            // 
            // treePB
            // 
            this.treePB.Location = new System.Drawing.Point(221, 10);
            this.treePB.Name = "treePB";
            this.treePB.Size = new System.Drawing.Size(678, 378);
            this.treePB.TabIndex = 5;
            this.treePB.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(104, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(111, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Abstract Syntax Trees";
            // 
            // parseButton
            // 
            this.parseButton.Location = new System.Drawing.Point(7, 5);
            this.parseButton.Name = "parseButton";
            this.parseButton.Size = new System.Drawing.Size(91, 23);
            this.parseButton.TabIndex = 0;
            this.parseButton.Text = "Parse Tokens";
            this.parseButton.UseVisualStyleBackColor = true;
            this.parseButton.Click += new System.EventHandler(this.parseButton_Click);
            // 
            // evaluateTab
            // 
            this.evaluateTab.Location = new System.Drawing.Point(4, 22);
            this.evaluateTab.Name = "evaluateTab";
            this.evaluateTab.Size = new System.Drawing.Size(905, 394);
            this.evaluateTab.TabIndex = 2;
            this.evaluateTab.Text = "Evaluate";
            this.evaluateTab.UseVisualStyleBackColor = true;
            // 
            // gaTestInterface
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(947, 636);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.inputGB);
            this.Name = "gaTestInterface";
            this.Text = "Boolean Genetic Algorithm Test Interface";
            this.inputGB.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.manualTab.ResumeLayout(false);
            this.manualTab.PerformLayout();
            this.autoTab.ResumeLayout(false);
            this.autoTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.interfaceErrorProvider)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.processTab.ResumeLayout(false);
            this.lexTab.ResumeLayout(false);
            this.lexTab.PerformLayout();
            this.parseTab.ResumeLayout(false);
            this.parseTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treePB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox inputGB;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage manualTab;
        private System.Windows.Forms.TabPage autoTab;
        private System.Windows.Forms.TextBox postfixTB;
        private System.Windows.Forms.Button pfConvert;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox infixTB;
        private System.Windows.Forms.Label statusBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox generatedDNATB;
        private System.Windows.Forms.Button generate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox thresholdTB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox numInputsTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox lengthTB;

        private giSystem gisystem = new giSystem();
        private System.Windows.Forms.ErrorProvider interfaceErrorProvider;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabControl processTab;
        private System.Windows.Forms.TabPage lexTab;
        private System.Windows.Forms.TextBox lexTB;
        private System.Windows.Forms.Button lexButton;
        private System.Windows.Forms.TabPage parseTab;
        private System.Windows.Forms.TabPage evaluateTab;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button parseButton;
        private System.Windows.Forms.PictureBox treePB;
        private System.Windows.Forms.ListBox astLB;
    }
}

