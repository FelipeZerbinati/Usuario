using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuario.Domain.DTO
{
    public class ResultData<T>
    {

        public T? Data { get; set; }
        public string ErrorDescription { get; set; } = string.Empty;
        public bool Success { get; set; }
    }
}
