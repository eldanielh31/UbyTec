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
    [Route("api/producto")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProductoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select * from producto
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
        public JsonResult Post(Producto emp)
        {
            string query = @"
                insert into producto (id, cedula_comercio, precio, foto, nombre) 
                values(
                DEFAULT,
                @cedula_comercio,
                @precio,
                @foto,
                @nombre
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
                    myCommand.Parameters.AddWithValue("@cedula_comercio", emp.cedula_comercio);
                    myCommand.Parameters.AddWithValue("@precio", emp.precio);
                    myCommand.Parameters.AddWithValue("@foto", emp.foto);
                    myCommand.Parameters.AddWithValue("@nombre", emp.nombre);
                    newpedidoID = myCommand.ExecuteScalar();

                    myCon.Close();
                }
            }
            return new JsonResult(newpedidoID);
        }

        [HttpPut]
        public JsonResult Put(Producto emp)
        {
            string query = @"
                update producto
                set 
                cedula_comercio = @cedula_comercio,
                precio = @precio,
                foto = @foto,
                nombre = @nombre
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UbyAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@cedula_comercio", emp.cedula_comercio);
                    myCommand.Parameters.AddWithValue("@precio", emp.precio);
                    myCommand.Parameters.AddWithValue("@foto", emp.foto);
                    myCommand.Parameters.AddWithValue("@nombre", emp.nombre);
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
                delete from producto
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