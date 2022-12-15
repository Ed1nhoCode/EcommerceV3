using System;
using System.Collections.Generic;

namespace ProyFinalV2.Models
{
    public partial class OrdenXproducto
    {
        public int Id { get; set; }
        public int OrdenId { get; set; }
        public int ProductoId { get; set; }

        public virtual Ordene Orden { get; set; } = null!;
        public virtual Producto Producto { get; set; } = null!;
    }
}
