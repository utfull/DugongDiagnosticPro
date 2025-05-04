// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at http://mozilla.org/MPL/2.0/.
// Copyright (C) DugongDiagnosticPro and Contributors.
// Original Copyright (C) LibreHardwareMonitor and Contributors.
// Partial Copyright (C) Michael Möller <mmoeller@openhardwaremonitor.org> and Contributors.
// All Rights Reserved.

using System;
using System.IO;
using System.Windows.Forms;
using DugongDiagnosticPro.UI;
using Microsoft.Win32;

namespace DugongDiagnosticPro;

public static class Program
{
    // Set this to true when testing on Windows, set to false for production
    private static readonly bool enableWindowsTest = true;
    
    [STAThread]
    public static void Main()
    {
        if (!AllRequiredFilesAvailable())
            Environment.Exit(0);
            
        if (!IsDugongSystem())
        {
            MessageBox.Show("Dugong Diagnostic Pro only supports Dugong systems.",
                           "Unsupported System",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error);
            Environment.Exit(0);
        }

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        using (MainForm form = new MainForm())
        {
            form.FormClosed += delegate
            {
                Application.Exit();
            };
            Application.Run();
        }
    }

    private static bool IsFileAvailable(string fileName)
    {
        string path = Path.GetDirectoryName(Application.ExecutablePath) + Path.DirectorySeparatorChar;
        if (!File.Exists(path + fileName))
        {
            MessageBox.Show("The following file could not be found: " + fileName +
                            "\nPlease extract all files from the archive.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        return true;
    }

    private static bool AllRequiredFilesAvailable()
    {
        if (!IsFileAvailable("Aga.Controls.dll"))
            return false;

        if (!IsFileAvailable("DugongDiagnosticProLib.dll"))
            return false;

        if (!IsFileAvailable("OxyPlot.dll"))
            return false;

        if (!IsFileAvailable("OxyPlot.WindowsForms.dll"))
            return false;

        return true;
    }
    
    private static bool IsDugongSystem()
    {
        // For testing on Windows, return true if enableWindowsTest is true
        if (enableWindowsTest)
            return true;
            
        try
        {
            // Check common registry locations for the "dugong" keyword
            string[] registryPaths = new string[]
            {
                @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\OEMInformation",
                @"HKEY_LOCAL_MACHINE\HARDWARE\DESCRIPTION\System\BIOS",
                @"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SystemInformation"
            };
            
            foreach (string path in registryPaths)
            {
                RegistryKey key = null;
                
                if (path.StartsWith("HKEY_LOCAL_MACHINE"))
                    key = Registry.LocalMachine.OpenSubKey(path.Substring(19));
                else if (path.StartsWith("HKEY_CURRENT_USER"))
                    key = Registry.CurrentUser.OpenSubKey(path.Substring(18));
                
                if (key != null)
                {
                    foreach (string valueName in key.GetValueNames())
                    {
                        object value = key.GetValue(valueName);
                        if (value != null && value.ToString().ToLower().Contains("dugong"))
                            return true;
                    }
                    key.Close();
                }
            }
            
            return false;
        }
        catch (Exception)
        {
            // If there's an error accessing the registry, don't allow the app to run
            return false;
        }
    }
}
