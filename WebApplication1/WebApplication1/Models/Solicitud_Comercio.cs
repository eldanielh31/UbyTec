using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Solicitud_Comercio
    {
        public int cedula {get; set;}
        public int cedula_admin {get; set;}
        public int cedula_comercio {get; set;}
        public bool aceptado {get; set;}
    }
}