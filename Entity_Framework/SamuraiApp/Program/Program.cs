using System;
using System.Collections.Generic;
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
            //InsertBattle();
            //QueryAndUpdateBattleDisconected();
            //InsertNewSamuraiWithQuote();
            //InsertNewSamuraiWithManyQuote();
            //AddQuoteToExistingSamuraiWhileTracked();
            //AddQuoteToExistingSamuraiIsNoTracked_Easy(3);
            //EagerLoadSamuraiWithQuotes();
            //ProjectSomeProperties();
            //ProjectSamuraisWithQuotes();
            //ExplicitLoadQuotes();
            LazyLoadQuotes();
            
            Console.ReadKey();
        }

        private static void LazyLoadQuotes()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name.Contains("Liam"));
            var quoteCount = samurai.Quotes.Count();
        }

        private static void ExplicitLoadQuotes()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name.Contains("Kyuzo"));
            _context.Entry(samurai).Collection(s => s.Quotes).Load();
            _context.Entry(samurai).Reference(s => s.Horse).Load();
        }

        private static void ProjectSamuraisWithQuotes()
        {
            // var somePropertiesWithQuotes = _context.Samurais
            // .Select(s => new { s.Id, s.Name, 
            // HappyQuotes = s.Quotes.Where(q => q.Text.Contains("happy")) })
            // .ToList();

            var samuraisWithHappyQuotes = _context.Samurais
            .Select(s => new { 
                Samurai = s, 
            HappyQuotes = s.Quotes.Where(q => q.Text.Contains("happy")) 
            })
            .ToList();
            var firstsamurai = samuraisWithHappyQuotes[0].Samurai.Name += "The Happiest";
        }

        private static void ProjectSomeProperties()
        {
            var someProperties = _context.Samurais.Select(s => new {s.Id, s.Name}).ToList();
            var idsAndNames = _context.Samurais.Select(s => new IdAndName(s.Id, s.Name)).ToList();
        }

        public struct IdAndName
        {
            public IdAndName (int id, string name)
            {
                Id = id;
                Name = name;
            }
            public int Id;
            public string Name;
        }

        private static void EagerLoadSamuraiWithQuotes()
        {
            var samuraiWithQuotes = _context.Samurais.Include(s => s.Quotes).ToList();
        }

        private static void AddQuoteToExistingSamuraiIsNoTracked_Easy(int samuraiId)
        {
            var quote = (new Quote
            {
                Text = "Now that I saved you, will you feed me dinner again?",
                SamuraiId = samuraiId
            });
            using (var newContext = new SamuraiContext())
            {
                newContext.Quotes.Update(quote);
                newContext.SaveChanges();
            }
        }

        private static void AddQuoteToExistingSamuraiWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Quotes.Add(new Quote
            {
                Text = "I bet you're happy that I've saved you!"
            });
            _context.SaveChanges();
        }

        private static void InsertNewSamuraiWithManyQuote()
        {
            var samurai = new Samurai
            {
                Name = "Kyuzo",
                Quotes = new List<Quote>
                {
                    new Quote{ Text = "Watch out for my sharp sword!" },
                    new Quote{ Text = "I told you to watch out for the sharp sword! Oh well!"}
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
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

        private static void InsertNewSamuraiWithQuote()
        {
            var samurai = new Samurai
            {
                Name = "Kambei Shimada",
                Quotes = new List<Quote>
                {
                    new Quote{ Text = "I've come to save you" }
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
    }
}
