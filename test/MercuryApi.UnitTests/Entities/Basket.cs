﻿using System.Collections.Generic;

namespace MercuryApi.UnitTests.Entities
{
    public class Basket
    {
        public ICollection<Product> Products { get; set; }
    }
}