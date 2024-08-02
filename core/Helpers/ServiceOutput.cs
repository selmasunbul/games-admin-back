using core.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace core.Helpers
{
    [DataContract]
    public class ServiceOutput:IServiceOutput
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
        public int? PageCount
        {
            get
            {
                if (RowCount != null && TotalCount != null && RowCount > 0)
                {
                    int pageCount = TotalCount.Value / 10;
                    if (TotalCount.Value % 10 > 0)
                    {
                        pageCount++;
                    }
                    return pageCount;
                }
                return null;
            }
        }

        [DataMember]
        public object? Data { get; set; }


        private static IServiceOutput Generate(IServiceOutput serviceOutput)
        {
            return serviceOutput;
        }

        private static IServiceOutput Generate(int code, bool status = false, string? message = null, int? rowCount = null, int? totalCount = null, object? data = null)
        {
            IServiceOutput output = new ServiceOutput
            {
                Code = code,
                Status = status,
                Message = message,
                RowCount = rowCount,
                TotalCount = totalCount,
                Data = data
            };

            return output;
        }

        public static async Task<IServiceOutput> GenerateAsync(IServiceOutput serviceOutput)
        {
            return await Task.Run(() => Generate(serviceOutput));
        }

        public static async Task<IServiceOutput> GenerateAsync(int code, bool status = false, string? message = null, int? rowCount = null, int? totalCount = null, object? data = null)
        {
            return await Task.Run(() => Generate(code, status, message, rowCount, totalCount, data));
        }

    }
}
