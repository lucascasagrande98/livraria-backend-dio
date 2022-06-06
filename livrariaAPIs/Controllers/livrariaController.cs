using livraria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace livraria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class livrariaController : ControllerBase
    {
        private readonly ToDoContext _context;

        public livrariaController(ToDoContext context)
        {
            _context = context;

            foreach(Produto x in _context.todoProducts)
                _context.todoProducts.Remove(x);
            _context.SaveChanges();

            _context.todoProducts.Add(new Produto { ID = "1", Nome = "Book1", Preco = 47.99, Quant = 1, Categoria = "Ação", Img = "img1" });
            _context.todoProducts.Add(new Produto { ID = "2", Nome = "Book2", Preco = 68.99, Quant = 4, Categoria = "Aventura", Img = "img2" });
            _context.todoProducts.Add(new Produto { ID = "3", Nome = "Book3", Preco = 33.99, Quant = 7, Categoria = "Romance", Img = "img3" });
            _context.todoProducts.Add(new Produto { ID = "4", Nome = "Book4", Preco = 25.99, Quant = 12, Categoria = "Ficção", Img = "img4" });
            _context.todoProducts.Add(new Produto { ID = "5", Nome = "Book5", Preco = 52.99, Quant = 9, Categoria = "Terror", Img = "img5" });

            _context.SaveChanges();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _context.todoProducts.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetItem(int id)
        {
            var item = await _context.todoProducts.FindAsync(id.ToString());

            if(item == null)
            {
                return NotFound();
            }

            return item;
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            _context.todoProducts.Add(produto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProdutos), new
            {
                id = produto.ID
            }, produto);
        }
    }
}
