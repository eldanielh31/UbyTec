using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Producto_Pedido
    {
        public int id_pedido {get; set;}
        public int id_producto {get; set;}
        public int cantidad {get; set;}
    }
}