using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var enrty in ChangeTracker.Entries<EntityBase>())
            {
                switch (enrty.State)
                {
                    case EntityState.Added:
                        enrty.Entity.CreatedDate = DateTime.Now;
                        enrty.Entity.CreatedBy = "swn";
                        break;
                    case EntityState.Modified:
                        enrty.Entity.LastModifiedDate = DateTime.Now;
                        enrty.Entity.LastModifiedBy = "swn";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
