using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoU1.utils
{
    internal static class ImprimirEnConsola
    {
        private static void ImprimirConColor(string texto, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(texto);
            Console.ResetColor();
        }
        public static void Error(string texto)
        {
            ImprimirConColor(texto, ConsoleColor.Red);
        }

        public static void Success(string texto)
        {
            ImprimirConColor(texto, ConsoleColor.Green);
        }

        public static void Warning(string texto)
        {
            ImprimirConColor(texto, ConsoleColor.Yellow);
        }

        public static void Info(string texto)
        {
            ImprimirConColor(texto, ConsoleColor.Cyan);
        }

        public static void ColorPersonalizado(string texto, ConsoleColor color)
        {
            ImprimirConColor(texto, color);
        }
    }
}
