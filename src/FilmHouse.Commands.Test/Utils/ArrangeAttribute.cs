using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace FilmHouse.Commands.Test.Utils
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ArrangeAttribute : Attribute
    {
        /// <summary>
        /// <see cref="ArrangeAttribute"/>
        /// </summary>
        /// <param name="arrangeMethodName"><see cref="Action{IServiceCollection}"/></param>
        public ArrangeAttribute(string arrangeMethodName)
        {
            this.ArrangeMethodName = arrangeMethodName;
        }

        public string ArrangeMethodName { get; }
    }
}
