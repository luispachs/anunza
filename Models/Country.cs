using System;
using System.Collections.Generic;

namespace TablonAnuncios.Models
{
    public partial class Country
    {
        public Country()
        {
            Cities = new HashSet<City>();
            States = new HashSet<State>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Prefix { get; set; }

        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
}
