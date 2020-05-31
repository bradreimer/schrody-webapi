using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchrodyWebApi.Models;

namespace SchrodyWebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HelloDogsController : ControllerBase
	{
		private readonly HelloDogsContext m_context;

		public HelloDogsController(HelloDogsContext context)
		{
			m_context = context;
		}

		// GET: api/HelloDogs
		[HttpGet]
		public async Task<ActionResult<IEnumerable<HelloDogsItem>>> GetHelloDogsItem()
		{
			return await m_context.HelloDogsItem.ToListAsync();
		}

		// GET: api/HelloDogs/summary
		[HttpGet("summary")]
		public async Task<ActionResult<HelloDogsSummary>> GetSummary()
		{
			int fletch = await m_context.HelloDogsItem.CountAsync(x => x.Name == "Fletch");

			int fibs = await m_context.HelloDogsItem.CountAsync(x => x.Name == "Fibs");

			return new HelloDogsSummary { FletchCount = fletch, FibsCount = fibs };
		}

		// GET: api/HelloDogs/5
		[HttpGet("{id}")]
		public async Task<ActionResult<HelloDogsItem>> GetHelloDogsItem(long id)
		{
			var helloDogsItem = await m_context.HelloDogsItem.FindAsync(id);

			if (helloDogsItem == null)
			{
				return NotFound();
			}

			return helloDogsItem;
		}

		// PUT: api/HelloDogs/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPut("{id}")]
		public async Task<IActionResult> PutHelloDogsItem(long id, HelloDogsItem helloDogsItem)
		{
			if (id != helloDogsItem.Id)
			{
				return BadRequest();
			}

			m_context.Entry(helloDogsItem).State = EntityState.Modified;

			try
			{
				await m_context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!HelloDogsItemExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/HelloDogs
		// To protect from overposting attacks, enable the specific properties you want to bind to, for
		// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
		[HttpPost]
		public async Task<ActionResult<HelloDogsItem>> PostHelloDogsItem(HelloDogsItem helloDogsItem)
		{
			m_context.HelloDogsItem.Add(helloDogsItem);
			await m_context.SaveChangesAsync();

			return CreatedAtAction(nameof(GetHelloDogsItem), new { id = helloDogsItem.Id }, helloDogsItem);
		}

		// DELETE: api/HelloDogs/5
		[HttpDelete("{id}")]
		public async Task<ActionResult<HelloDogsItem>> DeleteHelloDogsItem(long id)
		{
			var helloDogsItem = await m_context.HelloDogsItem.FindAsync(id);
			if (helloDogsItem == null)
			{
				return NotFound();
			}

			m_context.HelloDogsItem.Remove(helloDogsItem);
			await m_context.SaveChangesAsync();

			return helloDogsItem;
		}

		private bool HelloDogsItemExists(long id)
		{
			return m_context.HelloDogsItem.Any(e => e.Id == id);
		}
	}
}
