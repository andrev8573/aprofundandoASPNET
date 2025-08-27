using APICatalogo.Domain;
using APICatalogo.Properties.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")] // Define que a URL vai ser o nome do controller
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context; 

        public CategoriasController(AppDbContext context) 
        {
            _context = context;
        }

        [HttpGet]
        [Route("/buscarCategorias")]
        public ActionResult<IEnumerable<Categoria>> buscarCategorias()
        {
            var categorias = _context.Categorias.AsNoTracking().ToList(); // AsNoTracking não rastreia os dados, o que otimiza
            if (categorias is null)
            {
                return NotFound("Não há nenhuma categoria registrada."); // 404 Not Found
            }
            else
            {
                return Ok(categorias); // 200 Ok
            }
        }

       
        [HttpGet("{id:int}", Name = "ObterCategoriaCriada")]
        public ActionResult<Categoria> buscarCategoriaPeloId(int id)
        {
            var categoriaEspecifica = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
            if (categoriaEspecifica is null)
            {
                return NotFound("Categoria não encontrada"); // 404 Not Found
            }
            else
            {
                return Ok(categoriaEspecifica); // 200 OK
            }
        }

        [HttpPost]
        public ActionResult criarCategoria(Categoria categoria) 
        {
            if (categoria == null)
            {
                return BadRequest();
            }
            else
            {
                _context.Categorias.Add(categoria);
                _context.SaveChanges();
                return new CreatedAtRouteResult("ObterCategoriaCriada", new { id = categoria.CategoriaId }, categoria);
            }
        }
        [HttpPut("{id:int}")]
        public ActionResult atualizarCategoria(int id, Categoria categoria)
        { // Obs: A requisição tem que seguir o model
            if (id != categoria.CategoriaId)
            {
                return BadRequest(); // 404 Not Found (tem que estar previamente cadastrado)
            }
            else
            {
                _context.Entry(categoria).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(categoria); // 200 OK
            }
        }

        [HttpDelete("id")]
        public ActionResult excluirCategoria(int id)
        {
            var categoriaEspecifica = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
            if (categoriaEspecifica == null)
            {
                return BadRequest("Categoria não localizada"); // 404 Bad Request
            }
            else
            {
                _context.Remove(categoriaEspecifica);
                _context.SaveChanges();
                return Ok(categoriaEspecifica); // 200 OK
            }
        }

        [HttpGet("CategoriasEProdutosJuntos")]
        public async Task<ActionResult<IEnumerable<Categoria>>> BuscarCategoriasEProdutos(int id)
        {
            try
            {
                // throw new DataMisalignedException(); // Simulação de exceção
                return await _context.Categorias.Include(p => p.Produtos).AsNoTracking().ToListAsync(); // Dados relacionados com método Include()
            } catch (Exception){
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorre um problema com a sua busca.");
            }
        }
    }
}
