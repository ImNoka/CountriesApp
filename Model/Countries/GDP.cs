﻿using Microsoft.EntityFrameworkCore;

namespace TestEFAsyncWPF.Model.Countries
{
    [Owned]
    public class GDP
    {
        public double Value { get; set; }
    }
}