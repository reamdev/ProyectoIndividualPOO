using System;

namespace ProyectoU1.modelo
{
    // Herencia: Recarga es una especializacion de OperacionAgua.
    internal class Recarga : OperacionAgua
    {
        // Constructor derivado: establece el precio fijo de S/ 10.
        public Recarga(Cliente cliente, int cantidad)
            : base(cliente, cantidad, 10m)
        {
        }

        // Polimorfismo: una recarga recibe un envase vacio por unidad.
        public override int ObtenerVaciosRecibidos() { return Cantidad; }

        // Sobrescritura del tipo de operacion.
        public override string ObtenerTipo() { return "Recarga"; }
    }
}
