using System;
using System.Collections.Generic;

namespace SGR
{
    public partial class Mercadoriainartigo
    {
        public int Id { get; set; }
        public int IdMercadoria { get; set; }
        public int IdArtigo { get; set; }

        public virtual Artigo IdArtigoNavigation { get; set; }
        public virtual Mercadoria IdMercadoriaNavigation { get; set; }
    }
}
