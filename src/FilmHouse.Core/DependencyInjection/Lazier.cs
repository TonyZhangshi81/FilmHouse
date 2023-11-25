using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmHouse.Core.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TInner"></typeparam>
    public class Lazier<T, TInner> : Lazy<T>, ILazier
        where T : notnull
        where TInner : T
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="valueFactory"></param>
        public Lazier(Func<T> valueFactory)
            : base(valueFactory)
        {
        }
    }
}
