using Mango.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Data;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{

	}

	public DbSet<Category> Category { get; set; }
}
