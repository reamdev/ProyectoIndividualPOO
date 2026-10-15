/*
PROYECTO: SISTEMA DE GESTION DE DISTRIBUIDORA DE AGUA

PROBLEMATICA:
    Una distribuidora de agua necesita controlar las entregas de bidones, las existencias de envases llenos y
vacios, y los ingresos obtenidos. El registro manual puede ocasionar errores en el inventario y los cobros.

REGLAS DEL NEGOCIO:
    1. La recarga cuesta S/ 10 por bidon y recibe un envase vacio.
    2. La compra con bidon cuesta S/ 35 y no recibe envases vacios.
    3. Toda entrega requiere un cliente y una cantidad mayor que cero.
    4. No se pueden entregar mas bidones llenos de los disponibles.
    5. El inventario nunca puede tener cantidades negativas.
    6. El abastecimiento convierte bidones vacios en llenos.
    7. Una operacion cancelada o rechazada no modifica el inventario.
    8. Las operaciones confirmadas se registran en el historial.
    9. La recaudacion es la suma de los importes de las operaciones.
*/

using ProyectoU1.gestion;
using System;

namespace ProyectoU1;

class Program
{
    public static void Main(string[] args)
    {
        // Creacion de objetos e inicializacion del sistema.
        InventarioBidones inventario = new InventarioBidones(20, 5);
        Distribuidora distribuidora = new Distribuidora(inventario);

        ConsoleProgram app = new ConsoleProgram(distribuidora);
        app.Run();
    }
}
