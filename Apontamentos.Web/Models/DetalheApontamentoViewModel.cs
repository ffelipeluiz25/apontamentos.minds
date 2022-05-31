namespace Apontamentos.Web.Models
{
    public class DetalheApontamentoViewModel
    {
        public DetalheApontamentoViewModel()
        {
            externalId = 0;
            titlte = string.Empty;
        }
        public DetalheApontamentoViewModel(int _externalId, string _title)
        {
            externalId = _externalId;
            titlte = _title;
        }
        public int externalId { get; set; }
        public string titlte { get; set; }
    }
}