using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LinkShortener.Domain.Model
{
    public class ResponseMessageSchema
    {
        public string TraceID { get; set; } = "";


        public DateTime Timestamp { get; set; } = DateTime.Now;


        public string Message { get; set; } = "Success";


        public object? Data { get; set; }

        public ResponseMessageSchema()
        {
        }

        public ResponseMessageSchema(HttpContext? context)
        {
            TraceID = ((context != null) ? context.TraceIdentifier : "NoHttpContext");
        }
    }
}
