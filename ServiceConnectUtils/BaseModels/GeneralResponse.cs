using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceConnectUtils.BaseModels
{
    public class GeneralResponse : BaseResponseModel
    {
    }
    public class GeneralResponse<T> : BaseResponseModel
    {
        public T Object { get; set; }
    }
}
