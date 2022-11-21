using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class PedidoRepartidor
    {
        public int cedula_cliente { get; set; }
        public string comprobante { get; set; }
        public string direc_exacta { get; set; }
        public int id_repartidor { get; set; }
        public int id_direccion { get; set; }
        public int total { get; set; }
    }
}