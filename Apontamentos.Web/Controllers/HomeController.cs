using Apontamentos.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
namespace Apontamentos.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Filtros(string data, string usuario)
        {
            return PartialView("_ApontamentoModal", await RecuperaApontamento(data, usuario));
        }

        [HttpPost]
        public async Task<IActionResult> Details(string dataSelecionada, string dataBuscaPorPeriodo, string usuario)
        {
            var listaApontamento = await RecuperaApontamento(dataBuscaPorPeriodo, usuario);
            var detalhes = listaApontamento.FirstOrDefault(s => s.dateFormatada.Equals(dataSelecionada) && s.teamMember.Equals("luiz.felipe"));
            if (detalhes != null)
            {
                return PartialView("_DetalhesModal", detalhes.workItens);
            }
            return PartialView("_DetalhesModal", new List<DetalheApontamentoViewModel>());
        }

        private async Task<List<ApontamentoViewModel>> RecuperaApontamento(string data, string usuario)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://services.bmgmoney.com");
                var responseTask = client.GetAsync($"devops-metrics-api/api/appointment/details?period=" + data);
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    Stream stream = await result.Content.ReadAsStreamAsync();
                    var apontamentos = await JsonSerializer.DeserializeAsync<ApontamentosViewModel>(stream);
                    var filtroFelipe = apontamentos?.detailedAppointments.Where(s => s.teamMember != null && s.teamMember.Equals(usuario)).ToList();
                    if (filtroFelipe != null)
                        return filtroFelipe;
                }
            }
            return new List<ApontamentoViewModel>();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}