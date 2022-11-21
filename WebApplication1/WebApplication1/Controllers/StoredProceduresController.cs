using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/procedures")]
    [ApiController]
    public class StoredProceduresController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public StoredProceduresController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("cambiarcontrasena")]
        public JsonResult PostCambiarContrasena(CambiarContrasena emp)
        {
            string query = @"
                CALL public.cambiar_contrasenna(
	            @cedula, 
	            @contrasena
                )
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UbyAppCon");
            // NpgsqlDataReader myReader;
            object newDireccionID;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@cedula", emp.cedula);
                    myCommand.Parameters.AddWithValue("@contrasena", emp.contrasena);

                    newDireccionID = myCommand.ExecuteScalar();

                    myCon.Close();
                }
            }
            return new JsonResult(newDireccionID);
        }

        [HttpPost("pedidoarepartidor")]
        public JsonResult PostPedidoARepartidor(PedidoRepartidor emp)
        {
            string query = @"
            CALL public.pedido_a_repartidor(
                @cedula_cliente, 
                @comprobante, 
                @id_repartidor, 
                @id_direccion, 
                @direc_exacta, 
                @total
            )
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UbyAppCon");
            // NpgsqlDataReader myReader;
            object newDireccionID;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@cedula_cliente", emp.cedula_cliente);
                    myCommand.Parameters.AddWithValue("@comprobante", emp.comprobante);
                    myCommand.Parameters.AddWithValue("@id_repartidor", emp.id_repartidor);
                    myCommand.Parameters.AddWithValue("@id_direccion", emp.id_direccion);
                    myCommand.Parameters.AddWithValue("@direc_exacta", emp.direc_exacta);
                    myCommand.Parameters.AddWithValue("@total", emp.total);

                    newDireccionID = myCommand.ExecuteScalar();

                    myCon.Close();
                }
            }
            return new JsonResult(newDireccionID);
        }


        [HttpPost("recepcionpedido")]
        public JsonResult PostRecepcionPedido(RecepcionPedido emp)
        {
            string query = @"
            CALL public.recepcion_pedido(
                @id
            )
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UbyAppCon");
            // NpgsqlDataReader myReader;
            object newDireccionID;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", emp.id);

                    newDireccionID = myCommand.ExecuteScalar();

                    myCon.Close();
                }
            }
            return new JsonResult(newDireccionID);
        }

    }

}