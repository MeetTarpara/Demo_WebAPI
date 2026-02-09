using DemoApi.Models;

namespace DemoApi.Services
{
    public class ProductService
    {
        private readonly List<Product> _products = new();
        private int _nextId = 1;

        public ProductService()
        {
         
            _products.Add(new Product
            {
                Id = _nextId++,
                Name = "Laptop",
                Price = 55000
            });

            _products.Add(new Product
            {
                Id = _nextId++,
                Name = "Mobile Phone",
                Price = 25000
            });

            _products.Add(new Product
            {
                Id = _nextId++,
                Name = "Headphones",
                Price = 3000
            });
        }


        public List<Product> GetAll(){
            return _products;
        }

        public Product? GetById(int id){
            return _products.FirstOrDefault(p => p.Id == id);
        }
             

        public Product Create(Product product)
        {
            
            _products.Add(new Product{
                Id=_nextId++,
                Name=product.Name,
                Price=product.Price
            });
            return product;
        }

        public bool Update(int id, Product product)
        {
            var existing = GetById(id);
            if (existing == null) return false;

            existing.Name = product.Name;
            existing.Price = product.Price;
            return true;
        }

        public bool Delete(int id)
        {
            var product = GetById(id);
            if (product == null) return false;

            _products.Remove(product);
            return true;
        }
    }
}
