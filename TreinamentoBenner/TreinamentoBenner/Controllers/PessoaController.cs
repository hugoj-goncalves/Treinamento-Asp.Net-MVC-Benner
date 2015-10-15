using System.Web.Mvc;
using TreinamentoBenner.Core.Model;
using TreinamentoBenner.Core.Service.Interfaces;

namespace TreinamentoBenner.Controllers
{
    [Authorize]
    public class PessoaController : Controller
    {
        private readonly IServicePessoa _servicePessoa;
        private readonly IServiceCidade _serviceCidade;

        public PessoaController(IServicePessoa servicePessoa, IServiceCidade serviceCidade)
        {
            _servicePessoa = servicePessoa;
            _serviceCidade = serviceCidade;
        }

        public ActionResult Index()
        {
            return View(_servicePessoa.All(true));
        }

        public ActionResult Create(int id = 0)
        {
            var pessoa = _servicePessoa.Find(id) ?? new Pessoa();

            Dropdown(pessoa);

            return View(pessoa);
        }

        private void Dropdown(Pessoa pessoa)
        {
            var uf = pessoa.Cidade?.Uf;
            ViewBag.Ufs = new SelectList(_serviceCidade.ListarEstados(), uf);
            ViewBag.IdCidade = new SelectList(_serviceCidade.Query(q => q.Uf == uf), "Id", "Nome", pessoa.IdCidade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pessoa pessoa)
        {
            if (!ModelState.IsValid) return View(pessoa);

            _servicePessoa.Save(pessoa);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            return View(_servicePessoa.Find(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _servicePessoa.Delete(id);
            return RedirectToAction("Index");
        }
    }
}