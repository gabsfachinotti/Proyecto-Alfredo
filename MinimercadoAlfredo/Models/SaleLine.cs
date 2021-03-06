﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MinimercadoAlfredo.Models
{
    public class SaleLine
    {
        [Key]
        public int IdSaleLine { get; set; }

        public int Discount { get; set; }

        public float SaleQuantity { get; set; }

        public float LineTotal { get; set; }

        public int IdSale { get; set; }

        public virtual Sale Sale { get; set; }

        public int IdProduct { get; set; } //Clave Foránea Producto

        public virtual Product Product { get; set; }
    }
}