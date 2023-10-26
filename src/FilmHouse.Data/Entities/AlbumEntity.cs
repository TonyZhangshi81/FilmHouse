using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmHouse.Data.Entities
{
    public class AlbumEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string User { get; set; }

        public string Item { get; set; }

        public string Summary { get; set; }

        public DateTime Time { get; set; }

        public DateTime AlterTime { get; set; }

        public string Cover { get; set; }

        public int Visit { get; set; }

    }
}
