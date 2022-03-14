using System.ComponentModel.DataAnnotations;

namespace DB.AirBDB.Common.Model.DTO
{
    public class LugarPostDTO
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Requer uma Descrição válida.")]
        [StringLength(500, MinimumLength = 5, ErrorMessage = "Campo requer entre 5 e 500 caracteres")]
        public string Descricao { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Requer um tipo de acomodação válido. Ex. Casa, Apartamento, Quarto, etc.")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Campo requer entre 5 e 20 caracteres")]
        public string TipoDeAcomodacao { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Requer uma cidade válida.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Campo requer entre 5 e 50 caracteres")]
        public string Cidade { get; set; }
        public double Valor { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
