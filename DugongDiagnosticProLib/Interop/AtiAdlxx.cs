// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at http://mozilla.org/MPL/2.0/.
// Copyright (C) DugongDiagnosticPro and Contributors.
// Original Copyright (C) LibreHardwareMonitor and Contributors.
// All Rights Reserved.

using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

// ReSharper disable InconsistentNaming

namespace DugongDiagnosticPro.Interop;

internal static class AtiAdlxx
{
    public const int ADL_DL_FANCTRL_FLAG_USER_DEFINED_SPEED = 1;

    public const int ADL_DL_FANCTRL_SPEED_TYPE_PERCENT = 1;
    public const int ADL_DL_FANCTRL_SPEED_TYPE_RPM = 2;

    public const int ADL_DL_FANCTRL_SUPPORTS_PERCENT_READ = 1;
    public const int ADL_DL_FANCTRL_SUPPORTS_PERCENT_WRITE = 2;
    public const int ADL_DL_FANCTRL_SUPPORTS_RPM_READ = 4;
    public const int ADL_DL_FANCTRL_SUPPORTS_RPM_WRITE = 8;

    public const int ADL_DRIVER_OK = 0;
    public const int ADL_FALSE = 0;

    public const int ADL_MAX_ADAPTERS = 40;
    public const int ADL_MAX_DEVICENAME = 32;
    public const int ADL_MAX_DISPLAYS = 40;
    public const int ADL_MAX_GLSYNC_PORT_LEDS = 8;
    public const int ADL_MAX_GLSYNC_PORTS = 8;
    public const int ADL_MAX_NUM_DISPLAYMODES = 1024;
    public const int ADL_MAX_PATH = 256;
    public const int ADL_TRUE = 1;

    public const int ATI_VENDOR_ID = 0x1002;

    internal const int ADL_PMLOG_MAX_SENSORS = 256;

    internal const string DllName = "atiadlxx.dll";

    public static Context Context_Alloc = Marshal.AllocHGlobal;

    // create a Main_Memory_Alloc delegate and keep it alive
    public static ADL_Main_Memory_AllocDelegate Main_Memory_Alloc = Marshal.AllocHGlobal;

    public delegate IntPtr ADL_Main_Memory_AllocDelegate(int size);

    public delegate IntPtr Context(int size);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Overdrive5_ODParameters_Get(IntPtr context, int adapterIndex, out ADLODParameters parameters);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Overdrive5_CurrentActivity_Get(IntPtr context, int iAdapterIndex, ref ADLPMActivity activity);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Overdrive5_Temperature_Get(IntPtr context, int adapterIndex, int thermalControllerIndex, ref ADLTemperature temperature);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_OverdriveN_Temperature_Get(IntPtr context, int adapterIndex, ADLODNTemperatureType iTemperatureType, ref int temp);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Overdrive5_FanSpeed_Get(IntPtr context, int adapterIndex, int thermalControllerIndex, ref ADLFanSpeedValue fanSpeedValue);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Overdrive5_FanSpeedInfo_Get(IntPtr context, int adapterIndex, int thermalControllerIndex, ref ADLFanSpeedInfo fanSpeedInfo);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Overdrive5_FanSpeedToDefault_Set(IntPtr context, int adapterIndex, int thermalControllerIndex);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Overdrive5_FanSpeed_Set(IntPtr context, int adapterIndex, int thermalControllerIndex, ref ADLFanSpeedValue fanSpeedValue);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_OverdriveN_PerformanceStatus_Get(IntPtr context, int adapterIndex, out ADLODNPerformanceStatus performanceStatus);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Overdrive_Caps(IntPtr context, int adapterIndex, ref int supported, ref int enabled, ref int version);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Overdrive6_Capabilities_Get(IntPtr context, int adapterIndex, ref ADLOD6Capabilities lpODCapabilities);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Overdrive6_CurrentPower_Get(IntPtr context, int adapterIndex, ADLODNCurrentPowerType powerType, ref int currentValue);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Main_Control_Create(ADL_Main_Memory_AllocDelegate callback, int connectedAdapters, ref IntPtr context);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Main_Control_Destroy(IntPtr context);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_New_QueryPMLogData_Get(IntPtr context, int adapterIndex, ref ADLPMLogDataOutput aDLPMLogDataOutput);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Adapter_FrameMetrics_Caps(IntPtr context, int adapterIndex, ref int supported);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Adapter_FrameMetrics_Get(IntPtr context, int adapterIndex, int displayIndex, ref float fps);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Adapter_FrameMetrics_Start(IntPtr context, int adapterIndex, int displayIndex);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Adapter_FrameMetrics_Stop(IntPtr context, int adapterIndex, int displayIndex);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Adapter_PMLog_Support_Get(IntPtr context, int adapterIndex,
                                                                  ref ADLPMLogSupportInfo pPMLogSupportInfo);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Adapter_PMLog_Start(IntPtr context, int adapterIndex,
                                                            ref ADLPMLogStartInput pPMLogStartInput,
                                                            ref ADLPMLogStartOutput pPMLogStartOutput,
                                                            uint device);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Adapter_PMLog_Stop(IntPtr context, int adapterIndex, uint device);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Device_PMLog_Device_Create(IntPtr context, int adapterIndex, ref uint device);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Device_PMLog_Device_Destroy(IntPtr context, uint device);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_GcnAsicInfo_Get(IntPtr context, int adapterIndex, ref ADLGcnInfo gcnInfo);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Adapter_NumberOfAdapters_Get(IntPtr context, ref int numAdapters);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Adapter_AdapterInfo_Get(IntPtr context, IntPtr adapterInfo, int size);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Adapter_ID_Get(IntPtr context, int adapterIndex, out int adapterId);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Adapter_Active_Get(IntPtr context, int adapterIndex, out int status);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Adapter_DedicatedVRAMUsage_Get(IntPtr context, int adapterIndex, out int iVRAMUsageInMB);

    [DllImport(DllName, CallingConvention = CallingConvention.Cdecl)]
    [DefaultDllImportSearchPaths(DllImportSearchPath.System32)]
    public static extern ADLStatus ADL2_Adapter_MemoryInfoX4_Get(IntPtr context, int adapterIndex, out ADLMemoryInfoX4 memoryInfo);

    public static bool ADL_Method_Exists(string ADL_Method)
    {
        IntPtr module = Kernel32.LoadLibrary(DllName);
        if (module != IntPtr.Zero)
        {
            bool result = Kernel32.GetProcAddress(module, ADL_Method) != IntPtr.Zero;
            Kernel32.FreeLibrary(module);
            return result;
        }

        return false;
    }

    public static ADLStatus ADL2_Adapter_AdapterInfo_Get(ref IntPtr context, ADLAdapterInfo[] info)
    {
        int elementSize = Marshal.SizeOf(typeof(ADLAdapterInfo));
        int size = info.Length * elementSize;
        IntPtr ptr = Marshal.AllocHGlobal(size);
        ADLStatus result = ADL2_Adapter_AdapterInfo_Get(context, ptr, size);
        for (int i = 0; i < info.Length; i++)
            info[i] = (ADLAdapterInfo)Marshal.PtrToStructure((IntPtr)((long)ptr + (i * elementSize)), typeof(ADLAdapterInfo));

        Marshal.FreeHGlobal(ptr);

        // the ADLAdapterInfo.VendorID field reported by ADL is wrong on
        // Windows systems (parse error), so we fix this here
        for (int i = 0; i < info.Length; i++)
        {
            // try Windows UDID format
            Match m = Regex.Match(info[i].UDID, "PCI_VEN_([A-Fa-f0-9]{1,4})&.*");
            if (m.Success && m.Groups.Count == 2)
            {
                info[i].VendorID = Convert.ToInt32(m.Groups[1].Value, 16);
                continue;
            }

            // if above failed, try Unix UDID format
            m = Regex.Match(info[i].UDID, "[0-9]+:[0-9]+:([0-9]+):[0-9]+:[0-9]+");
            if (m.Success && m.Groups.Count == 2)
            {
                info[i].VendorID = Convert.ToInt32(m.Groups[1].Value, 10);
            }
        }

        return result;
    }

    public static bool UsePmLogForFamily(int familyId)
    {
        return familyId >= (int)GCNFamilies.FAMILY_AI;
    }

    internal enum ADLStatus
    {
        /// <summary>
        /// All OK, but need to wait.
        /// </summary>
        ADL_OK_WAIT = 4,

        /// <summary>
        /// All OK, but need restart.
        /// </summary>
        ADL_OK_RESTART = 3,

        /// <summary>
        /// All OK but need mode change.
        /// </summary>
        ADL_OK_MODE_CHANGE = 2,

        /// <summary>
        /// All OK, but with warning.
        /// </summary>
        ADL_OK_WARNING = 1,

        /// <summary>
        /// ADL function completed successfully.
        /// </summary>
        ADL_OK = 0,

        /// <summary>
        /// Generic Error. Most likely one or more of the Escape calls to the driver
        /// failed!
        /// </summary>
        ADL_ERR = -1,

        /// <summary>
        /// ADL not initialized.
        /// </summary>
        ADL_ERR_NOT_INIT = -2,

        /// <summary>
        /// One of the parameter passed is invalid.
        /// </summary>
        ADL_ERR_INVALID_PARAM = -3,

        /// <summary>
        /// One of the parameter size is invalid.
        /// </summary>
        ADL_ERR_INVALID_PARAM_SIZE = -4,

        /// <summary>
        /// Invalid ADL index passed.
        /// </summary>
        ADL_ERR_INVALID_ADL_IDX = -5,

        /// <summary>
        /// Invalid controller index passed.
        /// </summary>
        ADL_ERR_INVALID_CONTROLLER_IDX = -6,

        /// <summary>
        /// Invalid display index passed.
        /// </summary>
        ADL_ERR_INVALID_DIPLAY_IDX = -7,

        /// <summary>
        /// Function not supported by the driver.
        /// </summary>
        ADL_ERR_NOT_SUPPORTED = -8,

        /// <summary>
        /// Null Pointer error.
        /// </summary>
        ADL_ERR_NULL_POINTER = -9,

        /// <summary>
        /// Call can't be made due to disabled adapter.
        /// </summary>
        ADL_ERR_DISABLED_ADAPTER = -10,

        /// <summary>
        /// Invalid Callback.
        /// </summary>
        ADL_ERR_INVALID_CALLBACK = -11,

        /// <summary>
        /// Display Resource conflict.
        /// </summary>
        ADL_ERR_RESOURCE_CONFLICT = -12,

        /// <summary>
        /// Failed to update some of the values. Can be returned by set request that
        /// include multiple values if not all values were successfully committed.
        /// </summary>
        ADL_ERR_SET_INCOMPLETE = -20,

        /// <summary>
        /// There's no Linux XDisplay in Linux Console environment.
        /// </summary>
        ADL_ERR_NO_XDISPLAY = -21
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct ADLAdapterInfo
    {
        public int Size;
        public int AdapterIndex;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ADL_MAX_PATH)]
        public string UDID;

        public int BusNumber;
        public int DeviceNumber;
        public int FunctionNumber;
        public int VendorID;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ADL_MAX_PATH)]
        public string AdapterName;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ADL_MAX_PATH)]
        public string DisplayName;

        public int Present;
        public int Exist;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ADL_MAX_PATH)]
        public string DriverPath;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ADL_MAX_PATH)]
        public string DriverPathExt;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ADL_MAX_PATH)]
        public string PNPString;

        public int OSDisplayIndex;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct ADLODParameterRange
    {
        public int iMin;
        public int iMax;
        public int iStep;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct ADLODParameters
    {
        public int iSize;
        public int iNumberOfPerformanceLevels;
        public int iActivityReportingSupported;
        public int iDiscretePerformanceLevels;
        public int iReserved;
        public ADLODParameterRange sEngineClock;
        public ADLODParameterRange sMemoryClock;
        public ADLODParameterRange sVddc;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct ADLOD6Capabilities
    {
        public int iCapabilities;
        public int iSupportedStates;
        public int iNumberOfPerformanceLevels;
        public ADLODParameterRange sEngineClockRange;
        public ADLODParameterRange sMemoryClockRange;
        public int iExtValue;
        public int iExtMask;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct ADLPMActivity
    {
        public int iSize;
        public int iEngineClock;
        public int iMemoryClock;
        public int iVddc;
        public int iActivityPercent;
        public int iCurrentPerformanceLevel;
        public int iCurrentBusSpeed;
        public int iCurrentBusLanes;
        public int iMaximumBusLanes;
        public int iReserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct ADLTemperature
    {
        public int iSize;
        public int iTemperature;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct ADLFanSpeedValue
    {
        public int iSize;
        public int iSpeedType;
        public int iFanSpeed;
        public int iFlags;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct ADLFanSpeedInfo
    {
        public int iSize;
        public int iFlags;
        public int iMinPercent;
        public int iMaxPercent;
        public int iMinRPM;
        public int iMaxRPM;
    }

    internal enum ADLODNCurrentPowerType
    {
        ODN_GPU_TOTAL_POWER = 0,
        ODN_GPU_PPT_POWER,
        ODN_GPU_SOCKET_POWER,
        ODN_GPU_CHIP_POWER
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct ADLVersionsInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string DriverVer;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string CatalystVersion;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
        public string CatalystWebLink;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct ADLODNPerformanceStatus
    {
        public int iCoreClock;
        public int iMemoryClock;
        public int iDCEFClock;
        public int iGFXClock;
        public int iUVDClock;
        public int iVCEClock;
        public int iGPUActivityPercent;
        public int iCurrentCorePerformanceLevel;
        public int iCurrentMemoryPerformanceLevel;
        public int iCurrentDCEFPerformanceLevel;
        public int iCurrentGFXPerformanceLevel;
        public int iUVDPerformanceLevel;
        public int iVCEPerformanceLevel;
        public int iCurrentBusSpeed;
        public int iCurrentBusLanes;
        public int iMaximumBusLanes;
        public int iVDDC;
        public int iVDDCI;
    }

    internal enum ADLODNTemperatureType
    {
        // This typed is named like this in the documentation but for some reason AMD failed to include it...
        // Yet it seems these correspond with ADL_PMLOG_TEMPERATURE_xxx.
        EDGE = 1,
        MEM = 2,
        VRVDDC = 3,
        VRMVDD = 4,
        LIQUID = 5,
        PLX = 6,
        HOTSPOT = 7
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct ADLSingleSensorData
    {
        public int supported;
        public int value;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct ADLPMLogDataOutput
    {
        public int size;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = ADL_PMLOG_MAX_SENSORS)]
        public ADLSingleSensorData[] sensors;
    }

    internal enum ADLPMLogSensors
    {
        ADL_SENSOR_MAXTYPES             = 0,
        ADL_PMLOG_CLK_GFXCLK            = 1,
        ADL_PMLOG_CLK_MEMCLK            = 2,
        ADL_PMLOG_CLK_SOCCLK            = 3,
        ADL_PMLOG_CLK_UVDCLK1           = 4,
        ADL_PMLOG_CLK_UVDCLK2           = 5,
        ADL_PMLOG_CLK_VCECLK            = 6,
        ADL_PMLOG_CLK_VCNCLK            = 7,
        ADL_PMLOG_TEMPERATURE_EDGE      = 8,
        ADL_PMLOG_TEMPERATURE_MEM       = 9,
        ADL_PMLOG_TEMPERATURE_VRVDDC    = 10,
        ADL_PMLOG_TEMPERATURE_VRMVDD    = 11,
        ADL_PMLOG_TEMPERATURE_LIQUID    = 12,
        ADL_PMLOG_TEMPERATURE_PLX       = 13,
        ADL_PMLOG_FAN_RPM               = 14,
        ADL_PMLOG_FAN_PERCENTAGE        = 15,
        ADL_PMLOG_SOC_VOLTAGE           = 16,
        ADL_PMLOG_SOC_POWER             = 17,
        ADL_PMLOG_SOC_CURRENT           = 18,
        ADL_PMLOG_INFO_ACTIVITY_GFX     = 19,
        ADL_PMLOG_INFO_ACTIVITY_MEM     = 20,
        ADL_PMLOG_GFX_VOLTAGE           = 21,
        ADL_PMLOG_MEM_VOLTAGE           = 22,
        ADL_PMLOG_ASIC_POWER            = 23,
        ADL_PMLOG_TEMPERATURE_VRSOC     = 24,
        ADL_PMLOG_TEMPERATURE_VRMVDD0   = 25,
        ADL_PMLOG_TEMPERATURE_VRMVDD1   = 26,
        ADL_PMLOG_TEMPERATURE_HOTSPOT   = 27,
        ADL_PMLOG_TEMPERATURE_GFX       = 28,
        ADL_PMLOG_TEMPERATURE_SOC       = 29,
        ADL_PMLOG_GFX_POWER             = 30,
        ADL_PMLOG_GFX_CURRENT           = 31,
        ADL_PMLOG_TEMPERATURE_CPU       = 32,
        ADL_PMLOG_CPU_POWER             = 33,
        ADL_PMLOG_CLK_CPUCLK            = 34,
        ADL_PMLOG_THROTTLER_STATUS      = 35,   // GFX
        ADL_PMLOG_CLK_VCN1CLK1          = 36,
        ADL_PMLOG_CLK_VCN1CLK2          = 37,
        ADL_PMLOG_SMART_POWERSHIFT_CPU  = 38,
        ADL_PMLOG_SMART_POWERSHIFT_DGPU = 39,
        ADL_PMLOG_BUS_SPEED             = 40,
        ADL_PMLOG_BUS_LANES             = 41,
        ADL_PMLOG_TEMPERATURE_LIQUID0   = 42,
        ADL_PMLOG_TEMPERATURE_LIQUID1   = 43,
        ADL_PMLOG_CLK_FCLK              = 44,
        ADL_PMLOG_THROTTLER_STATUS_CPU  = 45,
        ADL_PMLOG_SSPAIRED_ASICPOWER    = 46, // apuPower
        ADL_PMLOG_SSTOTAL_POWERLIMIT    = 47, // Total Power limit
        ADL_PMLOG_SSAPU_POWERLIMIT      = 48, // APU Power limit
        ADL_PMLOG_SSDGPU_POWERLIMIT     = 49, // DGPU Power limit
        ADL_PMLOG_TEMPERATURE_HOTSPOT_GCD      = 50,
        ADL_PMLOG_TEMPERATURE_HOTSPOT_MCD      = 51,
        ADL_PMLOG_THROTTLER_TEMP_EDGE_PERCENTAGE        = 52,
        ADL_PMLOG_THROTTLER_TEMP_HOTSPOT_PERCENTAGE     = 53,
        ADL_PMLOG_THROTTLER_TEMP_HOTSPOT_GCD_PERCENTAGE = 54,
        ADL_PMLOG_THROTTLER_TEMP_HOTSPOT_MCD_PERCENTAGE = 55,
        ADL_PMLOG_THROTTLER_TEMP_MEM_PERCENTAGE     = 56,
        ADL_PMLOG_THROTTLER_TEMP_VR_GFX_PERCENTAGE  = 57,
        ADL_PMLOG_THROTTLER_TEMP_VR_MEM0_PERCENTAGE = 58,
        ADL_PMLOG_THROTTLER_TEMP_VR_MEM1_PERCENTAGE = 59,
        ADL_PMLOG_THROTTLER_TEMP_VR_SOC_PERCENTAGE  = 60,
        ADL_PMLOG_THROTTLER_TEMP_LIQUID0_PERCENTAGE = 61,
        ADL_PMLOG_THROTTLER_TEMP_LIQUID1_PERCENTAGE = 62,
        ADL_PMLOG_THROTTLER_TEMP_PLX_PERCENTAGE = 63,
        ADL_PMLOG_THROTTLER_TDC_GFX_PERCENTAGE  = 64,
        ADL_PMLOG_THROTTLER_TDC_SOC_PERCENTAGE  = 65,
        ADL_PMLOG_THROTTLER_TDC_USR_PERCENTAGE  = 66,
        ADL_PMLOG_THROTTLER_PPT0_PERCENTAGE     = 67,
        ADL_PMLOG_THROTTLER_PPT1_PERCENTAGE     = 68,
        ADL_PMLOG_THROTTLER_PPT2_PERCENTAGE     = 69,
        ADL_PMLOG_THROTTLER_PPT3_PERCENTAGE     = 70,
        ADL_PMLOG_THROTTLER_FIT_PERCENTAGE           = 71,
        ADL_PMLOG_THROTTLER_GFX_APCC_PLUS_PERCENTAGE = 72,
        ADL_PMLOG_BOARD_POWER                        = 73,
        ADL_PMLOG_MAX_SENSORS_REAL
    }

    internal enum GCNFamilies
    {
        FAMILY_UNKNOWN = 0,
        FAMILY_TN = 105, // Trinity APUs
        FAMILY_SI = 110, // Southern Islands: Tahiti, Pitcairn, CapeVerde, Oland, Hainan
        FAMILY_CI = 120, // Sea Islands: Bonaire, Hawaii
        FAMILY_KV = 125, // Kaveri, Kabini, Mullins
        FAMILY_VI = 130, // Volcanic Islands: Iceland, Tonga, Fiji
        FAMILY_CZ = 135, // Carrizo APUs: Carrizo, Stoney
        FAMILY_AI = 141, // Vega: 10, 20
        FAMILY_RV = 142, // Raven (Vega GCN 5.0)
        FAMILY_NV = 143, // Navi10, Navi2x
        FAMILY_VGH = 144, // Van Gogh (RDNA 2.0)
        FAMILY_NV3 = 145, // Navi: 3x (GC 11.0.0, RDNA 3.0)
        FAMILY_YC = 146, // Rembrandt (Yellow Carp, RDNA 2.0)
        FAMILY_GC_11_0_1 = 148, // Phoenix (GC 11.0.1, RDNA 3.0)
        FAMILY_GC_10_3_6 = 149, // Raphael (GC 10.3.6, RDNA 2.0)
        FAMILY_GC_11_5_0 = 150, // GC 11.5.0
        FAMILY_GC_10_3_7 = 151, // Mendocino (GC 10.3.7, RDNA 2.0)
    }

    //Structure containing information related power management logging.
    [StructLayout(LayoutKind.Sequential)]
    internal struct ADLPMLogSupportInfo
    {
        /// list of sensors defined by ADL_PMLOG_SENSORS
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = ADL_PMLOG_MAX_SENSORS)]
        public ushort[] usSensors;

        /// Reserved
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public int[] iReserved;
    }

    //Structure containing information to start power management logging.
    [StructLayout(LayoutKind.Sequential)]
    internal struct ADLPMLogStartInput
    {
        /// list of sensors defined by ADL_PMLOG_SENSORS
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = ADL_PMLOG_MAX_SENSORS)]
        public ushort[] usSensors;

        /// Sample rate in milliseconds
        public uint ulSampleRate;

        /// Reserved
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
        public int[] iReserved;
    }

    //Structure containing information to start power management logging.
    [StructLayout(LayoutKind.Explicit)]
    internal struct ADLPMLogStartOutput
    {
        /// Pointer to memory address containing logging data
        [FieldOffset(0)] public IntPtr pLoggingAddress;
        [FieldOffset(0)] public ulong ptrLoggingAddress;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct ADLPMLogData
    {
        /// Structure version
        public uint ulVersion;

        /// Current driver sample rate
        public uint ulActiveSampleRate;

        /// Timestamp of last update
        public ulong ulLastUpdated;

        // 2D array of sensor and values -- unsigned int ulValues[ADL_PMLOG_MAX_SUPPORTED_SENSORS][2]
        // the nested array will be accessed like a single dimension array
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = ADL_PMLOG_MAX_SENSORS*2)]
        public uint[] ulValues;

        /// Reserved
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public uint[] ulReserved;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct ADLGcnInfo
    {
        public int CuCount; //Number of compute units on the ASIC.
        public int TexCount; //Number of texture mapping units.
        public int RopCount; //Number of Render backend Units.

        // see GCNFamilies enum, references:
        //        https://gitlab.freedesktop.org/mesa/mesa/-/blob/main/src/amd/addrlib/src/amdgpu_asic_addr.h
        //        https://github.com/torvalds/linux/blob/master/include/uapi/drm/amdgpu_drm.h
        public int ASICFamilyId;
        public int ASICRevisionId;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct ADLMemoryInfoX4
    {
        /// Memory size in bytes.
        public long iMemorySize;
        /// Memory type in string.
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = ADL_MAX_PATH)]
        public string strMemoryType;
        /// Highest default performance level Memory bandwidth in Mbytes/s
        public long iMemoryBandwidth;
        /// HyperMemory size in bytes.
        public long iHyperMemorySize;
        /// Invisible Memory size in bytes.
        public long iInvisibleMemorySize;
        /// Visible Memory size in bytes.
        public long iVisibleMemorySize;
        /// Vram vendor ID
        public long iVramVendorRevId;
        /// Memory Bandiwidth that is calculated and finalized on the driver side, grab and go.
        public long iMemoryBandwidthX2;
        /// Memory Bit Rate that is calculated and finalized on the driver side, grab and go.
        public long iMemoryBitRateX2;
    }
}
