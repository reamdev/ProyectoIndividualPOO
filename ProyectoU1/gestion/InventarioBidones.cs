using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoU1.gestion
{
    internal class InventarioBidones
    {
        private int bidonesLlenos;
        private int bidonesVacios;

        public int BidonesLlenos { get { return bidonesLlenos; } }
        public int BidonesVacios { get { return bidonesVacios; } }

        public InventarioBidones(int bidonesLlenos, int bidonesVacios)
        {
            if (bidonesLlenos < 0) throw new ArgumentException("Cantidad de Bidones Llenos es invalido");
            if (bidonesVacios < 0) throw new ArgumentException("Cantidad de Bidones Vacios es invalido");

            this.bidonesLlenos = bidonesLlenos;
            this.bidonesVacios = bidonesVacios;
        }

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
