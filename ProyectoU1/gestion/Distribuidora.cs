using ProyectoU1.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoU1.gestion
{
    internal class Distribuidora
    {
        private InventarioBidones inventario;
        private List<OperacionAgua> operaciones;

        public int BidonesLlenos
        {
            get { return inventario.BidonesLlenos; }
        }

        public int BidonesVacios
        {
            get { return inventario.BidonesVacios; }
        }
        
        public int CantidadOperaciones
        {
            get { return operaciones.Count; }
        }

        public Distribuidora(InventarioBidones inventario)
        {
            if (inventario == null)
                throw new ArgumentNullException(nameof(inventario));

            this.inventario = inventario;
            operaciones = new List<OperacionAgua>();
        }

        public bool RegistrarOperacion(OperacionAgua operacion)
        {
            if (operacion == null) throw new ArgumentNullException(nameof(operacion));
            if (operaciones.Contains(operacion)) return false;

            bool resultado = inventario.RegistrarEntrega(operacion.Cantidad, operacion.ObtenerVaciosRecibidos());
            if (!resultado) return false;

            operaciones.Add(operacion);
            return true;
        }

        public bool Abastecer(int cantidad)
        {
            return inventario.Abastecer(cantidad);
        }

        public OperacionAgua ObtenerOperacion(int indice)
        {
            if (indice < 0 || indice >= operaciones.Count) throw new ArgumentOutOfRangeException(nameof(indice));

            return operaciones[indice];
        }

        public decimal CalcularRecaudacion()
        {
            decimal total = 0;

            foreach (OperacionAgua operacion in operaciones)
            {
                total += operacion.CalcularTotal();
            }

            return total;
        }
    }
}
