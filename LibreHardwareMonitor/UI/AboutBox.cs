// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at http://mozilla.org/MPL/2.0/.
// Copyright (C) LibreHardwareMonitor and Contributors.
// Partial Copyright (C) Michael Möller <mmoeller@openhardwaremonitor.org> and Contributors.
// All Rights Reserved.

using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LibreHardwareMonitor.UI.Themes;

namespace LibreHardwareMonitor.UI;

public sealed partial class AboutBox : Form
{
    public AboutBox()
    {
        InitializeComponent();
        Font = SystemFonts.MessageBoxFont;
        
        // Load the Dugong logo directly from the file and resize it
        try
        {
            string logoPath = Path.Combine(Application.StartupPath, "Resources", "dugonglogo1.png");
            if (File.Exists(logoPath))
            {
                using (Image originalImage = Image.FromFile(logoPath))
                {
                    // Create a smaller version of the image (48x48 pixels)
                    Bitmap resizedImage = new Bitmap(48, 48);
                    using (Graphics g = Graphics.FromImage(resizedImage))
                    {
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                        g.DrawImage(originalImage, 0, 0, 48, 48);
                    }
                    pictureBox1.Image = resizedImage;
                }
            }
            else
            {
                Image originalImage = Utilities.EmbeddedResources.GetImage("dugonglogo1.png");
                // Create a smaller version of the image (48x48 pixels)
                Bitmap resizedImage = new Bitmap(48, 48);
                using (Graphics g = Graphics.FromImage(resizedImage))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(originalImage, 0, 0, 48, 48);
                }
                pictureBox1.Image = resizedImage;
            }
        }
        catch { }
        
        label3.Text = "Version " + Application.ProductVersion;
        projectLinkLabel.Links.Remove(projectLinkLabel.Links[0]);
        projectLinkLabel.Links.Add(0, projectLinkLabel.Text.Length, "https://www.dugong.in/dugong-diagnostic-pro");
        licenseLinkLabel.Links.Remove(licenseLinkLabel.Links[0]);
        licenseLinkLabel.Links.Add(0, licenseLinkLabel.Text.Length, "mailto:Diagnostics@dugong.in");
        Theme.Current.Apply(this);
    }

    private void LinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
        try
        {
            Process.Start(new ProcessStartInfo(e.Link.LinkData.ToString()));
        }
        catch { }
    }
}
