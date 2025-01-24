namespace Usuario.Domain.Models
{
    public class UsuarioGrupo
    {
        public int UsuarioId { get; set; }
        public User Usuario { get; set; }

        public int GrupoId { get; set; }
        public Grupo Grupo { get; set; }
    }
}