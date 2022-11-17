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
    [Route("api/direccion")]
    [ApiController]
    public class DireccionController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DireccionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select * from direccion
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
        public JsonResult Post(Direccion emp)
        {
            string query = @"
                insert into direccion (id_direccion, provincia, canton, distrito) 
                values(
                DEFAULT,
                @provincia,
                @canton,
                @distrito
                )
                returning id_direccion
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
                    myCommand.Parameters.AddWithValue("@provincia", emp.provincia);
                    myCommand.Parameters.AddWithValue("@canton", emp.canton);
                    myCommand.Parameters.AddWithValue("@distrito", emp.distrito);

                    newDireccionID = myCommand.ExecuteScalar();

                    myCon.Close();
                }
            }
            return new JsonResult(newDireccionID);
        }

        [HttpPut]
        public JsonResult Put(Direccion emp)
        {
            string query = @"
                update direccion
                set 
                id_direccion = @id_direccion,
                provincia = @provincia,
                canton = @canton,
                distrito = @distrito;
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
                    myCommand.Parameters.AddWithValue("@provincia", emp.provincia);
                    myCommand.Parameters.AddWithValue("@canton", emp.canton);
                    myCommand.Parameters.AddWithValue("@distrito", emp.distrito);
                    // myCommand.Parameters.AddWithValue("@Department", emp.Department);
                    // myCommand.Parameters.AddWithValue("@DateOfJoining",Convert.ToDateTime(emp.DateOfJoining));
                    // myCommand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
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
                delete from direccion
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