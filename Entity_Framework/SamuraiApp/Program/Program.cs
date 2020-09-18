using System;
using System.Linq;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace Program
{
    class Program
    {
        private static SamuraiContext context = new SamuraiContext();
        static void Main(string[] args)
        {
            context.Database.EnsureCreated();
            // GetSamurais("Before Add:");
            // GetSamurais("After Add:");
            InsertMultipleSamurais();
            Console.Write("Press any key...");
            Console.ReadKey();
        }
        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Jasper"};
            var samurai2 = new Samurai { Name = "Garlem"};
            var samurai3 = new Samurai { Name = "Mika"};
            var samurai4 = new Samurai { Name = "Kirby"};
            context.Samurais.AddRange(samurai, samurai2, samurai3, samurai4);
            context.SaveChanges();
        }

        private static void GetSamurais(string text)
        {
            var samurais = context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
            foreach(var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }
    }
}
