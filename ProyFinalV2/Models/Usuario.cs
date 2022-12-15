using System;
using System.Collections.Generic;

namespace ProyFinalV2.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Ordenes = new HashSet<Ordene>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public int Edad { get; set; }

        public virtual ICollection<Ordene> Ordenes { get; set; }
    }
}
