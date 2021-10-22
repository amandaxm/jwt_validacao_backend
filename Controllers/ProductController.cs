using jwt_second_version.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace jwt_second_version.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDataContext context;

        public ProductController(ApplicationDataContext context)
        {
            this.context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> GetProducts()
        {
            return await context.Product.ToListAsync();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Products>> GetProduct(int id)
        {
           var products = await context.Product.FindAsync(id);
            if (products == null)
                return NotFound();

            return products;
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<Products>> PostProducts([FromBody] Products  product)
        {
            context.Product.Add(product);
            await context.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = product.ProdId, product });
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Products>> Put(int id, [FromBody] Products product)
        {
            if (id != product.ProdId)
                return BadRequest();
            context.Product.Update(product);
            await context.SaveChangesAsync();
            return CreatedAtAction("Get", new { id = product.ProdId, product });

        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Products>> Delete(int id)
        {
            var prod = await context.Product.FindAsync(id);
            if (prod == null)
                return BadRequest();
            await context.SaveChangesAsync();

            return prod;
        }

    }
}
