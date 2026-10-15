using System;

namespace ProyectoU1.gestion
{
    // Administra las existencias de bidones llenos y vacios.
    // Encapsulamiento: solo sus metodos pueden modificar las cantidades.
    internal class InventarioBidones
    {
        // Atributos privados que representan el estado del inventario.
        private int bidonesLlenos;
        private int bidonesVacios;

        // Propiedades de solo lectura para consultar las existencias.
        public int BidonesLlenos { get { return bidonesLlenos; } }
        public int BidonesVacios { get { return bidonesVacios; } }

        // Constructor: impide inicializar cantidades negativas.
        public InventarioBidones(int bidonesLlenos, int bidonesVacios)
        {
            if (bidonesLlenos < 0) throw new ArgumentException("Cantidad de Bidones Llenos es invalido");
            if (bidonesVacios < 0) throw new ArgumentException("Cantidad de Bidones Vacios es invalido");

            this.bidonesLlenos = bidonesLlenos;
            this.bidonesVacios = bidonesVacios;
        }

        // RegistrarEntrega(): valida y actualiza los bidones de una entrega.
        public bool RegistrarEntrega(int cantidad, int vaciosRecibidos)
        {
            if (cantidad <= 0) throw new ArgumentException("Registro Entrega: La cantidad es invalida.");
            if (vaciosRecibidos < 0) throw new ArgumentException("Registro Entrega: La cantidad de bidones vacios es invalida.");
            if (vaciosRecibidos > cantidad) throw new ArgumentException("Registro Entrega: Los bidones vacios recibidos no pueden superar la cantidad entregada.");
            if (cantidad > bidonesLlenos) return false;

            bidonesVacios += vaciosRecibidos;
            bidonesLlenos -= cantidad;

            return true;
        }

        /// <summary>
        /// Convierte bidones vacios en llenos si hay suficientes.
        /// </summary>
        /// <param name="cantidad">La cantidad de bidones a llenar.</param>
        public bool Abastecer(int cantidad)
        {
            if (cantidad <= 0) throw new ArgumentException("Abastecer: La cantidad es invalida.");
            if (bidonesVacios < cantidad) return false;

            bidonesLlenos += cantidad;
            bidonesVacios -= cantidad;
            return true;
        }
    }
}
