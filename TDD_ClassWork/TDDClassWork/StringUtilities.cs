using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDDClassWork
{
     public class StringUtilities
    {
        static void Main(string[] args)
        {

        }

        public int CountOccurences(string stringToCheck, string stringToFind)
        {
            if (string.IsNullOrEmpty(stringToCheck)) return -1;
            return stringToCheck.ToCharArray().Count(x => x.ToString() == stringToFind);
            /*
            var stringAsCharArray = stringToCheck.ToCharArray();
            var stringToCheckForAsChar = stringToFind.ToCharArray()[0];
           
            
            var occuranceCount = 0;
            for (var characterIndex = 0; characterIndex < stringAsCharArray.GetUpperBound(0); characterIndex++)
            {
                if (stringAsCharArray[characterIndex] == stringToCheckForAsChar)
                {
                    occuranceCount++;
                    
                }
               
               
            }
            return occuranceCount;
            {
                
            }
        }*/
        }
    }
}
