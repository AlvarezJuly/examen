using Backend_Tienda_JJJ.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Dapper;

namespace Backend_Tienda_JJJ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private IConfiguration _config;
        public FacturasController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]   //Mostrar todos los Facturas
        public async Task<ActionResult<List<Factura>>> MostrarFactu()
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var MFac = conexion.Query<Factura>("SP_ObtenerTodasLasFacturas", commandType: System.Data.CommandType.StoredProcedure);
            return Ok(MFac);
        }

        [HttpGet("{Id}")]  //Mostrar todos los Factura por ID
        public async Task<ActionResult<List<Factura>>> MostFacId(int Id)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var Param = new DynamicParameters();
            Param.Add("@Id", Id);
            var MIFac = conexion.Query<Factura>("sp_ObtenerFacturaPorID", Param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(MIFac);
        }


        [HttpPost]    //Insertar Factura

        public async Task<ActionResult<List<Factura>>> InsertrFac(Factura Fac)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Cliente_Id", Fac.Cliente_Id);
            param.Add("@Empleado_Id", Fac.Empleado_Id);
            param.Add("@Pedido_Cliente_Id", Fac.Pedido_Cliente_Id);
            param.Add("@Fecha", Fac.Fecha);
            param.Add("@Total_Pagar", Fac.Total_Pagar);
            var IFac = conexion.Query<Factura>("SP_InsertarFactura", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(IFac);

        }

        [HttpPut("{Id}")] //Actualizar por Id Factura

        public async Task<ActionResult<List<Factura>>> ActuFac(Factura Fac)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Id", Fac.Id);
            param.Add("@Cliente_Id", Fac.Cliente_Id);
            param.Add("@Empleado_Id", Fac.Empleado_Id);
            param.Add("@Pedido_Cliente_Id", Fac.Pedido_Cliente_Id);
            param.Add("@Fecha", Fac.Fecha);
            param.Add("@Total_Pagar", Fac.Total_Pagar);

            var AFac = conexion.Query<Factura>("SP_ActualizarFactura", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(AFac);
        }
        [HttpDelete("{Id}")] //Eliminar por Id Factura

        public async Task<ActionResult<List<Factura>>> ElimFacId(int Id)
        {
            using var conexion = new SqlConnection(_config.GetConnectionString("ConexioBD"));
            conexion.Open();
            var param = new DynamicParameters();
            param.Add("@Id", Id);
            var EFac = conexion.Query<Factura>("SP_EliminarFactura", param, commandType: System.Data.CommandType.StoredProcedure).SingleOrDefault();
            return Ok(EFac);
        }

    }
}
