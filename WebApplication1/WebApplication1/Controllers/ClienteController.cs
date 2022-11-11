using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/cliente")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public ClienteController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }


        [HttpGet("{username}")]
        public JsonResult GetByUsername(string username)
        {
            string query = @"
                select * from cliente
                where usuario=@username
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UbyAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@username", username);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("cedula/{cedula}")]
        public JsonResult GetByCedula(int cedula)
        {
            string query = @"
                select * from cliente
                where cedula=@cedula
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UbyAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@cedula", cedula);
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
                select *
                from cliente
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
        public JsonResult Post(Cliente emp)
        {
            string query = @"
                insert into cliente (cedula,usuario,contrasena,nombre,apellido1,apellido2,id_direccion, telefono, fecha_nac) 
                values(
                    @cedula,
                    @usuario,
                    @contrasena,
                    @nombre,
                    @apellido1,
                    @apellido2,
                    @id_direccion,
                    @telefono,
                    @fecha_nac
                    )
                    returning cedula
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UbyAppCon");
            object newCedula;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {

                    myCommand.Parameters.AddWithValue("@cedula", emp.cedula);
                    myCommand.Parameters.AddWithValue("@usuario", emp.usuario);
                    myCommand.Parameters.AddWithValue("@contrasena", emp.contrasena);
                    myCommand.Parameters.AddWithValue("@nombre", emp.nombre);
                    myCommand.Parameters.AddWithValue("@apellido1", emp.apellido1);
                    myCommand.Parameters.AddWithValue("@apellido2", emp.apellido2);
                    myCommand.Parameters.AddWithValue("@id_direccion", emp.id_direccion);
                    myCommand.Parameters.AddWithValue("@telefono", emp.telefono);
                    myCommand.Parameters.AddWithValue("@fecha_nac", Convert.ToDateTime(emp.fecha_nac));
                    newCedula = myCommand.ExecuteScalar();
                    myCon.Close();

                }
            }

            return new JsonResult(newCedula);
        }

        [HttpPut]
        public JsonResult Put(Cliente emp)
        {
            string query = @"
                update cliente
                set cedula = @cedula,
                usuario = @usuario,
                contrasena = @contrasena,
                nombre = @nombre,
                apellido1 = @apellido1,
                apellido2 = @apellido2,
                id_direccion = @id_direccion,
                telefono = @telefono,
                fecha_nac = @fecha_nac
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UbyAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@cedula", emp.cedula);
                    myCommand.Parameters.AddWithValue("@usuario", emp.usuario);
                    myCommand.Parameters.AddWithValue("@contrasena", emp.contrasena);
                    myCommand.Parameters.AddWithValue("@nombre", emp.nombre);
                    myCommand.Parameters.AddWithValue("@apellido1", emp.apellido1);
                    myCommand.Parameters.AddWithValue("@apellido2", emp.apellido2);
                    myCommand.Parameters.AddWithValue("@id_direccion", emp.id_direccion);
                    myCommand.Parameters.AddWithValue("@telefono", emp.telefono);
                    myCommand.Parameters.AddWithValue("@fecha_nac",Convert.ToDateTime(emp.fecha_nac));
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{cedula}")]
        public JsonResult Delete(int cedula)
        {
            string query = @"
                delete from cliente
                where cedula=@cedula 
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("UbyAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@cedula", cedula);
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
