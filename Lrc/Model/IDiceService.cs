using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lrc.Model
{
    public interface IDiceService 
    {
        public List<char> GetDiceResult(int numberOfDices);


    }
}
