using System;

namespace ProyectoU1.modelo
{
    // Clase base: representa los datos comunes de las operaciones de agua.
    // Herencia: Recarga y CompraBidon derivan de esta clase.
    internal class OperacionAgua
    {
        // Atributos privados: encapsulamiento de los datos de la operacion.
        private Cliente cliente;
        private int cantidad;
        private decimal precioUnitario;
        private DateTime fecha;

        // Propiedades de solo lectura para consultar la operacion.
        public Cliente Cliente { get { return cliente; } }
        public int Cantidad { get { return cantidad; } }
        public decimal PrecioUnitario { get { return precioUnitario; } }
        public DateTime Fecha { get { return fecha; } }

        // Constructor: valida cliente, cantidad y precio unitario.
        public OperacionAgua(Cliente cliente, int cantidad, decimal precioUnitario)
        {
            if (cliente == null) throw new ArgumentNullException(nameof(cliente));
            if (cantidad <= 0) throw new ArgumentException("Cantidad Invalida");
            if (precioUnitario <= 0) throw new ArgumentException("Precio Unitario Invalido");

            this.cliente = cliente;
            this.cantidad = cantidad;
            this.precioUnitario = precioUnitario;
            fecha = DateTime.Now;
        }

        // Metodo comun: calcula el importe de cualquier operacion.
        public decimal CalcularTotal() { return this.cantidad * this.precioUnitario; }

        // Polimorfismo: permite redefinir los envases recibidos.
        public virtual int ObtenerVaciosRecibidos() { return 0; }

        // Polimorfismo: permite identificar cada tipo de operacion.
        public virtual string ObtenerTipo() { return "Operacion de agua"; }
    }
}
