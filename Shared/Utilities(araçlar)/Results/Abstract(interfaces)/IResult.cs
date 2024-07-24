using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Utilities_araçlar_.Results
{
    public interface IResult
    {
        public ResultStatus ResultStatus { get;  } //ResultStatus.Success //ResultStatus.Error gibi kullanımları olacak
        public string Message { get;} 
        public Exception Exception { get; }
    }
}
