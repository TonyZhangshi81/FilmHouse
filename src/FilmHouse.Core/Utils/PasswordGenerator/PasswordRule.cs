using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmHouse.Core.Utils.PasswordGenerator
{
    public class PasswordRule
    {
        public int Length { get; set; }

        public int LeastNumberOfNonAlphanumericCharacters { get; set; }

        public PasswordRule(int length, int leastNumberOfNonAlphanumericCharacters)
        {
            Length = length;
            LeastNumberOfNonAlphanumericCharacters = leastNumberOfNonAlphanumericCharacters;
        }
    }
}
