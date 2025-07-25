﻿// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at http://mozilla.org/MPL/2.0/.
// Copyright (C) DugongDiagnosticPro and Contributors.
// Original Copyright (C) LibreHardwareMonitor and Contributors.
// All Rights Reserved.

using System;
using HidSharp;

namespace DugongDiagnosticPro.Hardware.Controller.AquaComputer;
//TODO:
//Implement set RGB Controls

internal sealed class Farbwerk : Hardware
{
    private const int FEATURE_ID = 3;

    private const int HEADER_SIZE = 27;
    private const int SENSOR_OFFSET = 20;
    private const int COLORS_OFFSET = 40;

    private const int TEMPERATURE_COUNT = 4;
    private const int TEMP_SENSOR_DISCONNECTED = 32767;
    private const int COLOR_COUNT = 4;
    private const int COLOR_VALUE_COUNT = COLOR_COUNT * 3;
    private readonly HidDevice _device;
    private readonly byte[] _rawData = new byte[140];
    
    private readonly Sensor[] _temperatures = new Sensor[TEMPERATURE_COUNT];
    private readonly Sensor[] _colors = new Sensor[COLOR_VALUE_COUNT];

    public Farbwerk(HidDevice dev, ISettings settings) : base("Farbwerk", new Identifier(dev), settings)
    {
        _device = dev;

        for (int i = 0; i < _temperatures.Length; i++)
        {
            _temperatures[i] = new Sensor($"Temperature #{i + 1}", i, SensorType.Temperature, this, settings);
            ActivateSensor(_temperatures[i]);
        }

        for (int i = 0; i < _colors.Length; i++)
        {
            int control = (i / 3) + 1;
            string color = (i % 3) switch
            {
                0 => "Red",
                1 => "Green",
                2 => "Blue",
                _ => "Invalid"
            };
            _colors[i] = new Sensor($"Controller {control} {color}", COLOR_COUNT + i, SensorType.Level, this, settings);
            ActivateSensor(_colors[i]);
        }

        Update();
    }

    public ushort FirmwareVersion { get; private set; }

    public override HardwareType HardwareType
    {
        get { return HardwareType.EmbeddedController; }
    }

    public string Status
    {
        get
        {
            if (_rawData[0] != 0x1)
            {
                return $"Status: Invalid header {_rawData[0]}";
            }

            if (FirmwareVersion < 1009)
            {
                return $"Status: Untested Firmware Version {FirmwareVersion}! Please consider Updating to Version 1009";
            }

            return "Status: OK";
        }
    }

    public override void Close()
    {
        base.Close();
    }

    public override void Update()
    {
        if (_device.TryOpen(out HidStream stream)) {
            int length = stream.Read(_rawData);

            if (length != _rawData.Length || _rawData[0] != 0x1)
            {
                return;
            }

            FirmwareVersion = Convert.ToUInt16(_rawData[21] << 8 | _rawData[22]);

            int offset = HEADER_SIZE + SENSOR_OFFSET;
            foreach (Sensor temp in _temperatures)
            {
                temp.Value = (_rawData[offset] << 8 | _rawData[offset + 1]) / 100.0f;

                if (temp.Value == (TEMP_SENSOR_DISCONNECTED / 100.0f)) {
                    DeactivateSensor(temp);
                }
                
                offset += 2;
            }

            offset = HEADER_SIZE + COLORS_OFFSET;
            foreach (Sensor color in _colors)
            {
                color.Value = (_rawData[offset] << 8 | _rawData[offset + 1]) / 81.90f;
                offset += 2;
            }
        }
        
    }
}
