
namespace FileManager
{
    partial class InputForm
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
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnEnter = new System.Windows.Forms.Button();
            this.lblScore = new System.Windows.Forms.Label();
            this.lblBlocks = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(35, 124);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(80, 25);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name: ";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(121, 121);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(344, 31);
            this.txtName.TabIndex = 1;
            // 
            // btnEnter
            // 
            this.btnEnter.Location = new System.Drawing.Point(485, 120);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(127, 40);
            this.btnEnter.TabIndex = 2;
            this.btnEnter.Text = "ENTER";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // lblScore
            // 
            this.lblScore.AutoSize = true;
            this.lblScore.Location = new System.Drawing.Point(35, 54);
            this.lblScore.Name = "lblScore";
            this.lblScore.Size = new System.Drawing.Size(129, 25);
            this.lblScore.TabIndex = 3;
            this.lblScore.Text = "Your score: ";
            // 
            // lblBlocks
            // 
            this.lblBlocks.AutoSize = true;
            this.lblBlocks.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBlocks.Location = new System.Drawing.Point(163, 45);
            this.lblBlocks.Name = "lblBlocks";
            this.lblBlocks.Size = new System.Drawing.Size(34, 37);
            this.lblBlocks.TabIndex = 4;
            this.lblBlocks.Text = "1";
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 205);
            this.Controls.Add(this.lblBlocks);
            this.Controls.Add(this.lblScore);
            this.Controls.Add(this.btnEnter);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Name = "InputForm";
            this.Text = "Fifty-Five";
            this.Load += new System.EventHandler(this.InputForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnEnter;
        private System.Windows.Forms.Label lblScore;
        private System.Windows.Forms.Label lblBlocks;
    }
}

