using TablonAnuncios.Models;

namespace TablonAnuncios.DataModels
{
    public class profileDataModel
    {
        public User user { get; set; }
        public  List<Anuncio>? anuncios { get; set; }
    }
}
