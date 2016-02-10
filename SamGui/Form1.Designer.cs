namespace SamGui
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.customSliderLeft1 = new SamGui.CustomSliderLeft();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(990, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // customSliderLeft1
            // 
            this.customSliderLeft1.Location = new System.Drawing.Point(126, 0);
            this.customSliderLeft1.Name = "customSliderLeft1";
            this.customSliderLeft1.Size = new System.Drawing.Size(760, 683);
            this.customSliderLeft1.TabIndex = 0;
            this.customSliderLeft1.Text = "customSliderLeft1";
            this.customSliderLeft1.TMBsize = 50;
            this.customSliderLeft1.ScrollLeft += new System.Windows.Forms.ScrollEventHandler(this.customSliderLeft1_ScrollLeft);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(990, 737);
            this.Controls.Add(this.customSliderLeft1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomSliderLeft customSliderLeft1;
        private System.Windows.Forms.MenuStrip menuStrip1;





    }
}

