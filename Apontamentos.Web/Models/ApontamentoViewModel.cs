using System.Text.Json;

namespace Apontamentos.Web.Models
{
    public class ApontamentoViewModel
    {
        public ApontamentoViewModel()
        {
            date = string.Empty;
            teamMember = string.Empty;
            workItens = new List<DetalheApontamentoViewModel>();
        }
        public ApontamentoViewModel(string _date, string _teamMember, List<DetalheApontamentoViewModel> _workItens)
        {
            date = _date;
            teamMember = _teamMember;
            workItens = _workItens;

        }
        public string date { get; set; }
        public string teamMember { get; set; }
        public List<DetalheApontamentoViewModel> workItens { get; set; }

        public string jsonWorkItens { get { return JsonSerializer.Serialize(workItens); } }


        public string dateFormatada { get { return Convert.ToDateTime(date).ToShortDateString(); } }
        public string dateFiltro { get { return date.Replace("T00:00:00", ""); } }
        public string diaDaSemana { get { return TraducaoDiaDaSemana(Convert.ToDateTime(date).DayOfWeek.ToString()); } }

        public string TraducaoDiaDaSemana(string diaEmIngles)
        {

            switch (diaEmIngles)
            {
                case "Monday":
                    {
                        return "Segunda-Feira";
                    }
                case "Tuesday":
                    {
                        return "Terça-Feira";
                    }
                case "Wednesday":
                    {
                        return "Quarta-Feira";
                    }
                case "Thursday":
                    {
                        return "Quinta-Feira";
                    }
                case "Friday":
                    {
                        return "Sexta-Feira";
                    }
                default:
                    return string.Empty;
            }
        }
    }
}