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
    [Route("api/pedido")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public PedidoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("pedidocliente")]
        public JsonResult GetPedidoCliente()
        {
            string query = @"
                SELECT 
                    p.id, 
                    p.comprobante, 
                    p.id_direccion as id_direccion_pedido, 
                    p.cedula_cliente, 
                    p.direc_exacta, 
                    p.entregado,
                    p.total,
                    c.usuario, 
                    c.nombre,
                    c.apellido1,
                    c.apellido2,
                    c.telefono,
                    c.id_direccion as id_direccion_cliente
                    FROM
                    pedido p LEFT JOIN cliente c
                    ON p.cedula_cliente = c.cedula
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UbyAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }



        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select * from pedido
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UbyAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Pedido emp)
        {
            string query = @"
                insert into pedido (id, id_direccion, cedula_cliente, id_repartidor, direc_exacta, comprobante, entregado, total) 
                values(
                DEFAULT,
                @id_direccion,
                @cedula_cliente,
                @id_repartidor,
                @direc_exacta,
                @comprobante,
                @entregado,
                @total
                )
                returning id
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UbyAppCon");
            // NpgsqlDataReader myReader;
            object newpedidoID;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id_direccion", emp.id_direccion);
                    myCommand.Parameters.AddWithValue("@cedula_cliente", emp.cedula_cliente);
                    myCommand.Parameters.AddWithValue("@id_repartidor", emp.id_repartidor);
                    myCommand.Parameters.AddWithValue("@direc_exacta", emp.direc_exacta);
                    myCommand.Parameters.AddWithValue("@comprobante", emp.comprobante);
                    myCommand.Parameters.AddWithValue("@entregado", emp.entregado);
                    myCommand.Parameters.AddWithValue("@total", emp.total);
                    newpedidoID = myCommand.ExecuteScalar();

                    myCon.Close();
                }
            }
            return new JsonResult(newpedidoID);
        }

        [HttpPut]
        public JsonResult Put(Pedido emp)
        {
            string query = @"
                update pedido
                set id = @id,
                id_direccion = @id_direccion,
                cedula_cliente = @cedula_cliente,
                id_repartidor = @id_repartidor,
                direc_exacta = @direc_exacta,
                comprobante = @comprobante
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UbyAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id_direccion", emp.id_direccion);
                    myCommand.Parameters.AddWithValue("@cedula_cliente", emp.cedula_cliente);
                    myCommand.Parameters.AddWithValue("@id_repartidor", emp.id_repartidor);
                    myCommand.Parameters.AddWithValue("@direc_exacta", emp.direc_exacta);
                    myCommand.Parameters.AddWithValue("@comprobante", emp.comprobante);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                delete from pedido
                where id=@id 
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UbyAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Deleted Successfully");
        }

    }
}