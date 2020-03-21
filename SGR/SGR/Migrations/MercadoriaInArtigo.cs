using System;
using System.Collections.Generic;

namespace SGR.Migrations
{
    public partial class MercadoriaInArtigo
    {
        public int Id { get; set; }
        public int IdMercadoria { get; set; }
        public int IdArtigo { get; set; }

        public virtual Artigo IdArtigoNavigation { get; set; }
        public virtual Mercadoria IdMercadoriaNavigation { get; set; }
    }
}
