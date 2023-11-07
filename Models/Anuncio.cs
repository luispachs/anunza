using System;
using System.Collections.Generic;

namespace TablonAnuncios.Models
{
    public partial class Anuncio
    {
        public Anuncio()
        {
            Notifications = new HashSet<Notification>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Config { get; set; } = null!;
        public int City { get; set; }
        public float? Value { get; set; }
        public int UserId { get; set; }

        public virtual City CityNavigation { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
