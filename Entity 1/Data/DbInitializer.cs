using Entity_1.Models;

namespace Entity_1.Data
{
    public class DbInitializer
    {
        public static void Initalize(DataContext context)
        {
            var created = context.Database.EnsureCreated();
            if (!created)
            {
                return;
            }
            var categories = new Category[]
            {
                new Category {  Name = "Tv", Code = "01", Active = true },
                new Category {  Name = "Bela tehnika", Code = "02", Active = true },
                new Category {  Name = "Racunari", Code = "03", Active = true },
                new Category {  Name = "Mobilni", Code = "04", Active = true }
            };
            context.AddRange(categories);
            context.SaveChanges();

            var products = new Product[]
            {
                    new Product {
                        Name = "Samsung tv 40''",
                        Code = "02",
                        Active = true,
                        Category = context.Category.FirstOrDefault(p => p.CategoryId == 1),
                        Description = "LED TV",
                        Price = 540,
                        ImageName = "samsung_40_nd.jpg"
                    },
                    new Product {
                        Name = "Ves masina LG",
                        Code = "03",
                        Active = true,
                        Category = context.Category.FirstOrDefault(p => p.CategoryId == 2),
                        Description = "Ves masina akcija",
                        Price = 620,
                        ImageName = "vesmasina_lg_nd.jpg"
                    },
                    new Product {
                        Name = "Dell monitor 24''",
                        Code = "04",
                        Active = true,
                        Category = context.Category.FirstOrDefault(p => p.CategoryId == 3),
                        Description = "LED TV",
                        Price = 240,
                        ImageName = "dell_montor_24_nd.jpg"
                    }
            };
            context.AddRange(products);
            context.SaveChanges();
        }
    }
}
