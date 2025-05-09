using Microsoft.AspNetCore.Mvc;
using TrabalhoCidade.Models;
using TrabalhoCidade.Repositorio;

namespace TrabalhoCidade.Controllers
{
    public class ProdutoControler : Controller
    {
        private readonly ProdutoRepositorio _produtoRepositorio;

        public ProdutoControler(ProdutoRepositorio produtoRepositorio)
        {
            /* O construtor é chamado quando uma nova instância de LoginController é criada.*/
            _produtoRepositorio = produtoRepositorio;
        }

        public IActionResult Index()
        {
            return View(_produtoRepositorio.TodosProduto());
        }

        public IActionResult CadastrarPoduto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastrarPoduto(Produto produto)
        {
            _produtoRepositorio.CadastrarProduto(produto);

            return RedirectToAction("Index");
        }

        public IActionResult EditarProduto(int id)
        {
            Produto produto = _produtoRepositorio.ObterProduto(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditarProduto(int id, [Bind("CodProd, Nome, Descricao, quantidade, preco")] Produto produto)
        {

            try
            {
                _produtoRepositorio.AtualizarProduto(produto);

                return RedirectToAction("Index");

            }
            catch
            {
                Console.WriteLine("Aconteceu um erro");
                return View();
            }
        }

        public IActionResult ExcluirProduto(int id)
        {
            // Obtém o cliente específico do repositório usando o Codigo fornecido.
            _produtoRepositorio.Excluir(id);
            // Retorna a View de confirmação de exclusão, passando o cliente como modelo.
            return RedirectToAction(nameof(Index));
        }
    }
}
