﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHIVAMFaceEcomm.Models
{
    public class ProductDetail
    {
        public Product Product { get; set; }
        public List<ProductDetailAttributes> Attributes { get; set; }

    }

    public class ProductDetailAttributes
    {
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }

        public int Quantity { get; set; }
        public decimal Cost { get; set; }

        public string ImagePath { get; set; }
        public string ImageName { get; set; }

    }

     


}