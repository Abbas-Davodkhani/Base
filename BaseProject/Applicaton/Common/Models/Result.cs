﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicaton.Common.Models
{
    public class Result
    {
        internal Result(bool succeeded , IEnumerable<string> errors) 
        { 
            this.Succeeded = succeeded;
            this.Errors = errors.ToArray();
        }

        public bool Succeeded { get; set; } 
        public string[] Errors { get; set; }


        public static Result Success() =>
            new Result(true, Array.Empty<string>());

        public static Result Failure(IEnumerable<string> errors) =>
            new Result(false , errors);   
    }
}
