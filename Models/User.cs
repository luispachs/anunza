using System;
using System.Collections.Generic;

namespace TablonAnuncios.Models
{
    public partial class User
    {
        public User()
        {
            Anuncios = new HashSet<Anuncio>();
            Notifications = new HashSet<Notification>();
        }

        public int Id { get; set; }
        public long Nit { get; set; }
        public string Firstname { get; set; } = null!;
        public string? Lastname { get; set; }
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int City { get; set; }
        public string? Language { get; set; }
        public string Role { get; set; } = null!;
        public int State { get; set; }
        public string? Token { get; set; }
        public string Password { get; set; } = null!;

        public virtual City CityNavigation { get; set; } = null!;
        public virtual State StateNavigation { get; set; } = null!;
        public virtual ICollection<Anuncio> Anuncios { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
