using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace server.Responses
{
    public class Data
    {
        public string Message { get; set; }
        public string Token { get; set; }
    }

    public class UserResponse
    {
    public Data Data { get; set; }
    public string Error { get; set; }

    public UserResponse()
    {
        Data = new Data();
    }
    }
}