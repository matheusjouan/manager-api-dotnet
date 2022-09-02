using Manager.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Manager.Infra.Persistence.Context;

public class ManagerContext : DbContext
{
	public ManagerContext() { }
	public ManagerContext(DbContextOptions<ManagerContext> options) : base(options) { }

	public DbSet<User> Users { get; set; }

	protected override void OnModelCreating(ModelBuilder mb)
	{
        // Vai aplicar todas as configurações (classes que implementam IEntityTypeConfiguration<T>)
        mb.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}

