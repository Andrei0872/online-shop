using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Responses
{
    public class GenericResponse
        {
        public string Error { get; set; }
        public object Data { get; set; }
    }
}