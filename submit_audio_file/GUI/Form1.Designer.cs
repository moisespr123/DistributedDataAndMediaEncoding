namespace GUI
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.PathTxt = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.opusRadioButton = new System.Windows.Forms.RadioButton();
            this.flacRadioButton = new System.Windows.Forms.RadioButton();
            this.CategoryTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ffmpegLibOpusRadioButton = new System.Windows.Forms.RadioButton();
            this.bitrateUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.userKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bitrateUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Path";
            // 
            // PathTxt
            // 
            this.PathTxt.Location = new System.Drawing.Point(12, 29);
            this.PathTxt.Name = "PathTxt";
            this.PathTxt.Size = new System.Drawing.Size(319, 20);
            this.PathTxt.TabIndex = 1;
            this.PathTxt.TextChanged += new System.EventHandler(this.PathTxt_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ffmpegLibOpusRadioButton);
            this.groupBox1.Controls.Add(this.opusRadioButton);
            this.groupBox1.Controls.Add(this.flacRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 55);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(205, 45);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Format";
            // 
            // opusRadioButton
            // 
            this.opusRadioButton.AutoSize = true;
            this.opusRadioButton.Location = new System.Drawing.Point(54, 16);
            this.opusRadioButton.Name = "opusRadioButton";
            this.opusRadioButton.Size = new System.Drawing.Size(48, 17);
            this.opusRadioButton.TabIndex = 1;
            this.opusRadioButton.TabStop = true;
            this.opusRadioButton.Text = "opus";
            this.opusRadioButton.UseVisualStyleBackColor = true;
            // 
            // flacRadioButton
            // 
            this.flacRadioButton.AutoSize = true;
            this.flacRadioButton.Location = new System.Drawing.Point(6, 16);
            this.flacRadioButton.Name = "flacRadioButton";
            this.flacRadioButton.Size = new System.Drawing.Size(42, 17);
            this.flacRadioButton.TabIndex = 0;
            this.flacRadioButton.TabStop = true;
            this.flacRadioButton.Text = "flac";
            this.flacRadioButton.UseVisualStyleBackColor = true;
            // 
            // CategoryTxt
            // 
            this.CategoryTxt.Location = new System.Drawing.Point(12, 119);
            this.CategoryTxt.Name = "CategoryTxt";
            this.CategoryTxt.Size = new System.Drawing.Size(319, 20);
            this.CategoryTxt.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Category";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 184);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(319, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "submit";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.bitrateUpDown);
            this.groupBox2.Location = new System.Drawing.Point(223, 55);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(108, 45);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Bitrate";
            // 
            // ffmpegLibOpusRadioButton
            // 
            this.ffmpegLibOpusRadioButton.AutoSize = true;
            this.ffmpegLibOpusRadioButton.Location = new System.Drawing.Point(108, 16);
            this.ffmpegLibOpusRadioButton.Name = "ffmpegLibOpusRadioButton";
            this.ffmpegLibOpusRadioButton.Size = new System.Drawing.Size(93, 17);
            this.ffmpegLibOpusRadioButton.TabIndex = 2;
            this.ffmpegLibOpusRadioButton.TabStop = true;
            this.ffmpegLibOpusRadioButton.Text = "ffmpeg libopus";
            this.ffmpegLibOpusRadioButton.UseVisualStyleBackColor = true;
            // 
            // bitrateUpDown
            // 
            this.bitrateUpDown.Location = new System.Drawing.Point(6, 19);
            this.bitrateUpDown.Name = "bitrateUpDown";
            this.bitrateUpDown.Size = new System.Drawing.Size(63, 20);
            this.bitrateUpDown.TabIndex = 0;
            this.bitrateUpDown.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(72, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "kbps";
            // 
            // userKey
            // 
            this.userKey.Location = new System.Drawing.Point(12, 158);
            this.userKey.Name = "userKey";
            this.userKey.Size = new System.Drawing.Size(319, 20);
            this.userKey.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 142);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "User Key";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 228);
            this.Controls.Add(this.userKey);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CategoryTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.PathTxt);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Audio Submitter GUI";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bitrateUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PathTxt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton opusRadioButton;
        private System.Windows.Forms.RadioButton flacRadioButton;
        private System.Windows.Forms.TextBox CategoryTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton ffmpegLibOpusRadioButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown bitrateUpDown;
        private System.Windows.Forms.TextBox userKey;
        private System.Windows.Forms.Label label4;
    }
}

