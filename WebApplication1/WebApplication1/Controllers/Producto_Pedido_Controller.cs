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
    [Route("api/productopedido")]
    [ApiController]
    public class Producto_Pedido_Controller : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public Producto_Pedido_Controller(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select * from producto_pedido
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
        public JsonResult Post(Producto_Pedido emp)
        {
            string query = @"
                insert into producto_pedido (id_pedido, id_producto, cantidad) 
                values(
                @id_pedido,
                @id_producto,
                @cantidad
                )
                returning id_pedido
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
                    myCommand.Parameters.AddWithValue("@id_pedido", emp.id_pedido);
                    myCommand.Parameters.AddWithValue("@id_producto", emp.id_producto);
                    myCommand.Parameters.AddWithValue("@cantidad", emp.cantidad);
                    newpedidoID = myCommand.ExecuteScalar();

                    myCon.Close();
                }
            }
            return new JsonResult(newpedidoID);
        }

        [HttpPut]
        public JsonResult Put(Producto_Pedido emp)
        {
            string query = @"
                update producto_pedido
                set 
                id_pedido = @id_pedido,
                id_producto = @id_producto,
                cantidad = @cantidad
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UbyAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id_pedido", emp.id_pedido);
                    myCommand.Parameters.AddWithValue("@id_producto", emp.id_producto);
                    myCommand.Parameters.AddWithValue("@cantidad", emp.cantidad);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id_pedido, int id_producto)
        {
            string query = @"
                delete from producto_pedido
                where id_pedido=@id_pedido and id_producto=@id_producto
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UbyAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id_pedido", id_pedido);
                    myCommand.Parameters.AddWithValue("@id_producto", id_producto);
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