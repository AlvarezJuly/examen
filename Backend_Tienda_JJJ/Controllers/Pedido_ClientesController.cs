using Backend_Tienda_JJJ.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;

namespace Backend_Tienda_JJJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pedido_ClientesController : ControllerBase
    {

        private IConfiguration _config;
        public Pedido_ClientesController(IConfiguration config)
        {
            _config = config;
        }


        [HttpGet]   //Mostrar todos los Pedidos de clientes
        public async Task<ActionResult<List<Pedido_Cliente>>> MostrarPedid_Cl()
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var MPCliente = conexion.Query<Pedido_Cliente>("SP_ObtenerTodosLosClientes", commandType: System.Data.CommandType.StoredProcedure);
            return Ok(MPCliente);
        }


        [HttpGet("{Id}")]  //Mostrar todos los Pedidos de los clientes por ID
        public async Task<ActionResult<List<Pedido_Cliente>>> MostPClienteId(int Id)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Id", Id);
            var MIPCliente = conexion.Query<Pedido_Cliente>("SP_ObtenerClientePorID", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(MIPCliente);
        }

        [HttpPost]    //Insertar Pediddos de clientes

        public async Task<ActionResult<List<Pedido_Cliente>>> InsertrPed_Clientes(Pedido_Cliente P_Cl)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Cliente_Id", P_Cl.Cliente_Id);
            param.Add("@Producto_Id", P_Cl.Producto_Id);
            param.Add("@Cantidad_Pro", P_Cl.Cantidad_Pro);
            param.Add("@Fecha", P_Cl.Fecha);
            param.Add("@Empleado_Id", P_Cl.Empleado_Id);
            var IPCliente = conexion.Query<Pedido_Cliente>("SP_InsertarPedidoCliente", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(IPCliente);

        }

        [HttpPut("{Id}")]  //Actualizar por Id Pedidos_Clientes

        public async Task<ActionResult<List<Pedido_Cliente>>> ActuClientes(Pedido_Cliente P_Cl)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Id", P_Cl.Id);
            param.Add("@Cliente_Id", P_Cl.Cliente_Id);
            param.Add("@Producto_Id", P_Cl.Producto_Id);
            param.Add("@Cantidad_Pro", P_Cl.Cantidad_Pro);
            param.Add("@Fecha", P_Cl.Fecha);
            param.Add("@Empleado_Id", P_Cl.Empleado_Id);
            var APCliente = conexion.Query<Pedido_Cliente>("SP_ActualizarPedido", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(APCliente);
        }


        [HttpDelete ("{Id}")] //Eliminar por Id Pedidos_Clientes

        public async Task<ActionResult<List<Pedido_Cliente>>> ElimPed_ClienteId(int Id)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Id", Id);
            var EPCliente = conexion.Query<Pedido_Cliente>("SP_EliminarPedido", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(EPCliente);
        }


    }
}
