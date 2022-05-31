namespace Apontamentos.Web.Models
{
    public class ApontamentosViewModel
    {

        public ApontamentosViewModel() { detailedAppointments = new List<ApontamentoViewModel>(); }
        public ApontamentosViewModel(List<ApontamentoViewModel> _detailedAppointments)
        {
            detailedAppointments = _detailedAppointments;
        }
        public List<ApontamentoViewModel> detailedAppointments { get; set; }
    }
}