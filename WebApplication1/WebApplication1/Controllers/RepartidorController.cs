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
    [Route("api/repartidor")]
    [ApiController]
    public class RepartidorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public RepartidorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select * from repartidor
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
        public JsonResult Post(Repartidor emp)
        {
            string query = @"
                insert into repartidor (id, usuario, id_direccion, contrasena, nombre, apellido1, apellido2, email) 
                values(
                DEFAULT,
                @usuario,
                @id_direccion,
                @contrasena,
                @nombre,
                @apellido1,
                @apellido2,
                @email
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
                    myCommand.Parameters.AddWithValue("@usuario", emp.usuario);
                    myCommand.Parameters.AddWithValue("@id_direccion", emp.id_direccion);
                    myCommand.Parameters.AddWithValue("@contrasena", emp.contrasena);
                    myCommand.Parameters.AddWithValue("@nombre", emp.nombre);
                    myCommand.Parameters.AddWithValue("@apellido1", emp.apellido1);
                    myCommand.Parameters.AddWithValue("@apellido2", emp.apellido2);
                    myCommand.Parameters.AddWithValue("@email", emp.email);
                    newpedidoID = myCommand.ExecuteScalar();

                    myCon.Close();
                }
            }
            return new JsonResult(newpedidoID);
        }

        [HttpPut]
        public JsonResult Put(Repartidor emp)
        {
            string query = @"
                update repartidor
                set 
                id = @id,
                usuario = @usuario,
                id_direccion = @id_direccion,
                contrasena = @contrasena,
                nombre = @nombre,
                apellido1 = @apellido1,
                apellido2 = @apellido2,
                email = @email
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UbyAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", emp.id);
                    myCommand.Parameters.AddWithValue("@usuario", emp.usuario);
                    myCommand.Parameters.AddWithValue("@id_direccion", emp.id_direccion);
                    myCommand.Parameters.AddWithValue("@contrasena", emp.contrasena);
                    myCommand.Parameters.AddWithValue("@nombre", emp.nombre);
                    myCommand.Parameters.AddWithValue("@apellido1", emp.apellido1);
                    myCommand.Parameters.AddWithValue("@apellido2", emp.apellido2);
                    myCommand.Parameters.AddWithValue("@email", emp.email);
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
                delete from repartidor
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