using System;

namespace Zeno
{
    internal partial class Program
    {
        private static void OnProcessExit(object sender, EventArgs e)
        {
            Print("Zeno is gone");
            _cts.Cancel(); // Asegurar que las tareas en ejecución se detengan
        }
    }
}
