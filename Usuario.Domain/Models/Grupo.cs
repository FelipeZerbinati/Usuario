using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usuario.Domain.Models
{
    public class Grupo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<UsuarioGrupo> UsuarioGrupos { get; set; }
    }
}
