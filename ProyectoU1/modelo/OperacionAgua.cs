using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoU1.modelo
{
    internal class OperacionAgua
    {
        private Cliente cliente;
        private int cantidad;
        private decimal precioUnitario;
        private DateTime fecha;

        public Cliente Cliente { get { return cliente; } }
        public int Cantidad { get { return cantidad; } }
        public decimal PrecioUnitario { get { return precioUnitario; } }
        public DateTime Fecha { get { return fecha; } }

        public OperacionAgua(Cliente cliente, int cantidad, decimal precioUnitario)
        {
            if (cliente == null) throw new ArgumentNullException(nameof(cliente));
            if (cantidad <= 0) throw new ArgumentException("Cantidad Invalida");
            if (precioUnitario <= 0) throw new ArgumentException("Precio Unitario Invalido");

            this.cliente = cliente;
            this.cantidad = cantidad;
            this.precioUnitario = precioUnitario;
            this.fecha = DateTime.Now;
        }

        public decimal CalcularTotal() { return this.cantidad * this.precioUnitario; }

        public virtual int ObtenerVaciosRecibidos() { return 0; }

        public virtual string ObtenerTipo() { return "Operacion de agua"; }
    }
}
