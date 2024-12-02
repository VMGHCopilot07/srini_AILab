using System;
using System.Collections.Generic;

public class ProductManager
{
    private List<Product> products = new List<Product>();

    public void AddProduct(Product product)
    {
        if (product == null)
        {
            throw new ArgumentException("Product cannot be null");
        }
        products.Add(product);
    }

    public List<Product> GetProductsByCategory(string category)
    {
        List<Product> result = new List<Product>();
        foreach (var product in products)
        {
            if (product.Category == category)
            {
                result.Add(product);
            }
        }
        return result;
    }

    public double GetTotalPrice()
    {
        double total = 0;
        foreach (var product in products)
        {
            total += product.Price;
        }
        return total;
    }

    public List<string> GetProductNames()
    {
        List<string> names = new List<string>();
        foreach (var product in products)
        {
            names.Add(product.Name);
        }
        return names;
    }
}

public class Product
{
    public string Name { get; set; }
    public string Category { get; set; }
    public double Price { get; set; }
}

public class Program
{
    public static void Main()
    {
        var productManager = new ProductManager();
        productManager.AddProduct(new Product { Name = "Laptop", Category = "Electronics", Price = 1000 });
        productManager.AddProduct(new Product { Name = "Phone", Category = "Electronics", Price = 500 });
        productManager.AddProduct(new Product { Name = "Shirt", Category = "Clothing", Price = 50 });

        var electronics = productManager.GetProductsByCategory("Electronics");
        Console.WriteLine("Electronics:");
        foreach (var product in electronics)
        {
            Console.WriteLine(product.Name);
        }

        double totalPrice = productManager.GetTotalPrice();
        Console.WriteLine($"Total Price: {totalPrice}");

        var productNames = productManager.GetProductNames();
        Console.WriteLine("Product Names:");
        foreach (var name in productNames)
        {
            Console.WriteLine(name);
        }
    }
}
