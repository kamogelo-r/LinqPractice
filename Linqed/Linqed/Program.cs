using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linqed
{
    class Program 
    {
        delegate double CalculateAreaPointer(int r);
        //static CalculateAreaPointer pointer = CalculateArea;
        static void Main(string[] args)
        {
            #region inline delegate
            //CalculateAreaPointer pointer = new CalculateAreaPointer(delegate (int r)
            //{
            //    return 3.14 * r * r;
            //});
            //double area = pointer(4); 
            #endregion

            //CalculateAreaPointer pointer = r => 3.14 * r * r;
            //double area = pointer(4);

            QueryAnimalData();

            Func<Double, Double> pointer = r => 3.14 * r * r;
            double area = pointer(4);

            Console.WriteLine(area);


            #region LINQ method calls
            //QueryStringArray();
            //QueryIntArray();
            //QueryArrayList();
            //QueryCollection();
            //QueryAnimalData();
            #endregion

            Console.Read();
        }

        public static void QueryStringArray()
        {
            string[] dogs = {"K9", "Brian Griffin", "Scooby Doo", "Old Yeller", "Rin Tin Tin", "Benji",
                             "Charlie B. Barkin", "Lassie", "Snoopy"};

            var dogSpaces = from dog in dogs
                            where dog.Contains(" ")
                            orderby dog ascending
                            select dog;

            foreach (var item in dogSpaces)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        public static int[] QueryIntArray()
        {
            int[] numbers = { 5, 10, 15, 20, 25, 30, 35 };

            var gt20 = from nums in numbers
                       where nums > 20
                       orderby nums ascending
                       select nums;

            #region alternative to the gt20
            var list = numbers.Select(n => n).ToList();

            //Console.WriteLine("List Items");
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion

            foreach (var item in gt20)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            Console.WriteLine($"Get type: {gt20.GetType()}");

            #region converting from list to array
            //var listGt = gt20.ToList<int>();
            //var arrayGt20 = gt20.ToArray();
            #endregion

            numbers[0] = 40; //adding item to array

            foreach (var item in gt20)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
            return numbers;
        }

        public static void QueryArrayList()
        {
            ArrayList farmAnimals = new ArrayList()
            {
                new Animal
                {
                    Name = "Jovi",
                    Height = 0.8,
                    Weight = 18
                },

                new Animal
                {
                    Name = "Bon",
                    Height = 1.8,
                    Weight = 15
                },

                new Animal
                {
                    Name = "Cave",
                    Height = 0.89,
                    Weight = 13
                },
            };

            var farmAnimalsEnum = farmAnimals.OfType<Animal>(); //give arrayList a type
            var smallAnimals = from animal in farmAnimalsEnum
                               where animal.Weight <= 90
                               orderby animal.Name
                               select animal;

            foreach (var item in smallAnimals)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }

        public static void QueryCollection()
        {
            var animalList = new List<Animal>()
            {
                new Animal
                {
                    Name = "German Shepherd",
                    Height = 7.8,
                    Weight = 18
                },

                new Animal
                {
                    Name = "Jack Russell",
                    Height = 1.8,
                    Weight = 50
                },

                new Animal
                {
                    Name = "Saint Bernard",
                    Height = 0.89,
                    Weight = 130
                },
            };

            var bigDogs = from dog in animalList
                          where (dog.Weight < 70) && (dog.Height < 25)
                          orderby dog.Name
                          select dog;

            foreach (var item in bigDogs)
            {
                Console.WriteLine("Dog Breed: " + item);
            }
            Console.WriteLine();
        }

        public static void QueryAnimalData()
        {
            Animal[] animals = new[]
            {
                new Animal
                {
                    Name = "Poodle",
                    Height = 7.8,
                    Weight = 18,
                    AnimalId = 1
                },

                new Animal
                {
                    Name = "Chihuahua",
                    Height = 1.8,
                    Weight = 50,
                    AnimalId = 2
                },

                new Animal
                {
                    Name = "Pug",
                    Height = 0.89,
                    Weight = 130,
                    AnimalId = 3
                },

                 new Animal
                {
                    Name = "German Shepherd",
                    Height = 7.8,
                    Weight = 18,
                    AnimalId = 1
                },

                new Animal
                {
                    Name = "Jack Russell",
                    Height = 1.8,
                    Weight = 50,
                    AnimalId = 2
                },

                new Animal
                {
                    Name = "Saint Bernard",
                    Height = 0.89,
                    Weight = 130,
                    AnimalId = 3
                },
            };

            Owner[] owners = new[]
            {
                new Owner {Name = "Sally", OwnerId = 2},
                new Owner {Name = "Harry", OwnerId = 1},
                new Owner {Name = "Barry", OwnerId = 3},
            };

            var nameHeight = from a in animals
                             select new
                             {
                                 a.Name,
                                 a.Height
                             }; //return only name and height (specifying columns)

            var height = from a in animals
                         select new
                         {
                             Name = a.Name,
                             Height = a.Height
                         };

            Array arrNameHeight = nameHeight.ToArray();

            foreach (var item in nameHeight)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("/////////////////////////////////////");
            foreach (var item in height)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("/////////////////////////////////////");

            var innerJoin = from animal in animals
                            join owner in owners on animal.AnimalId
                            equals owner.OwnerId
                            select new
                            {
                                OwnerName = owner.Name,
                                AnimalName = animal.Name
                            };

            foreach (var item in innerJoin)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            var groupJoin = from owner in owners
                            orderby owner.OwnerId
                            join animal in animals on owner.OwnerId
                            equals animal.AnimalId
                            into ownerGroup
                            select new
                            {
                                Owner = owner.Name,
                                Animals = from owner2
                                          in ownerGroup
                                          orderby owner2.Name
                                          select owner2
                            };

            int totalAnimals = 0;

            foreach (var item in groupJoin)
            {
                Console.WriteLine(item.Owner);
                foreach (var items in item.Animals)
                {
                    totalAnimals++;
                    Console.WriteLine(totalAnimals +" "+ items.Name);                    
                }
                Console.WriteLine();
            }
        }

        //public static double CalculateArea(int r)
        //{
        //    return 3.14 * r * r;
        //}

    }
}
