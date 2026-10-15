using System;

namespace ProyectoU1.modelo
{
    // Representa al cliente que realiza una operacion comercial.
    internal class Cliente
    {
        // Atributo privado: encapsulamiento.
        private string nombre;

        // Propiedad de solo lectura.
        public string Nombre { get { return nombre; } }

        // Constructor: valida el nombre del cliente.
        public Cliente(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) throw new ArgumentNullException("Nombre del Cliente invalido");

            this.nombre = nombre.Trim();
        }
    }
}
