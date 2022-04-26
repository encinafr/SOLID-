using System;
using System.Collections;
using System.Collections.Generic;
using static System.Console;

namespace _2_Open_Closed_Principle
{

    public enum Color
    {
        Red, Green, Color
    }

    public enum Size
    {
        Small, Medium, Large, Yuge
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if (name == null)
                throw new ArgumentNullException(paramName: nameof(name));

            Name = name;
            Color = color;
            Size = size;
        }

    }

    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var p in products)
            {
                if (p.Size == size)
                    yield return p;
            }
        }

        public  IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
            {
                if (p.Color == color)
                    yield return p;
            }
        } 
        
        public  IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
        {
            foreach (var p in products)
            {
                if (p.Size == size && p.Color == color)
                    yield return p;
            }
        }

    }

    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Color == color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;

        public SizeSpecification(Size size)
        {
            this.size = size;
        }
        public bool IsSatisfied(Product t)
        {
            return t.Size == size;
        }
    }

    public class AndSpecification<T> : ISpecification<T>
    {
        private  List<ISpecification<T>> specifications;

        public AndSpecification(List<ISpecification<T>> specifications)
        {
            if (specifications == null)
                throw new ArgumentNullException(paramName: nameof(specifications));

            this.specifications = specifications;
        }

        public bool IsSatisfied(T t)
        {
            bool isSatisfied = true;

            foreach (var s in specifications)
            {
                if (!s.IsSatisfied(t))
                    isSatisfied = false;
            } 

            return isSatisfied;
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
            {
                if (spec.IsSatisfied(i))
                    yield return i;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Red, Size.Large);

            Product[] products = { apple, tree, house };

            var pf = new ProductFilter();

            WriteLine("Green products (old):");

            var pdar = pf.FilterByColor(products, Color.Green);

            foreach (var p in pdar)
            {
                WriteLine($" - {p.Name} is green");
            }

            var bf = new BetterFilter();

            WriteLine("Green products (new):");

            foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
            {
                WriteLine($" - {p.Name} is green");
            }

            WriteLine("Large blue items:");

            var specifications = new List<ISpecification<Product>>() { new ColorSpecification(Color.Red),
                                                                       new SizeSpecification(Size.Large) };

            foreach (var p in bf.Filter(products, new AndSpecification<Product>(specifications)))
            {
                WriteLine($" - {p.Name} is big and red");
            }
        }
    }
}
