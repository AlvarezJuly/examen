﻿namespace Backend_Tienda_JJJ.Models
{
    public class Pedido_Proveedores
    {

        public int Id { get; set; }
        public string Nombre_Producto { get; set;}
        public int Cantidad { get; set;} 

        public string Fecha { get; set;}
        public int Empleado_Id { get;set;}

        public int Proveedor_Id { get; set;}

    }
}
