using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Me.Bartecki.InvoiceApp.Model;
using Me.Bartecki.InvoiceApp.Svelte.Data;

namespace AspNetSvelteSpa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceRowsController : ControllerBase
    {
        private readonly MeBarteckiInvoiceAppWebContext _context;

        public InvoiceRowsController(MeBarteckiInvoiceAppWebContext context)
        {
            _context = context;
        }

        // GET: api/InvoiceRows
        [HttpGet("{invoiceId}")]
        public async Task<ActionResult<IEnumerable<InvoiceRow>>> GetInvoiceRows(int invoiceId)
        {
            return await _context.InvoiceRow.Where(x => x.InvoiceId == invoiceId).ToListAsync();
        }

        // PUT: api/InvoiceRows/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{invoiceId}")]
        public async Task<IActionResult> ReplaceInvoiceRows(int invoiceId, IEnumerable<InvoiceRow> invoiceRow)
        {

            using (var transaction = _context.Database.BeginTransaction())
            {
                var receivedInvoiceRows = invoiceRow.Where(x => x.InvoiceId == invoiceId);
                var originalInvoiceRows = _context.InvoiceRow.Where(x => x.InvoiceId == invoiceId);
                var originalIds = originalInvoiceRows
                    .Select(x => x.Id)
                    .ToList();
                var newItems = receivedInvoiceRows
                    .Where(x => x.Id == default(int));
                var deleteItems = originalIds
                    .Where(x => !receivedInvoiceRows.Select(y => y.Id).Contains(x));
                var toUpdateItems = receivedInvoiceRows.Where(x => originalIds.Contains(x.Id));

                foreach (var id in deleteItems)
                {
                    var row = await _context.InvoiceRow.FindAsync(id);
                    if (row == null)
                    {
                        return NotFound();
                    }

                    _context.InvoiceRow.Remove(row);
                }
                foreach (var item in newItems)
                {
                    _context.InvoiceRow.Add(item);
                }
                foreach (var row in toUpdateItems)
                {
                    //var row = invoiceRow.First(x => x.Id == id);
                    _context.Entry(row).State = EntityState.Modified;
                }
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            return Ok();
        }

        // POST: api/InvoiceRows
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<InvoiceRow>> PostInvoiceRow(InvoiceRow invoiceRow)
        {
            _context.InvoiceRow.Add(invoiceRow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInvoiceRow", new { id = invoiceRow.Id }, invoiceRow);
        }

        // DELETE: api/InvoiceRows/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InvoiceRow>> DeleteInvoiceRow(int id)
        {
            var invoiceRow = await _context.InvoiceRow.FindAsync(id);
            if (invoiceRow == null)
            {
                return NotFound();
            }

            _context.InvoiceRow.Remove(invoiceRow);
            await _context.SaveChangesAsync();

            return invoiceRow;
        }

        private bool InvoiceRowExists(int id)
        {
            return _context.InvoiceRow.Any(e => e.Id == id);
        }
    }
}
