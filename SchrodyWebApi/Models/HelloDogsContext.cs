using Microsoft.EntityFrameworkCore;

namespace SchrodyWebApi.Models
{
	public class HelloDogsContext : DbContext
	{
		public HelloDogsContext(DbContextOptions<HelloDogsContext> options)
			: base(options)
		{
		}

		public DbSet<HelloDogsItem> HelloDogsItem { get; set; }
	}
}
