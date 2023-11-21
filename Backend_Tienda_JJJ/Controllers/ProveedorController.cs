using Backend_Tienda_JJJ.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;

namespace Backend_Tienda_JJJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private IConfiguration _config;
        public ProveedorController(IConfiguration config)
        {
            _config = config;
        }



        [HttpGet]   //Mostrar todos los Proveedores
        public async Task<ActionResult<List<Proveedor>>> MostrarProveedores()
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var MProveedor = conexion.Query<Proveedor>("SP_ObtenerTodosLosProveedores", commandType: System.Data.CommandType.StoredProcedure);
            return Ok(MProveedor);
        }


        [HttpGet("{Id}")]  //Mostrar todos los Proveedores por Id
        public async Task<ActionResult<List<Proveedor>>> MostProveeId(int Id)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Id", Id);
            var MIProvee = conexion.Query<Proveedor>("SP_ObtenerProveedorPorID", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(MIProvee);
        }

        [HttpPost]    //Insertar Proveedores

        public async Task<ActionResult<List<Cliente>>> Insertprovee(Proveedor Prov)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Nombre", Prov.Nombre);
            param.Add("@Direccion", Prov.Direccion);
            param.Add("@Telefono", Prov.Telefono);
            var Iprove = conexion.Query<Proveedor>("SP_InsertarProveedor", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(Iprove);

        }

        [HttpPut("{Id}")]  //Actualizar por Id Proveedor

        public async Task<ActionResult<List<Proveedor>>> ActuProvee(Proveedor Prov)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Id", Prov.Id);
            param.Add("@Nombre", Prov.Nombre);
            param.Add("@Direccion", Prov.Direccion);
            param.Add("@Telefono", Prov.Telefono);
            var AProvee = conexion.Query<Proveedor>("SP_ActualizarProveedor", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(AProvee);
        }


        [HttpDelete("{Id}")] //Eliminar Proveedor por Id 

        public async Task<ActionResult<List<Proveedor>>> ElimProvee(int Id)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Id", Id);
            var EProvee = conexion.Query<Proveedor>("SP_EliminarProveedor", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(EProvee);
        }
    }


}

