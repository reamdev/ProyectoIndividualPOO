using ProyectoU1.modelo;
using System;

namespace ProyectoU1.gestion
{
    // Representa a la Empresa Distribuidora del Agua
    // Coordina el inventario y las operaciones de la distribuidora.
    internal class Distribuidora
    {
        // Asociacion: la distribuidora utiliza un inventario.
        private InventarioBidones inventario;
        
        // Multiplicidad 1 a 0..*: administra varias operaciones.
        // List<OperacionAgua> almacena objetos de clases derivadas.
        private List<OperacionAgua> operaciones;

        // Propiedades de consulta: no exponen el inventario directamente.
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

        // Constructor: recibe el inventario e inicializa el historial.
        public Distribuidora(InventarioBidones inventario)
        {
            if (inventario == null)
                throw new ArgumentNullException(nameof(inventario));

            this.inventario = inventario;
            operaciones = new List<OperacionAgua>();
        }

        // Registra una operacion solo si el inventario permite la entrega.
        public bool RegistrarOperacion(OperacionAgua operacion)
        {
            if (operacion == null) throw new ArgumentNullException(nameof(operacion));
            if (operaciones.Contains(operacion)) return false;

            // Polimorfismo: los vacios recibidos dependen del objeto real.
            bool resultado = inventario.RegistrarEntrega(operacion.Cantidad, operacion.ObtenerVaciosRecibidos());
            if (!resultado) return false;

            operaciones.Add(operacion);
            return true;
        }

        // Delegacion: el inventario realiza el abastecimiento.
        public bool Abastecer(int cantidad)
        {
            return inventario.Abastecer(cantidad);
        }

        // Consulta una operacion registrada.
        public OperacionAgua ObtenerOperacion(int indice)
        {
            if (indice < 0 || indice >= operaciones.Count) throw new ArgumentOutOfRangeException(nameof(indice));

            return operaciones[indice];
        }

        // Calcula la recaudacion sumando las operaciones registradas.
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
