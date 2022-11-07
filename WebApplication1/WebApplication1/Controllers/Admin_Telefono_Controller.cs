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
    [Route("api/admintelefono")]
    [ApiController]
    public class Admin_Telefono_Controller : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public Admin_Telefono_Controller(IConfiguration configuration,IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select *
                from admin_telefono
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
        public JsonResult Post(Admin_Telefono emp)
        {
            string query = @"
                insert into admin_telefono (cedula,telefono) 
                values(
                    @cedula,
                    @telefono,
                    ) 
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
                    myCommand.Parameters.AddWithValue("@telefono", emp.telefono);
                    // myCommand.Parameters.AddWithValue("@DateOfJoining", Convert.ToDateTime(emp.DateOfJoining));
                    // myCommand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Admin_Telefono emp)
        {
            string query = @"
                update admin_telefono
                set cedula = @cedula,
                telefono = @telefono,
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
                    myCommand.Parameters.AddWithValue("@telefono", emp.telefono);
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
        public JsonResult Delete(int cedula, int telefono)
        {
            string query = @"
                delete from admin_telefono
                where cedula=@cedula and telefono=@telefono
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
                    myCommand.Parameters.AddWithValue("@telefono", telefono);
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
