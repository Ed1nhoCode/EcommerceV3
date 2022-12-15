using System;
using System.Collections.Generic;

namespace ProyFinalV2.Models
{
    public partial class Ordene
    {
        public Ordene()
        {
            OrdenXproductos = new HashSet<OrdenXproducto>();
        }

        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string DireccionEnvio { get; set; } = null!;
        public DateTime FechaOrden { get; set; }
        public string StatusOrden { get; set; } = null!;
        public double Total { get; set; }

        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
        public virtual ICollection<OrdenXproducto> OrdenXproductos { get; set; }
    }
}
