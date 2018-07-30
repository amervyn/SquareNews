using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SquareNews.Lib.Interface
{
    public interface IPublicApiCaller
    {
        Task<string> CallPublicService();
    }
}
