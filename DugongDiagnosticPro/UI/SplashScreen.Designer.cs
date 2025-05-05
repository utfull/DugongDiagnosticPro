namespace DugongDiagnosticPro.UI
{
    partial class SplashScreen
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.BackColor = System.Drawing.Color.Black;
            this.Name = "SplashScreen";
            this.Text = "Dugong Diagnostic Pro";
            this.ResumeLayout(false);
        }
    }
}