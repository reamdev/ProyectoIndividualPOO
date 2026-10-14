using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoU1.modelo
{
    internal class Cliente
    {
        private string nombre;

        public string Nombre { get { return nombre; } }

        public Cliente(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentNullException("Nombre del Cliente invalido");

            this.nombre = nombre.Trim();
        }
    }
}
