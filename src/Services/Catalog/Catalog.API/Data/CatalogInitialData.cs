using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData:IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();

        // ✅ If there are already products, skip seeding
        if (await session.Query<Product>().AnyAsync(token: cancellation))
            return;

        // ✅ Otherwise, insert predefined products
        session.Store(GetPreconfiguredProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private static IEnumerable<object> GetPreconfiguredProducts()
    {
        return new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Apple iPhone 14",
                Description = "Latest model with A15 Bionic chip",
                Price = 999.99m,
                Category = ["Electronics", "Smartphones"],
                ImageFile = "https://example.com/images/iphone14.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Samsung Galaxy S22",
                Description = "High-end smartphone with AMOLED display",
                Price = 899.99m,
                Category = ["Electronics", "Smartphones"],
                ImageFile = "https://example.com/images/galaxys22.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Sony WH-1000XM4",
                Description = "Noise-cancelling over-ear headphones",
                Price = 349.99m,
                Category = ["Electronics", "Audio"],
                ImageFile = "https://example.com/images/sonyheadphones.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Dell XPS 13",
                Description = "Compact laptop with InfinityEdge display",
                Price = 1299.99m,
                Category = ["Electronics", "Laptops"],
                ImageFile = "https://example.com/images/dellxps13.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Apple MacBook Pro 16",
                Description = "Powerful laptop with M1 Pro chip",
                Price = 2399.99m,
                Category = ["Electronics", "Laptops"],
                ImageFile = "https://example.com/images/macbookpro16.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Bose QuietComfort 35 II",
                Description = "Wireless noise-cancelling headphones",
                Price = 299.99m,
                Category = ["Electronics", "Audio"],
                ImageFile = "https://example.com/images/boseheadphones.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Google Pixel 6",
                Description = "Smartphone with Google Tensor chip",
                Price = 599.99m,
                Category = ["Electronics", "Smartphones"],
                ImageFile = "https://example.com/images/pixel6.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Microsoft Surface Pro 8",
                Description = "2-in-1 laptop with touchscreen",
                Price = 1099.99m,
                Category = ["Electronics", "Laptops"],
                ImageFile = "https://example.com/images/surfacepro8.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Fitbit Charge 5",
                Description = "Advanced fitness tracker with GPS",
                Price = 149.99m,
                Category = ["Electronics", "Wearables"],
                ImageFile = "https://example.com/images/fitbitcharge5.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Amazon Echo Dot (4th Gen)",
                Description = "Smart speaker with Alexa",
                Price = 49.99m,
                Category = ["Electronics", "Smart Home"],
                ImageFile = "https://example.com/images/echodot.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Logitech MX Master 3",
                Description = "Advanced wireless mouse with ergonomic design",
                Price = 99.99m,
                Category = ["Electronics", "Accessories"],
                ImageFile = "https://example.com/images/logitechmouse.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Samsung Galaxy Tab S8",
                Description = "Premium Android tablet with S Pen",
                Price = 699.99m,
                Category = ["Electronics", "Tablets"],
                ImageFile = "https://example.com/images/galaxytabs8.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Apple iPad Air (5th Gen)",
                Description = "Powerful tablet with M1 chip",
                Price = 599.99m,
                Category = ["Electronics", "Tablets"],
                ImageFile = "https://example.com/images/ipadair.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Anker PowerCore 20100mAh",
                Description = "High-capacity portable charger",
                Price = 39.99m,
                Category = ["Electronics", "Accessories"],
                ImageFile = "https://example.com/images/ankercharger.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Razer BlackWidow V3",
                Description = "Mechanical gaming keyboard with RGB lighting",
                Price = 129.99m,
                Category = ["Electronics", "Gaming"],
                ImageFile = "https://example.com/images/razerkeyboard.jpg"
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "NVIDIA GeForce RTX 3080",
                Description = "High-performance graphics card for gaming",
                Price = 699.99m,
                Category = ["Electronics", "Gaming"],
                ImageFile = "https://example.com/images/rtx3080.jpg"
            }
        };
    }
}