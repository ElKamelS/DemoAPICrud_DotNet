using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoAPICrud.Controllers
{
    [Route("api/[controller]")]
    public class CrudController : Controller
    {
        private readonly Spark_ErpContext _context;


        public CrudController(Spark_ErpContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Facture>>> GetListFactures()
        {
            return await _context.Factures.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Facture>> GetFactureById(int id)
        {
            var fact = await _context.Factures.FindAsync(id);
            int i = 0;

            if (fact == null)
            {
                return NotFound();
            }

            return fact;
        }

        [HttpPost]
        public async Task<ActionResult<Facture>> AddFacture(Facture item)
        {
            _context.Factures.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddFacture), new { id = item.Id }, item);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutFacture(int id, Facture item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacture(int id)
        {
            var fact = await _context.Factures.FindAsync(id);

            if (fact == null)
            {
                return NotFound();
            }

            _context.Factures.Remove(fact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
