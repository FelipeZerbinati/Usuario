using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuario.Domain.Models
{
    public class Perfil
    {
        public int Id { get; set; }
        public string Bio { get; set; }
        public string Foto { get; set; }
        public int UsuarioId { get; set; }
        public User Usuario { get; set; }
    }
}
