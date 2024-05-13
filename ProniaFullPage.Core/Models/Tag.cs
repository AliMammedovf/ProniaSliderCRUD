﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaFullPage.Core.Models
{
    public class Tag: BaseEntity
    {
        public string Name { get; set; } = null!;

        public List<Product> Products { get; set; }
    }
}
