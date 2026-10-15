using System;

namespace ProyectoU1.modelo
{
    // Herencia: representa la compra de agua con su envase.
    internal class CompraBidon
        : OperacionAgua
    {
        // Constructor derivado: establece el precio fijo de S/ 35.
        public CompraBidon(Cliente cliente, int cantidad)
            : base(cliente, cantidad, 35m)
        {
        }

        // Sobrescritura del tipo de operacion.
        public override string ObtenerTipo() { return "Compra Bidon"; }
    }
}
