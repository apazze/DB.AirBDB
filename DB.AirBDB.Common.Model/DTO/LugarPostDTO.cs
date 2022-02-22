namespace DB.AirBDB.Common.Model.DTO
{
    public class LugarPostDTO
    {
        public string Descricao { get; set; }
        public string TipoDeAcomodacao { get; set; }
        public string Cidade { get; set; }
        public double Valor { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
