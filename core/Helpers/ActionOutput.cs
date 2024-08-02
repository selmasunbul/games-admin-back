using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using core.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace core.Helpers
{
    [DataContract]
    public class ActionOutput
    {
        [DataMember]
        public int Code { get; set; } = 100;

        [DataMember]
        public bool Status { get; set; } = false;

        [DataMember]
        public string? Message { get; set; }
        [DataMember]
        public int? RowCount { get; set; }

        [DataMember]
        public int? TotalCount { get; set; }

        [DataMember]
        public int? PageCount { get; set; }

        [DataMember]
        public object? Data { get; set; }


        private static IActionResult Generate(IServiceOutput? serviceOutput)
        {
            ObjectResult output = new(serviceOutput)
            {
                StatusCode = serviceOutput?.Code ?? 204
            };

            return output;
        }

        private static IActionResult Generate(int code, bool status = false, string? message = null, int? rowCount = null, int? totalCount = null, object? data = null)
        {
            IServiceOutput serviceOutput = new ServiceOutput
            {
                Code = code,
                Status = status,
                Message = message,
                RowCount = rowCount,
                TotalCount = totalCount,
                Data = data
            };

            ObjectResult output = new(serviceOutput)
            {
                StatusCode = serviceOutput?.Code ?? 204
            };

            return output;
        }

        public static async Task<IActionResult> GenerateAsync(IServiceOutput serviceOutput)
        {
            return await Task.Run(() => Generate(serviceOutput));
        }

        public static async Task<IActionResult> GenerateAsync(int code, bool status = false, string? message = null, int? rowCount = null, int? totalCount = null, object? data = null)
        {
            return await Task.Run(() => Generate(code, status, message, rowCount, totalCount, data));
        }
    }
}

