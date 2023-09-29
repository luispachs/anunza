using System;
using System.Collections.Generic;

namespace TablonAnuncios.Models
{
    public partial class City
    {
        public City()
        {
            Anuncios = new HashSet<Anuncio>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int State { get; set; }
        public int Country { get; set; }

        public virtual Country CountryNavigation { get; set; } = null!;
        public virtual State StateNavigation { get; set; } = null!;
        public virtual ICollection<Anuncio> Anuncios { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
