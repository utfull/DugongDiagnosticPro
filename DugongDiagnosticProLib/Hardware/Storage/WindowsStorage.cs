﻿// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at http://mozilla.org/MPL/2.0/.
// Copyright (C) DugongDiagnosticPro and Contributors.
// Original Copyright (C) LibreHardwareMonitor and Contributors.
// All Rights Reserved.

using System;
using System.Collections.Generic;
using System.Management;
using System.Runtime.InteropServices;
using DugongDiagnosticPro.Interop;
using Microsoft.Win32.SafeHandles;

namespace DugongDiagnosticPro.Hardware.Storage;

internal static class WindowsStorage
{
    public static Storage.StorageInfo GetStorageInfo(string deviceId, uint driveIndex)
    {
        using SafeFileHandle handle = Kernel32.OpenDevice(deviceId);

        if (handle?.IsInvalid != false)
            return null;

        var query = new Kernel32.STORAGE_PROPERTY_QUERY { PropertyId = Kernel32.STORAGE_PROPERTY_ID.StorageDeviceProperty, QueryType = Kernel32.STORAGE_QUERY_TYPE.PropertyStandardQuery };

        if (!Kernel32.DeviceIoControl(handle,
                                      Kernel32.IOCTL.IOCTL_STORAGE_QUERY_PROPERTY,
                                      ref query,
                                      Marshal.SizeOf(query),
                                      out Kernel32.STORAGE_DEVICE_DESCRIPTOR_HEADER header,
                                      Marshal.SizeOf<Kernel32.STORAGE_DEVICE_DESCRIPTOR_HEADER>(),
                                      out _,
                                      IntPtr.Zero))
        {
            return null;
        }

        IntPtr descriptorPtr = Marshal.AllocHGlobal((int)header.Size);

        try
        {
            return Kernel32.DeviceIoControl(handle, Kernel32.IOCTL.IOCTL_STORAGE_QUERY_PROPERTY, ref query, Marshal.SizeOf(query), descriptorPtr, header.Size, out uint bytesReturned, IntPtr.Zero)
                ? new StorageInfo((int)driveIndex, descriptorPtr)
                : null;
        }
        finally
        {
            Marshal.FreeHGlobal(descriptorPtr);
        }
    }

    public static string[] GetLogicalDrives(int driveIndex)
    {
        var list = new List<string>();

        try
        {
            using var s = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskPartition " + "WHERE DiskIndex = " + driveIndex);

            foreach (ManagementBaseObject o in s.Get())
            {
                if (o is ManagementObject dp)
                {
                    foreach (ManagementBaseObject ld in dp.GetRelated("Win32_LogicalDisk"))
                        list.Add(((string)ld["Name"]).TrimEnd(':'));
                }
            }
        }
        catch
        {
            // Ignored.
        }

        return list.ToArray();
    }

    private class StorageInfo : Storage.StorageInfo
    {
        public StorageInfo(int index, IntPtr descriptorPtr)
        {
            Kernel32.STORAGE_DEVICE_DESCRIPTOR descriptor = Marshal.PtrToStructure<Kernel32.STORAGE_DEVICE_DESCRIPTOR>(descriptorPtr);
            Index = index;
            Vendor = GetString(descriptorPtr, descriptor.VendorIdOffset, descriptor.Size);
            Product = GetString(descriptorPtr, descriptor.ProductIdOffset, descriptor.Size);
            Revision = GetString(descriptorPtr, descriptor.ProductRevisionOffset, descriptor.Size);
            Serial = GetString(descriptorPtr, descriptor.SerialNumberOffset, descriptor.Size);
            BusType = descriptor.BusType;
            Removable = descriptor.RemovableMedia;
            RawData = new byte[descriptor.Size];
            Marshal.Copy(descriptorPtr, RawData, 0, RawData.Length);
        }

        private static string GetString(IntPtr descriptorPtr, uint offset, uint size)
        {
            return offset > 0 && offset < size ? Marshal.PtrToStringAnsi(IntPtr.Add(descriptorPtr, (int)offset))?.Trim() : string.Empty;
        }
    }
}
