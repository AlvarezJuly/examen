using Backend_Tienda_JJJ.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;
namespace Backend_Tienda_JJJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private IConfiguration _config;
        public ClientesController(IConfiguration config)
        {
            _config = config;
        }



        [HttpGet]   //Mostrar todos los clientes
        public async Task<ActionResult<List<Cliente>>> MostrarClientes()
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var MCliente = conexion.Query<Cliente>("SP_ObtenerTodosLosClientes", commandType: System.Data.CommandType.StoredProcedure);
            return Ok(MCliente);
        }
        [HttpGet("{Id}")]  //Mostrar todos los clientes por ID
        public async Task<ActionResult<List<Cliente>>> MostClienteId(int Id)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var CI = new DynamicParameters();
            CI.Add("@Id", Id);
            var MICliente = conexion.Query<Cliente>("SP_ObtenerClientePorID", CI, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(MICliente);
        }

        [HttpPost]    //Insertar clientes

        public async Task<ActionResult<List<Cliente>>> InsertrClientes(Cliente Clien)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Nombre", Clien.Nombre);
            param.Add("@Direccion", Clien.Direccion);
            param.Add("@Telefono", Clien.Telefono);
            var ICliente = conexion.Query<Cliente>("SP_InsertarCliente", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(ICliente);

        }

       [HttpPut("{Id}")] //Actualizar por Id clientes

        public async Task<ActionResult<List<Cliente>>> ActuClientes(Cliente Clien)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Id", Clien.Id);
            param.Add("@Nombre", Clien.Nombre);
            param.Add("@Direccion", Clien.Direccion);
            param.Add("@Telefono", Clien.Telefono);
            var ACliente = conexion.Query<Cliente>("SP_ActualizarCliente", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(ACliente);
        }


        [HttpDelete("{Id}")] //Eliminar por Id clientes

        public async Task<ActionResult<List<Cliente>>> ElimClienteId(int Id)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var CI = new DynamicParameters();
            CI.Add("@Id", Id);
            var ECliente = conexion.Query<Cliente>("SP_EliminarCliente", CI, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(ECliente);
        }
    }
    }
