using ProyectoU1.gestion;
using ProyectoU1.modelo;
using System;

namespace ProyectoU1;

class Program
{
    public static void Main(string[] args)
    {
        InventarioBidones inventario = new InventarioBidones(20, 5);
        Distribuidora distribuidora = new Distribuidora(inventario);

        ConsoleProgram app = new ConsoleProgram(distribuidora);
        app.Run();
    }
}
