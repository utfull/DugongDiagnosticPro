################################################################
# Install:
# * python.exe -m pip install pypiwin32
# * python.exe -m pip install wmi
#
# Usage:
# * !! Make sure Dugong Diagnostic Pro is running !!
# * python basicwmi.py
################################################################

import wmi

hwmon = wmi.WMI(namespace="root\DugongDiagnosticPro")
sensors = hwmon.Sensor(SensorType="Control")

for s in sensors:
    print(s)
