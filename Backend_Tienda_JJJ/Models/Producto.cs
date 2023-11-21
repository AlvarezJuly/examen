namespace Backend_Tienda_JJJ.Models
{
    public class Producto
    {

        public int Id { get; set; } 
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Proveedor_Id { get; set; }

    }
}
