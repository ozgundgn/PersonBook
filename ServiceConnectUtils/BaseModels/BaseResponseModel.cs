using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceConnectUtils.BaseModels
{
    public class BaseResponseModel
    {
        public string Message { get; set; }
        public bool Success { get { return true; } set { } }

      
    }
}
