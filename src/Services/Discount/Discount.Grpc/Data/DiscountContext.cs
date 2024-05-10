namespace Discount.Grpc.Data;

public class DiscountContext(DbContextOptions<DiscountContext> options):DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; } = default!;
}