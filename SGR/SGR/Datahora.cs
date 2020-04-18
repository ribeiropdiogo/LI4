using System;
using System.Collections.Generic;

namespace SGR
{
    public partial class Datahora
    {
        public int Id { get; set; }
        public DateTime Inicio { get; set; }
        public int IdHorario { get; set; }
        public DateTime Fim { get; set; }

        public virtual Horario IdHorarioNavigation { get; set; }
    }
}
