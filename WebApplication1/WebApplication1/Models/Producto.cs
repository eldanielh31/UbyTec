using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Producto
    {
        public int id {get; set;}
        public int cedula_comercio {get; set;}
        public int precio {get; set;}
        public string foto {get; set;}
        public string nombre {get; set;}
    }
}