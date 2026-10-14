using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoU1.modelo
{
    internal class Recarga : OperacionAgua
    {
        public Recarga(Cliente cliente, int cantidad)
            : base(cliente, cantidad, 10m)
        {
        }

        public override int ObtenerVaciosRecibidos() { return Cantidad; }

        public override string ObtenerTipo() { return "Recarga"; }
    }
}
