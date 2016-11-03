namespace ArduinoACP
{
    partial class ArduinoACP
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.deviceInformationLB = new System.Windows.Forms.ListBox();
            this.ApplicationLogText = new System.Windows.Forms.RichTextBox();
            this.ArduinoCodeEditor = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.ComInfoTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BaudInfoTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.statusBox = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Desktop;
            this.button1.BackgroundImage = global::ArduinoACP.Properties.Resources.serialIcon;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(14, 15);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.button1.Size = new System.Drawing.Size(78, 234);
            this.button1.TabIndex = 0;
            this.button1.Text = "ATMC";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Desktop;
            this.button2.BackgroundImage = global::ArduinoACP.Properties.Resources.serialIcon;
            this.button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(528, 15);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.button2.Size = new System.Drawing.Size(78, 234);
            this.button2.TabIndex = 0;
            this.button2.Text = "Upload to Arduino";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // deviceInformationLB
            // 
            this.deviceInformationLB.BackColor = System.Drawing.SystemColors.Desktop;
            this.deviceInformationLB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.deviceInformationLB.ForeColor = System.Drawing.SystemColors.Info;
            this.deviceInformationLB.FormattingEnabled = true;
            this.deviceInformationLB.ItemHeight = 16;
            this.deviceInformationLB.Location = new System.Drawing.Point(98, 31);
            this.deviceInformationLB.Name = "deviceInformationLB";
            this.deviceInformationLB.Size = new System.Drawing.Size(423, 82);
            this.deviceInformationLB.TabIndex = 1;
            // 
            // ApplicationLogText
            // 
            this.ApplicationLogText.BackColor = System.Drawing.SystemColors.Desktop;
            this.ApplicationLogText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ApplicationLogText.ForeColor = System.Drawing.SystemColors.Info;
            this.ApplicationLogText.Location = new System.Drawing.Point(98, 151);
            this.ApplicationLogText.Margin = new System.Windows.Forms.Padding(5);
            this.ApplicationLogText.Name = "ApplicationLogText";
            this.ApplicationLogText.Size = new System.Drawing.Size(423, 97);
            this.ApplicationLogText.TabIndex = 2;
            this.ApplicationLogText.Text = "Texbox LOG";
            // 
            // ArduinoCodeEditor
            // 
            this.ArduinoCodeEditor.BackColor = System.Drawing.SystemColors.Desktop;
            this.ArduinoCodeEditor.ForeColor = System.Drawing.SystemColors.Info;
            this.ArduinoCodeEditor.Location = new System.Drawing.Point(14, 256);
            this.ArduinoCodeEditor.Multiline = true;
            this.ArduinoCodeEditor.Name = "ArduinoCodeEditor";
            this.ArduinoCodeEditor.Size = new System.Drawing.Size(590, 68);
            this.ArduinoCodeEditor.TabIndex = 3;
            this.ArduinoCodeEditor.Text = "Arduino Code Editor";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(14, 333);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(166, 29);
            this.button3.TabIndex = 4;
            this.button3.Text = "Detect Arduino";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.DetectArduino_Click);
            // 
            // ComInfoTB
            // 
            this.ComInfoTB.BackColor = System.Drawing.SystemColors.Desktop;
            this.ComInfoTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ComInfoTB.Enabled = false;
            this.ComInfoTB.ForeColor = System.Drawing.SystemColors.Info;
            this.ComInfoTB.Location = new System.Drawing.Point(182, 120);
            this.ComInfoTB.Name = "ComInfoTB";
            this.ComInfoTB.Size = new System.Drawing.Size(100, 23);
            this.ComInfoTB.TabIndex = 5;
            this.ComInfoTB.Text = "COM";
            this.ComInfoTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(98, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Serial Port:";
            // 
            // BaudInfoTB
            // 
            this.BaudInfoTB.BackColor = System.Drawing.SystemColors.Desktop;
            this.BaudInfoTB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BaudInfoTB.Enabled = false;
            this.BaudInfoTB.ForeColor = System.Drawing.SystemColors.Info;
            this.BaudInfoTB.Location = new System.Drawing.Point(421, 120);
            this.BaudInfoTB.Name = "BaudInfoTB";
            this.BaudInfoTB.Size = new System.Drawing.Size(100, 23);
            this.BaudInfoTB.TabIndex = 5;
            this.BaudInfoTB.Text = "9600";
            this.BaudInfoTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(336, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Baud Rate:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(98, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Device Info:";
            // 
            // statusBox
            // 
            this.statusBox.AutoSize = true;
            this.statusBox.Location = new System.Drawing.Point(186, 339);
            this.statusBox.Name = "statusBox";
            this.statusBox.Size = new System.Drawing.Size(46, 16);
            this.statusBox.TabIndex = 7;
            this.statusBox.Text = "label4";
            // 
            // ArduinoACP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(618, 375);
            this.Controls.Add(this.statusBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BaudInfoTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ComInfoTB);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.ArduinoCodeEditor);
            this.Controls.Add(this.ApplicationLogText);
            this.Controls.Add(this.deviceInformationLB);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Arial", 10F);
            this.Name = "ArduinoACP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Arduino Control Panel";
            this.Load += new System.EventHandler(this.ArduinoACP_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox deviceInformationLB;
        private System.Windows.Forms.RichTextBox ApplicationLogText;
        public System.Windows.Forms.TextBox ArduinoCodeEditor;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox ComInfoTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox BaudInfoTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label statusBox;
    }
}

