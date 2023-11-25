using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmHouse.Core.DependencyInjection;

namespace FilmHouse.Core.Utils.PasswordGenerator
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceRegister(SelfServiceLifetime.Transient)]
    public interface IPasswordGenerator
    {
        string GeneratePassword(PasswordRule rule = null);
    }
}
