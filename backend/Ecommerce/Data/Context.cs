using Ecommerce.Utils;

namespace Ecommerce.Data
{
    public class Context : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductInventory> ProductInventories { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>()
                .HasOne(product => product.ProductInventory)
                .WithOne(productInventory => productInventory.Product)
                .HasForeignKey<ProductInventory>(productInventory => productInventory.ProductId);

            builder.Entity<ProductImage>()
                .HasOne(productImage => productImage.Product)
                .WithMany(product => product.Images)
                .HasForeignKey(productImage => productImage.ProductId);

            builder.Entity<Product>()
                .HasOne(product => product.ProductCategory)
                .WithMany(productCategory => productCategory.Products)
                .HasForeignKey(product => product.ProductCategoryId);

            builder.Entity<State>()
                .HasOne(state => state.Country)
                .WithMany(country => country.States)
                .HasForeignKey(state => state.CountryId);

            builder.Entity<City>()
                .HasOne(city => city.State)
                .WithMany(state => state.Cities)
                .HasForeignKey(city => city.StateId);

            builder.Entity<Address>()
                .HasOne(address => address.City)
                .WithMany(city => city.Addresses)
                .HasForeignKey(address => address.CityId);

            foreach (var entity in builder.Model.GetEntityTypes())
            {
                builder.Entity(entity.ClrType).ToTable(entity.ClrType.Name.ToSnakeCase());

                foreach (var property in entity.ClrType.GetProperties().Where(p => p.PropertyType == typeof(Guid?)))
                {
                    builder
                        .Entity(entity.ClrType)
                        .Property(property.Name)
                        .HasDefaultValueSql("(uuid())");

                    builder
                        .Entity(entity.ClrType)
                        .HasIndex(property.Name)
                        .IsUnique();
                }
            }

            var brazilStates = new List<State>()
            {
                new State() { Id = 1, Code = "AC", Name = "Acre", CountryId = 1 },
                new State() { Id = 2, Code = "AL", Name = "Alagoas", CountryId = 1 },
                new State() { Id = 3, Code = "AP", Name = "Amapá", CountryId = 1 },
                new State() { Id = 4, Code = "AM", Name = "Amazonas", CountryId = 1 },
                new State() { Id = 5, Code = "BA", Name = "Bahia", CountryId = 1 },
                new State() { Id = 6, Code = "CE", Name = "Ceará", CountryId = 1 },
                new State() { Id = 7, Code = "ES", Name = "Espírito Santo", CountryId = 1 },
                new State() { Id = 8, Code = "GO", Name = "Goiás", CountryId = 1 },
                new State() { Id = 9, Code = "MA", Name = "Maranhão", CountryId = 1 },
                new State() { Id = 10, Code = "MT", Name = "Mato Grosso", CountryId = 1 },
                new State() { Id = 11, Code = "MS", Name = "Mato Grosso do Sul ", CountryId = 1 },
                new State() { Id = 12, Code = "MG", Name = "Minas Gerais", CountryId = 1 },
                new State() { Id = 13, Code = "PA", Name = "Pará", CountryId = 1 },
                new State() { Id = 14, Code = "PB", Name = "Paraíba", CountryId = 1 },
                new State() { Id = 15, Code = "PR", Name = "Paraná", CountryId = 1 },
                new State() { Id = 16, Code = "PE", Name = "Pernambuco", CountryId = 1 },
                new State() { Id = 17, Code = "PI", Name = "Piauí", CountryId = 1 },
                new State() { Id = 18, Code = "RJ", Name = "Rio de Janeiro", CountryId = 1 },
                new State() { Id = 19, Code = "RN", Name = "Rio Grande do Norte", CountryId = 1 },
                new State() { Id = 20, Code = "RS", Name = "Rio Grande do Sul ", CountryId = 1 },
                new State() { Id = 21, Code = "RO", Name = "Rondônia", CountryId = 1 },
                new State() { Id = 22, Code = "RR", Name = "Roraima", CountryId = 1 },
                new State() { Id = 23, Code = "SC", Name = "Santa Catarina ", CountryId = 1 },
                new State() { Id = 24, Code = "SP", Name = "São Paulo", CountryId = 1 },
                new State() { Id = 25, Code = "SE", Name = "Sergipe", CountryId = 1 },
                new State() { Id = 26, Code = "TO", Name = "Tocantins", CountryId = 1 },
                new State() { Id = 27, Code = "DF", Name = "Distrito Federal ", CountryId = 1 },
            };

            builder.Entity<Country>().HasData(new Country() { Id = 1, Code = "BR", Name = "Brazil" });
            builder.Entity<State>().HasData(brazilStates);

        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is BaseModel && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow;

                if (entity.State == EntityState.Added)
                {
                    ((BaseModel)entity.Entity).CreatedAt = now;
                }

                ((BaseModel)entity.Entity).UpdatedAt = now;
            }
        }
    }
}