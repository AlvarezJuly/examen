using Backend_Tienda_JJJ.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;
namespace Backend_Tienda_JJJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pedido_ProveedoresController : ControllerBase
    {
        private IConfiguration _config;
        public Pedido_ProveedoresController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]   //Mostrar todos los Pedido_Proveedores
        public async Task<ActionResult<List<Pedido_Proveedores>>> MostrarPedido()
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var MPedido = conexion.Query<Pedido_Proveedores>("SP_ObtenerPedidosProveedores", commandType: System.Data.CommandType.StoredProcedure);
            return Ok(MPedido);
        }

        [HttpGet("{Id}")]  //Mostrar todos los Pedido_Proveedores por ID
        public async Task<ActionResult<List<Pedido_Proveedores>>> MostPedido(int Id)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var Param = new DynamicParameters();
            Param.Add("@Id", Id);
            var MIPedido = conexion.Query<Pedido_Proveedores>("SP_ObtenerPedido_ProveedoresPorID", Param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(MIPedido);

        }

        [HttpPost]    //Insertar Pedido_Proveedores

        public async Task<ActionResult<List<Pedido_Proveedores>>> InsertrPedido(Pedido_Proveedores Pedido)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Nombre_Producto", Pedido.Nombre_Producto);
            param.Add("@Cantidad", Pedido.Cantidad);
            param.Add("@Fecha", Pedido.Fecha);
            param.Add("@Empleado_Id", Pedido.Empleado_Id);
            param.Add("@Proveedor_Id", Pedido.Proveedor_Id);
            var IPedido = conexion.Query<Pedido_Proveedores>("SP_InsertarPedido_Proveedores", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(IPedido);

        }


        [HttpPut("{Id}")] //Actualizar por Id Pedido_Proveedores

        public async Task<ActionResult<List<Pedido_Proveedores>>> ActuPedido(Pedido_Proveedores Pedido)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Nombre_Producto", Pedido.Nombre_Producto);
            param.Add("@Cantidad", Pedido.Cantidad);
            param.Add("@Fecha", Pedido.Fecha);
            param.Add("@Empleado_Id", Pedido.Empleado_Id);
            param.Add("@Proveedor_Id", Pedido.Proveedor_Id);

            var APedido = conexion.Query<Pedido_Proveedores>("SP_ActualizarPedidoProveedores", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(APedido);
        }

        [HttpDelete("{Id}")] //Eliminar por Id Pedido_Proveedores

        public async Task<ActionResult<List<Pedido_Proveedores>>> ElimPedido(int Id)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Id", Id);
            var EPedido = conexion.Query<Pedido_Proveedores>("SP_EliminarPedidoProveedores", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(EPedido);
        }
    }
}
