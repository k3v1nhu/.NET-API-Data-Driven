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
        public async Task<ActionResult<List<Product>>> Get(
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
        public async Task<ActionResult<List<Product>>> Post(
            [FromBody]Category model,
            [FromServices]DataContext context    
        )
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                context.Categories.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);
            }
            catch
            {
                return BadRequest("Ops.. Não foi possível criar a categoria");
            }
        }
    }
}