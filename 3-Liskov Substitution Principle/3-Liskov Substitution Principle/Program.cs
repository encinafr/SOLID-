using System;
using static System.Console;

namespace _3_Liskov_Substitution_Principle
{
    //Si para cada objeto o1 de tipo S hay un objeto o2 de tipo T
    //tal que para todos los programas P definidos en términos de T, 
    //el comportamiento de P no cambia cuando o1 es sustituido por o2,
    // entonces S es un subtipo de T.
    public class Rectangle
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {

        }
        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        override public int Width
        {
            set { base.Width = base.Height = value; } 
        }

        override public int Height
        {
            set { base.Width = base.Height = value; }
        }
    }
    public class Program
    {
        static public int Area(Rectangle r) => r.Width * r.Height;  
        static void Main(string[] args)
        {
            Rectangle rc = new Rectangle(2,3);
            WriteLine($"{rc} has area {Area(rc)}");

            Rectangle sq = new Square();
            sq.Width = 4;

            WriteLine($"{sq} has area {Area(sq)}");


        }
    }
}
