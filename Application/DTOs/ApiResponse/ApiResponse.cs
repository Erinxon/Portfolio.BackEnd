using Application.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ApiResponse
{
    public record ErrorDetail(string Description, int StatusCode, Guid ErrorId);

    public record ApiResponse<T>
    {
        public ApiResponse()
        {
                
        }

        public ApiResponse(T Data)
        {
            this.Succeeded = true;
            this.Data = Data;
        }

        public ApiResponse(string ErrorCode, int StatusCode)
        {
            var error = new ErrorDictionaries().ErrorDescriptions.FirstOrDefault(d => d.Key == ErrorCode);
            this.Succeeded = false;
            this.ErrorDetail = new ErrorDetail(error.Value, StatusCode, Guid.NewGuid());
        }

        public ApiResponse(int Identity)
        {
            this.Identity = Identity;
        }

        public T Data { get; set; }
        public int Identity { get; set; }
        public bool Succeeded { get; set; }
        public ErrorDetail ErrorDetail { get; set; }
    }
}
