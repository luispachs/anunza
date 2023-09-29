using System;
using System.Collections.Generic;

namespace TablonAnuncios.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int Anuncio { get; set; }
        public int User { get; set; }

        public virtual Anuncio AnuncioNavigation { get; set; } = null!;
        public virtual User UserNavigation { get; set; } = null!;
    }
}
