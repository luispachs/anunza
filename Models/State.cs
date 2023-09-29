using System;
using System.Collections.Generic;

namespace TablonAnuncios.Models
{
    public partial class State
    {
        public State()
        {
            Cities = new HashSet<City>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Code { get; set; }
        public int Country { get; set; }

        public virtual Country CountryNavigation { get; set; } = null!;
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
