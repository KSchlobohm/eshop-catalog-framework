using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using eShopCatalogApi.DAL;
using eShopCatalogApi.Models;

namespace eShopCatalogApi.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly CatalogContext db = new CatalogContext();

        [HttpGet, Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            return Ok(await db.Products.OrderBy(p => p.Id).ToListAsync());
        }

        [HttpGet, Route("{id:int}", Name = "GetProduct")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var product = await db.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Create(ProductRequest request)
        {
            if (request == null)
                return BadRequest("A product is required.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = new Product { Sku = request.Sku, Name = request.Name, Price = request.Price.Value };
            db.Products.Add(product);
            await db.SaveChangesAsync();
            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut, Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, ProductRequest request)
        {
            if (request == null)
                return BadRequest("A product is required.");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await db.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.Sku = request.Sku;
            product.Name = request.Name;
            product.Price = request.Price.Value;
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete, Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var product = await db.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();
            base.Dispose(disposing);
        }
    }
}
