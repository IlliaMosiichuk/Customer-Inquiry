﻿using System;
using System.Collections.Generic;
using System.Text;

namespace API.Models
{
    public class RequestValidationResult
    {
        public string Message { get; set; }

        public bool IsValid { get; set; }
    }
}
