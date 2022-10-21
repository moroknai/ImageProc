namespace ImgProc
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
            this.pbOriginal = new System.Windows.Forms.PictureBox();
            this.pbEdited = new System.Windows.Forms.PictureBox();
            this.lblEdited = new System.Windows.Forms.Label();
            this.lblOriginal = new System.Windows.Forms.Label();
            this.btnNext = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEdited)).BeginInit();
            this.SuspendLayout();
            // 
            // pbOriginal
            // 
            this.pbOriginal.Location = new System.Drawing.Point(12, 39);
            this.pbOriginal.Name = "pbOriginal";
            this.pbOriginal.Size = new System.Drawing.Size(368, 368);
            this.pbOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbOriginal.TabIndex = 0;
            this.pbOriginal.TabStop = false;
            // 
            // pbEdited
            // 
            this.pbEdited.Location = new System.Drawing.Point(386, 39);
            this.pbEdited.Name = "pbEdited";
            this.pbEdited.Size = new System.Drawing.Size(368, 368);
            this.pbEdited.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbEdited.TabIndex = 1;
            this.pbEdited.TabStop = false;
            // 
            // lblEdited
            // 
            this.lblEdited.AutoSize = true;
            this.lblEdited.Location = new System.Drawing.Point(383, 13);
            this.lblEdited.Name = "lblEdited";
            this.lblEdited.Size = new System.Drawing.Size(37, 13);
            this.lblEdited.TabIndex = 2;
            this.lblEdited.Text = "Edited";
            // 
            // lblOriginal
            // 
            this.lblOriginal.AutoSize = true;
            this.lblOriginal.Location = new System.Drawing.Point(12, 13);
            this.lblOriginal.Name = "lblOriginal";
            this.lblOriginal.Size = new System.Drawing.Size(42, 13);
            this.lblOriginal.TabIndex = 3;
            this.lblOriginal.Text = "Original";
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(12, 414);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(742, 40);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 466);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.lblOriginal);
            this.Controls.Add(this.lblEdited);
            this.Controls.Add(this.pbEdited);
            this.Controls.Add(this.pbOriginal);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEdited)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbOriginal;
        private System.Windows.Forms.PictureBox pbEdited;
        private System.Windows.Forms.Label lblEdited;
        private System.Windows.Forms.Label lblOriginal;
        private System.Windows.Forms.Button btnNext;
    }
}

