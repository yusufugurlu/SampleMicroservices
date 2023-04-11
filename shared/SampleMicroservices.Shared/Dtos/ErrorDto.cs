using System;
using System.Collections.Generic;
using System.Text;

namespace SampleMicroservices.Shared.Dtos
{
    public class ErrorDto
    {
        public List<string> error { get; set; }
        public string error_description { get; set; }
    }
}
