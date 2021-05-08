using System;

namespace HungryNinja
{
    class Program
    {
        static void Main(string[] args)
        {
            Ninja Clarence = new Ninja();
            Buffet TooManyVegetables = new Buffet();
            while (!Clarence.isFull)
            {
            Clarence.Eat(TooManyVegetables.Serve());
            }

        }
    }
}  

