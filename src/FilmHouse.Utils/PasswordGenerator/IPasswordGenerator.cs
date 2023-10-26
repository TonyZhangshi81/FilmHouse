using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmHouse.Utils.PasswordGenerator
{
    public interface IPasswordGenerator
    {
        string GeneratePassword(PasswordRule rule = null);
    }
}
