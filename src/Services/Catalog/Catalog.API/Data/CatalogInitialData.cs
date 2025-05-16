using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync()) { 
            }

            session.Store<Product>(GetPreconfiguredProducts());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>()
        {
            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "IPhone X",
                Description = "This phone is the company biggest change to its ",
                ImageFile = "product-1.png",
                Price = 950.00M,
                Category = new List<string> { "Smart Phone" }
            },
            new Product()
            {
                Id = Guid.NewGuid(),
                Name = "Samsung 10",
                Description = "This phone is the company biggest change to its",
                ImageFile = "product-2.png",
                Price = 840.00M,
                Category = new List<string> { "White Appliances" }
            },
            new Product()
            {
                Id= Guid.NewGuid(),
                Name = "Xiami Mi 9",
                Description = "This phone is the company biggest change to its ",
                 ImageFile = "product-4.png",
                Price = 470.00M,
                Category = new List<string> { "White Appliances" }
            },
            new Product()
            {
                Id= Guid.NewGuid(),
                Name = "HTC U11+ plus",
                Description = "This phone is the company biggest change to its ",
                ImageFile = "product-4.png",
                Price = 380.00M,
                Category = new List<string> { "White Appliances" }
            },
            new Product()
            {
                Id= Guid.NewGuid(),
                Name = "LG G7 ThinQ",
                Description = "This phone is the company biggest change to its",
                ImageFile = "product-6.png",
                Price = 240.00M,
                Category = new List<string> { "Home kitchen" }
            },
            new Product()
            {
                Id= Guid.NewGuid(),
                Name = "Panasonic Lumix",
                Description = "This phone is the company biggest change to its",
                 ImageFile = "product-6.png",
                Price = 240.00M,
                Category = new List<string> { "Camera" }
            }

        };
    }

   
}
