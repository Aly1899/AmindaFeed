    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

namespace AmindaFeed.Data
{
    public class AmindaProductDb
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public int Category { get; set; }
        public double PurchasePrice { get; set; }
        public double SalePrice { get; set; }
        public bool IsOutOfStock { get; set; }
    }
}
