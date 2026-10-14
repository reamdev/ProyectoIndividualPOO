using ProyectoU1.gestion;
using ProyectoU1.modelo;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoU1
{

    internal class ConsoleProgram
    {
        private bool ejecucion = true;
        private Distribuidora distribuidora;

        public ConsoleProgram(Distribuidora distribuidora)
        {
            if (distribuidora == null) throw new ArgumentNullException(nameof(distribuidora));

            this.distribuidora = distribuidora;
        }

        private void Menu()
        {
            try
            {
                Console.WriteLine("====================================");
                Console.WriteLine("       DISTRIBUIDORA DE AGUA");
                Console.WriteLine("====================================");
                Console.WriteLine("1.Registrar entrega");
                Console.WriteLine("2.Abastecer bidones");
                Console.WriteLine("3.Consultar inventario");
                Console.WriteLine("4.Consultar historial de entregas");
                Console.WriteLine("5.Ver reporte de operaciones");
                Console.WriteLine("0.Salir");

                Console.Write("Seleccione una opcion: ");
                int op = int.Parse(Console.ReadLine());
                Console.WriteLine();

                switch (op)
                {
                    case 1:
                        RegistrarEntrega();
                        break;
                    case 2:
                        AbastecerBidones();
                        break;
                    case 3:
                        ConsultarInventario();
                        break;
                    case 4:
                        ConsultarHistorial();
                        break;
                    case 5:
                        VerReporte();
                        break;
                    case 0:
                        ejecucion = false;
                        Console.WriteLine("Adios!");
                        break;
                    default:
                        Console.WriteLine("Opcion incorrecta o no disponible");
                        break;
                }
            } catch(FormatException)
            {
                Console.WriteLine("Ingrese un valor valido!\n");
            } catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            } catch (OverflowException)
            {
                Console.WriteLine("El numero ingresado es demasiado grande.");
            }
        }

        private void RegistrarEntrega()
        {
            Console.WriteLine("===== REGISTRAR ENTREGA =====");
            Console.Write("Nombre del cliente: ");
            
            string nombre = Console.ReadLine();
            Cliente cliente = new Cliente(nombre);

            Console.WriteLine();
            Console.WriteLine("Tipo de operacion:");
            Console.WriteLine("1. Recarga - S/ 10.00");
            Console.WriteLine("2. Compra con bidon - S/ 35.00");

            Console.Write("Seleccione una opcion: ");
            if (!int.TryParse(Console.ReadLine(), out int tipo))
            {
                Console.WriteLine("Tipo de operacion invalido.");
                return;
            }

            if (tipo != 1 && tipo != 2)
            {
                Console.WriteLine("Tipo de operacion no disponible.");
                return;
            }

            Console.Write("Cantidad de bidones: ");
            if (!int.TryParse(Console.ReadLine(), out int cantidad))
            {
                Console.WriteLine("Ingrese una cantidad valida.");
                return;
            }

            OperacionAgua operacion;
            if (tipo == 1)
            {
                operacion = new Recarga(cliente, cantidad);
            } else
            {
                operacion = new CompraBidon(cliente, cantidad);
            }

            Console.WriteLine();
            Console.WriteLine("===== RESUMEN DE ENTREGA =====");
            Console.WriteLine("Cliente: " + operacion.Cliente.Nombre);
            Console.WriteLine("Operacion: " + operacion.ObtenerTipo());
            Console.WriteLine("Cantidad: " + operacion.Cantidad);
            Console.WriteLine("Precio unitario: S/ " + operacion.PrecioUnitario);
            Console.WriteLine("Total: S/ " + operacion.CalcularTotal());

            Console.WriteLine();
            Console.Write("Confirmar entrega (S/N): ");
            
            string confirmacion = Console.ReadLine();
            if (confirmacion == null || confirmacion.Trim().ToUpper() != "S")
            {
                Console.WriteLine("Operacion cancelada.");
                return;
            }

            bool resultado = distribuidora.RegistrarOperacion(operacion);
            if (resultado)
            {
                Console.WriteLine("Entrega registrada correctamente.");
                ConsultarInventario();
            } else
            {
                Console.WriteLine("No hay suficientes bidones llenos.");
                Console.WriteLine("La operacion no fue registrada.");
            }
        }

        private void ConsultarInventario()
        {
            Console.WriteLine("===== INVENTARIO =====");
            Console.WriteLine($"Bidones llenos: {distribuidora.BidonesLlenos}");
            Console.WriteLine($"Bidones vacios: {distribuidora.BidonesVacios}");
        }

        private void AbastecerBidones()
        {
            Console.WriteLine("===== ABASTECIMIENTO =====");
            Console.Write("Cantidad de bidones a abastecer: ");

            if (!int.TryParse(Console.ReadLine(), out int cantidad))
            {
                Console.WriteLine("Ingrese una cantidad valida.");
                return;
            }

            bool resultado = distribuidora.Abastecer(cantidad);
            if (resultado)
            {
                Console.WriteLine("Abastecimiento realizado correctamente.");
                ConsultarInventario();
            } else
            {
                Console.WriteLine("No hay suficientes bidones vacios.");
            }
        }

        private void ConsultarHistorial()
        {
            Console.WriteLine("===== HISTORIAL =====");

            if (distribuidora.CantidadOperaciones == 0)
            {
                Console.WriteLine("No hay operaciones registradas.");
                return;
            }

            for (int i = 0; i < distribuidora.CantidadOperaciones; i++)
            {
                OperacionAgua operacion = distribuidora.ObtenerOperacion(i);

                Console.WriteLine();
                Console.WriteLine("Operacion N° " + (i + 1));
                Console.WriteLine("Cliente: " + operacion.Cliente.Nombre);
                Console.WriteLine("Tipo: " + operacion.ObtenerTipo());
                Console.WriteLine("Cantidad: " + operacion.Cantidad);
                Console.WriteLine("Fecha: " + operacion.Fecha);
                Console.WriteLine("Total: S/ " + operacion.CalcularTotal());
            }
        }

        private void VerReporte()
        {
            Console.WriteLine("===== REPORTE DE OPERACIONES =====");

            int totalBidones = 0;
            int totalVaciosRecibidos = 0;

            for (int i = 0; i < distribuidora.CantidadOperaciones; i++)
            {
                OperacionAgua operacion = distribuidora.ObtenerOperacion(i);

                totalBidones += operacion.Cantidad;
                totalVaciosRecibidos += operacion.ObtenerVaciosRecibidos();
            }

            Console.WriteLine($"Operaciones realizadas: {distribuidora.CantidadOperaciones}");
            Console.WriteLine($"Bidones entregados: {totalBidones}");
            Console.WriteLine($"Bidones vacios recibidos: {totalVaciosRecibidos}");
            Console.WriteLine($"Recaudacion total: S/ {distribuidora.CalcularRecaudacion()}");

            Console.WriteLine();
            ConsultarInventario();
        }


        public void Run()
        {
            while (ejecucion)
            {
                Menu();
            }
        }
    }
}
