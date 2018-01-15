using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myplat.Util
{
    public interface IAnalyser
    {
        float GetLikenessValue(string needle, string haystack);
    }
}
