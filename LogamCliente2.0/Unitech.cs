using System.Runtime.InteropServices;
using System;

namespace Cliente
{
    /* CLASE LA CUAL PERMITE ACCEDER A LA DLL UnitechAPI.dll Y ACCERDER A SUS METODOS */
    public class Unitech
    {
        /* extern SE INCLUYE EN LA DECLARACION DE METODOS LOS CUALES SE IMPLEMENTAN FUERA DE LA APLICACION, EN ESTE CASO
         * RS232EventEnable AL SER UNA FUNCION ADMINISTRADA POR CODIGO NO GESTIONADO (DLL) SE DEBE ESPECIFICAR EL OPERADOR extern
           PARA QUE SE BUSQUE LA IMPLEMENTACION DE ESTE METODO EN LA DLL ATERIORMENTE NOMBRADA */
        [DllImport("UnitechAPI.dll", EntryPoint = "RS232EventEnable")]
        public static extern bool RS232EventEnable(string FilePath);

        [DllImport("UnitechAPI.dll", EntryPoint = "RS232EventDisable")]
        public static extern bool RS232EventDisable();

        [DllImport("UnitechAPI.dll", EntryPoint = "Suspend")]
        public static extern bool Suspend();

        [DllImport("UnitechAPI.dll", EntryPoint = "DisableTaskbar")]
        public static extern bool DisableTaskbar();

        [DllImport("UnitechAPI.dll", EntryPoint = "EnableTaskbar")]
        public static extern bool EnableTaskbar();

        [DllImport("UnitechAPI.dll", EntryPoint = "DisableDesktop")]
        public static extern bool DisableDesktop();

        [DllImport("UnitechAPI.dll", EntryPoint = "EnableDesktop")]
        public static extern bool EnableDesktop();

        [DllImport("UnitechAPI.dll", EntryPoint = "DisableExploreToolbar")]
        public static extern bool DisableExploreToolbar();

        [DllImport("UnitechAPI.dll", EntryPoint = "EnableExploreToolbar")]
        public static extern bool EnableExploreToolbar();
    }
}
