using System;
using System.Collections.Generic;

namespace ProyFinalV2.Models
{
    public partial class Producto
    {
        public Producto()
        {
            OrdenXproductos = new HashSet<OrdenXproducto>();
        }

        public int Id { get; set; }
        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = null!;
        public double Peso { get; set; }
        public double Precio { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Imagen { get; set; } = null!;
        public int Inventario { get; set; }

        public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
        public virtual ICollection<OrdenXproducto> OrdenXproductos { get; set; }
    }
}
