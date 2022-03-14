using System.ComponentModel.DataAnnotations;

namespace DB.AirBDB.Common.Model.DTO
{
    public class UsuarioPostDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Requer um Login válido.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Campo requer entre 5 e 20 caracteres")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Requer uma Senha válida.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Campo requer entre 5 e 20 caracteres")]
        public string Senha { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Requer um Nome válido.")]
        [StringLength(120, MinimumLength = 5, ErrorMessage = "Campo requer entre 5 e 120 caracteres")]
        public string Nome { get; set; }
    }
}
