﻿using MinimercadoAlfredo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinimercadoAlfredo.ViewModels
{
    public class PurchaseVM
    {
        public string ProviderName { get; set; }

        public DateTime PurchaseDate { get; set; }

        public List<PurchaseLine> PurchaseLines { get; set; }

        public decimal PurchaseTotal { get; set; }

        public string PurchaseComments { get; set; }
    }
}