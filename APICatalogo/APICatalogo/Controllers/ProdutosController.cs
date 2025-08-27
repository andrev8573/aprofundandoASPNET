using APICatalogo.Domain;
using APICatalogo.Properties.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context; // Interface como dependência (diferente nesse caso)

        public ProdutosController(AppDbContext context) // No construtor é feita a injeção de dependência informando instância da interface
        {
            // O atributo da classe passa a ser a instância da interface em vez de declarar a classe direto, recebendo o valor desacopladamente
            _context = context;
        }

        [HttpGet]
        [Route("/buscarProdutos")]
        public ActionResult<IEnumerable<Produto>> buscarProdutos()
        {
            var produtos = _context.Produtos.AsNoTracking().ToList();
            if (produtos is null)
            {
                return NotFound("Não há nenhum produto registrado."); // 404 Not Found
            } else {
                return Ok(produtos); // 200 Ok
            }
        }

        // Parâmetro Path (não precisa definir outra rota) e Name chamando quando o método quando um novo é criado
        [HttpGet("{id:int}", Name = "ObterProdutoCriado")]
        // Padrão ActionResult<Model> para individuais ou ActionResult<Inemurable<Model>> para listas de objetos
        public ActionResult<Produto> buscarProdutoPeloId(int id)
        {
            // Expressão lambda que verifica se o id buscado existe, do contrário o método FirstOrDefault() retorna null
            var produtoEspecifico = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produtoEspecifico is null)
            {
                return NotFound("Produto não encontrado"); // 404 Not Found
            } else
            {
                return Ok(produtoEspecifico); // 200 OK
            }
        }

        [HttpPost]
        public ActionResult criarProduto(Produto produto) // Deve seguir o model, do contrário, retorna 400 Bad Request
        {
            if (produto == null)
            {
                return BadRequest();
            } else {
                _context.Produtos.Add(produto);
                _context.SaveChanges();
                // Retorna o id do produto criado junto do body (vide GET POR ID)
                return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
            }
        }
        [HttpPut("{id:int}")]
        public ActionResult atualizarProduto(int id, Produto produto) { // Obs: A requisição tem que seguir o model
            if (id != produto.ProdutoId)
            {
                return BadRequest(); // 404 Not Found (tem que estar previamente cadastrado)
            }
            else {
                // Como é um objeto que vem de fora, tem que ser tratado (.Modified, .Deleted, .Added, etc) antes de ser executado 
                _context.Entry(produto).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(produto); // 200 OK
            }
        }

        [HttpDelete("id")]
        public ActionResult excluirProduto(int id){
            var produtoEspecifico = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produtoEspecifico == null)
            {
                return BadRequest("Produto não localizado"); // 404 Bad Request
            } else {
            _context.Remove(produtoEspecifico);
            _context.SaveChanges();
            return Ok(produtoEspecifico); // 200 OK
            }
        }
    }
}
