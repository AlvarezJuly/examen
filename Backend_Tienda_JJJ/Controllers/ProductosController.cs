using Backend_Tienda_JJJ.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;

namespace Backend_Tienda_JJJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {

        private IConfiguration _config;
        public ProductosController(IConfiguration config)
        {
            _config = config;
        }



        [HttpGet]   //Mostrar todos los Productos
        public async Task<ActionResult<List<Producto>>> MostrarProducto()
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var MProduc = conexion.Query<Producto>("SP_ObtenerTodosLosProductos", commandType: System.Data.CommandType.StoredProcedure);
            return Ok(MProduc);
        }


        [HttpGet("{Id}")]  //Mostrar todos los Productos por Id
        public async Task<ActionResult<List<Producto>>> MostProdId(int Id)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Id", Id);
            var MIProd = conexion.Query<Producto>("SP_ObtenerProductoPorID", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(MIProd);
        }

        [HttpPost]  //Insertar Productos

        public async Task<ActionResult<List<Producto>>> Insertprod(Producto Prod)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Nombre", Prod.Nombre);
            param.Add("@Precio", Prod.Precio);
            param.Add("@Proveedor_Id", Prod.Proveedor_Id);
            var Iprod = conexion.Query<Producto>("SP_InsertarProducto", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(Iprod);

        }

        [HttpPut("{Id}")]  //Actualizar por Id Producto

        public async Task<ActionResult<List<Producto>>> ActuProd(Producto Prod)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Id", Prod.Id);
            param.Add("@Nombre", Prod.Nombre);
            param.Add("@Precio", Prod.Precio);
            param.Add("@Proveedor_Id", Prod.Proveedor_Id);
            var AProd = conexion.Query<Producto>("SP_ActualizarProducto", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(AProd);
        }


        [HttpDelete("{Id}")] //Eliminar producto por Id 

        public async Task<ActionResult<List<Producto>>> ElimProd(int Id)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Id", Id);
            var EProd = conexion.Query<Producto>("SP_EliminarProveedor", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(EProd);
        }
    }


}

