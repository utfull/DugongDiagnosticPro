using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace DugongDiagnosticPro.UI
{
    public partial class SplashScreen : Form
    {
        private PictureBox pictureBox;
        private Timer closeTimer;
        private Timer fadeTimer;
        private int opacity = 100;

        public SplashScreen(bool isDugongSystem)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Opacity = 0.0; // Start invisible for fade-in effect
            this.BackColor = Color.Black;

            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.BackColor = Color.Transparent;
            
            string imagePath = isDugongSystem ? "dugongsplash1.png" : "dugongsplash2.png";
            string resourcePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Resources", imagePath);
            
            if (File.Exists(resourcePath))
            {
                pictureBox.Image = Image.FromFile(resourcePath);
            }
            else
            {
                try
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    string resourceName = $"DugongDiagnosticPro.Resources.{imagePath}";
                    using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                    {
                        if (stream != null)
                            pictureBox.Image = Image.FromStream(stream);
                    }
                }
                catch
                {
                    this.Close();
                    return;
                }
            }
            
            
            this.Size = new Size(800, 500);
            
            // Add a border to make the splash screen more visible
            Panel borderPanel = new Panel();
            borderPanel.Dock = DockStyle.Fill;
            borderPanel.Padding = new Padding(3);
            borderPanel.BackColor = Color.DodgerBlue;
            
            // Add a label with the application name
            Label titleLabel = new Label();
            titleLabel.Text = "Dugong Diagnostic Pro";
            titleLabel.Font = new Font("Arial", 16, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.BackColor = Color.Transparent;
            titleLabel.Dock = DockStyle.Bottom;
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            titleLabel.Height = 40;
            
            // Add version label
            Label versionLabel = new Label();
            versionLabel.Text = "Version 1.0";
            versionLabel.Font = new Font("Arial", 9, FontStyle.Regular);
            versionLabel.ForeColor = Color.Silver;
            versionLabel.BackColor = Color.Transparent;
            versionLabel.Dock = DockStyle.Bottom;
            versionLabel.TextAlign = ContentAlignment.MiddleCenter;
            versionLabel.Height = 20;
            
            // Add loading animation
            ProgressBar loadingBar = new ProgressBar();
            loadingBar.Style = ProgressBarStyle.Marquee;
            loadingBar.MarqueeAnimationSpeed = 30;
            loadingBar.Height = 5;
            loadingBar.Dock = DockStyle.Bottom;
            
            this.Controls.Add(borderPanel);
            borderPanel.Controls.Add(pictureBox);
            borderPanel.Controls.Add(loadingBar);
            borderPanel.Controls.Add(versionLabel);
            borderPanel.Controls.Add(titleLabel);
            
            // Start the fade-in effect
            StartFadeIn();
        }

        private void StartFadeIn()
        {
            // Create and start the fade-in timer
            fadeTimer = new Timer();
            fadeTimer.Interval = 20;
            fadeTimer.Tick += (s, e) =>
            {
                opacity += 5;
                if (opacity >= 100)
                {
                    opacity = 100;
                    fadeTimer.Stop();
                    
                    // After fade-in completes, set a timer to start fade-out after 5 seconds
                    closeTimer = new Timer();
                    closeTimer.Interval = 5000;
                    closeTimer.Tick += (s2, e2) =>
                    {
                        closeTimer.Stop();
                        StartFadeOut();
                    };
                    closeTimer.Start();
                }
                this.Opacity = opacity / 100.0;
            };
            fadeTimer.Start();
        }
        
        private void StartFadeOut()
        {
            // Create and start the fade-out timer
            fadeTimer = new Timer();
            fadeTimer.Interval = 20;
            fadeTimer.Tick += (s, e) =>
            {
                opacity -= 3;
                if (opacity <= 0)
                {
                    opacity = 0;
                    fadeTimer.Stop();
                    this.Close();
                }
                this.Opacity = opacity / 100.0;
            };
            fadeTimer.Start();
        }
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
    }
}