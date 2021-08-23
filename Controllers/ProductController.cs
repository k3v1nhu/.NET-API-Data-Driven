using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;

namespace Shop.Controllers
{
    [Route("products")]
    public class ProductControler : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get([FromServices]DataContext context)
        {
            var products = await context.
                                 Products.
                                 Include(x => x.Category).
                                 AsNoTracking().
                                 ToListAsync();
            return Ok(products);
        }

        [Route("{id:int}")]
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetById(
            int id,
            [FromServices]DataContext context)
        {
            var products = await context.
                                 Products.
                                 Include(x => x.Category).
                                 AsNoTracking().
                                 Where(x => x.CategoryId == id).
                                 ToListAsync();
            return Ok(products);
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult<Product>> Post(
            [FromBody]Product model,
            [FromServices]DataContext context    
        )
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Products.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch
            {
                return BadRequest("Ops.. Não foi possível criar a categoria");
            }
        }

        [Route("{id:int}")]
        [HttpPut]
        public async Task<ActionResult<List<Product>>> Put (
            int id,
            [FromBody]Product model,
            [FromServices]DataContext context
        )
        {
            if (id != model.Id)
                return NotFound(new { message = "Categoria não encontrada" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Entry<Product>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "DB Error - Não foi possível atualizar o produto" });
            }
            catch (Exception)
            {
                return BadRequest("Não foi possível atualizar o produto");
            }
        }

        [Route("{id:int}")]
        [HttpDelete]
        public async Task<ActionResult<List<Product>>> Delete(
            int id,
            [FromServices]DataContext context)
        {
            var product = await context.
                                Products.
                                FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return NotFound(new { message = "Produto não encontrado" });

            try{
                context.Products.Remove(product);
                await context.SaveChangesAsync();
                return Ok(new { message = "Categoria removida com sucesso"});
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Não foi possivel remover a categoria"});
            }
        }
                
    }
}