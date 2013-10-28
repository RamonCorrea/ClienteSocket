using System;
using System.Runtime.InteropServices;

namespace Cliente
{
    class RelayAccess
    {
        [DllImport("UnitechAPI.dll", EntryPoint = "RS232EventEnable")]
        public static extern bool RS232EventEnable(string FilePath);

        [DllImport("SysIOAPI.dll", EntryPoint="SetCameraLED")]
        public static extern bool SetCameraLED(int Camara, bool PCMCIAStatus);
    }
}
