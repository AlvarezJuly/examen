﻿namespace Backend_Tienda_JJJ.Models
{
    public class Factura
    {
        public int Id { get; set; } 

        public int Cliente_Id { get; set; }    
        public int  Empleado_Id { get; set; } 

        public int Pedido_Cliente_Id { get; set; }

        public string Fecha { get; set; }

        public decimal Total_Pagar { get; set;
        }


    }
}
