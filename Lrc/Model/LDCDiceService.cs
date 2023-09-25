using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Lrc.Model
{
    public class LDCDiceService : IDiceService
    {
       
        
        public List<char> GetDiceResult(int numberOfDices)
        {
            List<char> _diseSides = new() { 'L', 'R', 'C', '.', '.', '.' };
            List<char> result = new();

            for (int i = 0; i < numberOfDices; i++)
            {
                Random random = new Random();
                int diceSideIndex = random.Next(0, 5);
                result.Add(_diseSides[diceSideIndex]);
            }

            return result;
        }
    }
}
