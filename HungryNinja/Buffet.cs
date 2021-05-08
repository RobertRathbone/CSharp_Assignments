using System;
using System.Collections.Generic;

namespace HungryNinja
{
    public class Food
    {
        public string Name;
        public int Calories;
        public bool IsSpicy;
        public bool IsSweet;

        public Food(string name, int calories, bool isSpicy, bool isSweet)
        {
            Name = name;
            Calories = calories;
            IsSpicy = isSpicy;
            IsSweet = isSweet;
        }

    }

    public class Buffet
    {
        public List<Food> menu;

        public Buffet()
        {
            menu = new List<Food>()
            {
                new Food("Potato", 312, false, false),
                new Food("Turnip", 88, false, false),
                new Food("Eggs", 215, false, false),
                new Food("Brown Rice", 310, false, false),
                new Food("Brussel Sprouts", 180, false, false),
                new Food("Celery", 7, false, false),
                new Food("Parsnips", 80, false, false)
            };
        }
        public Food Serve()
        {
            Random rand = new Random();
            int y = rand.Next(0, 6);
            return menu[y];

        }
    }

    public class Ninja
    {
        private int calorieIntake;
        public List<Food> FoodHistory;
        public bool isFull
        {
            get
            {
                if (calorieIntake > 1800)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }
        public Ninja()
        {
            calorieIntake = 0;
            FoodHistory = new List<Food>();
        }



        public void Eat(Food item)
        {
            FoodHistory.Add(item);
            calorieIntake += item.Calories;
            Console.WriteLine(item.Name, " ", calorieIntake);
            if (item.IsSpicy)
            {
            Console.Write("It's a spicy", item.IsSpicy);
            }
            if (item.IsSweet)
            {
            Console.Write("It's a sweet", item.IsSweet);
            }
        }
    }
}
