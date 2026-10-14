using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoU1.modelo
{
    internal class CompraBidon
        : OperacionAgua
    {
        public CompraBidon(Cliente cliente, int cantidad)
            : base(cliente, cantidad, 35m)
        {
        }

        public override string ObtenerTipo() { return "Compra Bidon"; }
    }
}
