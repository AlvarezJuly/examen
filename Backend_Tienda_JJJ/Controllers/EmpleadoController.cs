using Backend_Tienda_JJJ.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Backend_Tienda_JJJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private IConfiguration _config;

        public EmpleadoController(IConfiguration config)
        {
            _config = config;
        }


            [HttpGet]   //Mostrar todos los Empleados
            public async Task<ActionResult<List<Empleado>>> MostrarEmpleados()
            {
                using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
                conexion.Open();
                var MEmpleado = conexion.Query<Empleado>("SP_ObtenerTodosLosEmpleados", commandType: System.Data.CommandType.StoredProcedure);
                return Ok(MEmpleado);
            }
            [HttpGet("{Id}")]  //Mostrar todos los Empleados por ID
            public async Task<ActionResult<List<Empleado>>> MostEmpleadoId(int Id)
            {
                using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
                conexion.Open();
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                var MIEmpleado = conexion.Query<Empleado>("SP_ObtenerEmpleadoPorID", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
                return Ok(MIEmpleado);
            }

            [HttpPost]    //Insertar Empleados

            public async Task<ActionResult<List<Empleado>>> InsertEmpleados(Empleado Em)
            {
                using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
                conexion.Open();
                var param = new DynamicParameters();
                param.Add("@Nombre", Em.Nombre);
                param.Add("@Direccion", Em.Direccion);
                param.Add("@Telefono", Em.Telefono);
                var IEmpleado = conexion.Query<Empleado>("SP_InsertarEmpleado", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
                return Ok(IEmpleado);

            }

             [HttpPut("{Id}")]  //Actualizar por Id Empleados

             public async Task<ActionResult<List<Empleado>>> ActuEmpleados(Empleado Em)
            {
                using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
                conexion.Open();
                var param = new DynamicParameters();
                param.Add("@Id", Em.Id);
                param.Add("@Nombre", Em.Nombre);
                param.Add("@Direccion", Em.Direccion);
                param.Add("@Telefono", Em.Telefono);
                var AEmpleado = conexion.Query<Empleado>("SP_ActualizarEmpleado", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
                return Ok(AEmpleado);
            }


            [HttpDelete("{Id}")] //Eliminar Empleado por Id 

             public async Task<ActionResult<List<Empleado>>> ElimEmpleadoId(int Id)
            {
                using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
                conexion.Open();
                var param = new DynamicParameters();
                param.Add("@Id", Id);
                var EEmpleado = conexion.Query<Empleado>("SP_EliminarEmpleado", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
                return Ok(EEmpleado);
            }
        }
    }
