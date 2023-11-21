namespace Backend_Tienda_JJJ.Models
{
    public class Pedido_Cliente
    {

        public int Id { get; set; }

        public int Cliente_Id { get; set;}

        public int Producto_Id { get; set; }

        public int Cantidad_Pro { get; set; }

        public  string Fecha { get; set;}

        public int Empleado_Id { get; set;}  
    }
}
