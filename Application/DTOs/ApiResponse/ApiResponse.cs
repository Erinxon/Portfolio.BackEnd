using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ApiResponse
{
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

        public ApiResponse(int Identity)
        {
            this.Identity = Identity;
        }

        public T Data { get; set; }
        public int Identity { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
