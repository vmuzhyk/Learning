using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace Program
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();
        static void Main(string[] args)
        {
            //_context.Database.EnsureCreated();
            // GetSamurais("Before Add:");
            // GetSamurais("After Add:");
             //InsertMultipleSamurais();
            // Console.Write("Press any key...");
            //QueryFilters();
            //RetreiveAndUpdateSamurai();
            //RetreiveAndUpdateMultipleSamurais();
            //MultipleDatabaseOperation();
            InsertBattle();
            QueryAndUpdateBattleDisconected();
            
            Console.ReadKey();
        }
        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Liam"};
            var samurai2 = new Samurai { Name = "Wisdom"};
            var samurai3 = new Samurai { Name = "Poppy"};
            var samurai4 = new Samurai { Name = "Malcolm"};
            _context.Samurais.AddRange(samurai, samurai2, samurai3, samurai4);
            _context.SaveChanges();
        }

        private static void GetSamurais(string text)
        {
            var samurais = _context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
            foreach(var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void QueryFilters() 
        {
            var name = "Mika";
            var samurais = _context.Samurais.Where(s => s.Name == name).ToList();
        }

        private static void RetreiveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }

        private static void RetreiveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.Skip(3).Take(4).ToList();
            samurais.ForEach(s => s.Name += "San");
            _context.SaveChanges();
        }

        private static void MultipleDatabaseOperation()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.Samurais.Add(new Samurai{Name = "Kilon"});
            _context.SaveChanges();
        }

        private static void InsertBattle()
        {
            _context.Battles.Add( new Battle
            {
                Name = "Battle of Okehazama",
                StartDate = new DateTime(1560, 05, 01),
                EndDate = new DateTime(1560, 06, 15)
            });
            _context.SaveChanges();
        }

        private static void QueryAndUpdateBattleDisconected()
        {
            var battle = _context.Battles.AsNoTracking().FirstOrDefault();
            battle.EndDate = new DateTime(1560, 06, 30);
            using (var newContextInstance = new SamuraiContext())
            {
                newContextInstance.Battles.Update(battle);
                newContextInstance.SaveChanges();
            }
        }
    }
}
