using ProyectoU1.gestion;
using ProyectoU1.modelo;
using ProyectoU1.utils;
using System;

namespace ProyectoU1
{
    // Gestiona la interaccion del usuario mediante la consola.
    internal class ConsoleProgram
    {
        // Atributos privados para controlar la ejecucion y acceder al negocio.
        private bool ejecucion = true;
        private Distribuidora distribuidora;

        // Constructor: recibe la distribuidora que utilizara la aplicacion.
        public ConsoleProgram(Distribuidora distribuidora)
        {
            if (distribuidora == null) throw new ArgumentNullException(nameof(distribuidora));

            this.distribuidora = distribuidora;
        }

        // Presenta el menu y ejecuta la opcion seleccionada.
        private void Menu()
        {
            try
            {
                ImprimirEnConsola.ColorPersonalizado(
                    "====================================\n" +
                    "       DISTRIBUIDORA DE AGUA\n" +
                    "====================================\n"
                , ConsoleColor.Blue);
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
                        ImprimirEnConsola.Info("Adios!");
                        break;
                    default:
                        ImprimirEnConsola.Warning("Opcion incorrecta o no disponible");
                        break;
                }

                Console.WriteLine();
            } catch(FormatException)
            {
                ImprimirEnConsola.Error("Ingrese un valor valido!\n");
                Console.WriteLine();
            } catch (ArgumentException ex)
            {
                ImprimirEnConsola.Error("Error: " + ex.Message);
                Console.WriteLine();
            } catch (OverflowException)
            {
                ImprimirEnConsola.Error("El numero ingresado es demasiado grande.");
                Console.WriteLine();
            } catch (Exception e)
            {
                ImprimirEnConsola.Error($"Error: {e.Message}");
                Console.WriteLine();
            }
        }

        // Solicita los datos y confirma una recarga o compra con bidon.
        private void RegistrarEntrega()
        {
            ImprimirEnConsola.Info("===== REGISTRAR ENTREGA =====");

            string nombre;
            do
            {
                Console.Write("Nombre del cliente: ");

                nombre = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(nombre))
                    break;

                ImprimirEnConsola.Error("El nombre no puede estar vacio.");
                Console.WriteLine();
            } while (true);

            Cliente cliente = new Cliente(nombre);

            Console.WriteLine();
            Console.WriteLine("Tipo de operacion:");
            Console.WriteLine("1. Recarga - S/ 10.00");
            Console.WriteLine("2. Compra con bidon - S/ 35.00");

            int tipo;
            do
            {
                Console.Write("Seleccione una opcion: ");
                if (int.TryParse(Console.ReadLine(), out tipo) && (tipo == 1 || tipo == 2)) break;

                ImprimirEnConsola.Error("Opcion Incorrecta.");
                Console.WriteLine();
            } while (true);

            int cantidad;
            do
            {
                Console.Write("Cantidad de bidones: ");
                if (int.TryParse(Console.ReadLine(), out cantidad) && cantidad > 0) break;

                ImprimirEnConsola.Error("Ingrese una cantidad mayor que cero.");
                Console.WriteLine();
            } while (true);

            // Referencia de la clase base: puede contener Recarga o CompraBidon.
            OperacionAgua operacion;
            if (tipo == 1)
            {
                operacion = new Recarga(cliente, cantidad);
            } else
            {
                operacion = new CompraBidon(cliente, cantidad);
            }

            Console.WriteLine();
            ImprimirEnConsola.Info("===== RESUMEN DE ENTREGA =====");
            Console.WriteLine("Cliente: " + operacion.Cliente.Nombre);
            Console.WriteLine("Operacion: " + operacion.ObtenerTipo());
            Console.WriteLine("Cantidad: " + operacion.Cantidad);
            Console.WriteLine("Precio unitario: S/ " + operacion.PrecioUnitario);
            Console.WriteLine("Total: S/ " + operacion.CalcularTotal());
            Console.WriteLine();
            
            string confirmacion;
            do
            {
                Console.Write("Confirmar entrega (S/N): ");
                confirmacion = Console.ReadLine();

                if (confirmacion != null)
                    confirmacion = confirmacion.Trim().ToUpper();

                if (confirmacion == "S" || confirmacion == "N")
                    break;

                ImprimirEnConsola.Error("Opcion invalida. Ingrese S o N.");
            } while (true);

            if (confirmacion == "N")
            {
                ImprimirEnConsola.Warning("Operacion cancelada.");
                return;
            }

            bool resultado = distribuidora.RegistrarOperacion(operacion);
            if (resultado)
            {
                ImprimirEnConsola.Success("Entrega registrada correctamente.");

                Console.WriteLine();
                ConsultarInventario();
            } else
            {
                ImprimirEnConsola.Warning("No hay suficientes bidones llenos.");
                ImprimirEnConsola.Error("La operacion no fue registrada.");
            }

            Console.WriteLine();
        }

        // Muestra las existencias actuales de bidones.
        private void ConsultarInventario()
        {
            ImprimirEnConsola.Info("===== INVENTARIO =====");
            Console.WriteLine($"Bidones llenos: {distribuidora.BidonesLlenos}");
            Console.WriteLine($"Bidones vacios: {distribuidora.BidonesVacios}");
            Console.WriteLine();
        }

        // Solicita el abastecimiento mediante Distribuidora.
        private void AbastecerBidones()
        {
            ImprimirEnConsola.Info("===== ABASTECIMIENTO =====");

            int cantidad;
            do
            {
                Console.Write("Cantidad de bidones a abastecer: ");
                if (int.TryParse(Console.ReadLine(), out cantidad) && cantidad > 0) break;

                ImprimirEnConsola.Error("Ingrese una cantidad mayor que cero.");
                Console.WriteLine();
            } while (true);


            bool resultado = distribuidora.Abastecer(cantidad);
            if (resultado)
            {
                ImprimirEnConsola.Success("Abastecimiento realizado correctamente.");

                Console.WriteLine();
                ConsultarInventario();
            } else
            {
                ImprimirEnConsola.Warning("No hay suficientes bidones vacios.");
            }
            Console.WriteLine();
        }

        // Recorre y muestra las operaciones registradas.
        private void ConsultarHistorial()
        {
            ImprimirEnConsola.Info("===== HISTORIAL =====");

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

            Console.WriteLine();
        }

        // Presenta los totales de entregas y recaudacion.
        private void VerReporte()
        {
            ImprimirEnConsola.Info("===== REPORTE DE OPERACIONES =====");

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

        // Mantiene el menu activo hasta seleccionar salir.
        public void Run()
        {
            while (ejecucion)
            {
                Menu();
            }
        }
    }
}
