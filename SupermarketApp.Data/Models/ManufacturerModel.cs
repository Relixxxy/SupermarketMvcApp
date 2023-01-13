﻿using Microsoft.AspNetCore.Http;
using SupermarketApp.Data.Entities;

namespace SupermarketApp.Data.Models
{
    public class ManufacturerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public IEnumerable<Product>? Products { get; set; }
    }
}