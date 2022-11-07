using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Direccion
    {
        public int id_direccion {get; set;}
        public string provincia {get; set;}
        public string canton {get; set;}
        public string distrito {get; set;}
    }
}