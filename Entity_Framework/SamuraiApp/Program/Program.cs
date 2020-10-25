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
        //private static SamuraiContext _context = new SamuraiContext();
        static void Main(string[] args)
        {
            //_context.Database.EnsureCreated();
            // GetSamurais("Before Add:");
            // GetSamurais("After Add:");
            InsertMultipleSamurais();
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
            //LazyLoadQuotes();
            //FilteringWithRelatedData();
            //ModifyingRelatedDataWhenTracked();
            //ModifyingRelatedDataWhenNotTracked();
            //JoinBattleAndSamurai();
            //EnlistSamuraiIntoBattle();
            //GetSamuraiWithBattles();
            //AddNewSamuraiWithHorse();
            //AddNewSamuraiWithHorseToSamuraiUsingId();
            //AddNewSamuraiWithHorseToSamuraiObject();
            //AddNewHorseToDisconectedSamuraiObject();
            //ReplaceAHorce();
            //GetHorseWithSamurai();
            //GetClanWithSamurais();
            //QuerySamuraiBattleStats();
            //QueryUsingRawSql();
            //QueryUsingRawSqlWithInterpolation();
            //QueryUsingRawSqlFromRawSqlStoredProc();
            //ExecuteSomeRawSql();

            Console.ReadKey();
        }

        /*private static void ExecuteSomeRawSql()
        {
            var samuraiId = 22;
            //var x = _context.Database
            //.ExecuteSqlRaw("EXEC DeleteQuotesForSamurai {0}", samuraiId);
            samuraiId = 17;
            var x = _context.Database
            .ExecuteSqlInterpolated($"EXEC DeleteQuotesForSamurai {samuraiId}");
        }

        private static void QueryUsingRawSqlFromRawSqlStoredProc()
        {
            var text = "Happy";
            var samurais = _context.Samurais.FromSqlRaw(
                "EXEC dbo.SamuraisWhoSaidWord {0}", text).ToList();
        }

        private static void QueryUsingRawSqlWithInterpolation()
        {
            string name = "Mika";
            var samurais = _context.Samurais
                .FromSqlInterpolated($"Select * from Samurais Where Name = {name}")
                .ToList();
        }

        private static void QueryUsingRawSql()
        {
            //var samurais = _context.Samurais.FromSqlRaw("Select * from Samurais").ToList();
            var samurais = _context.Samurais.FromSqlRaw(
                "Select Id, Name, ClanId from Samurais").Include(s => s.Quotes).ToList();
        }

        private static void QuerySamuraiBattleStats()
        {
            //var stats = _context.SamuraiBattleStats.ToList();
            var firstStat = _context.SamuraiBattleStats.FirstOrDefault();
            var JasperStat =  _context.SamuraiBattleStats
                                .Where( s => s.Name == "JasperSan").FirstOrDefault();
        }

        private static void GetClanWithSamurais()
        {
            //var clan = _context.Clans.Include(c => c.Id???);
            var clan = _context.Clans.Find(3);
            var samuraisForClan = _context.Samurais.Where( s => s.Clan.Id == 3).ToList();
        }

        private static void GetHorseWithSamurai()
        {
            var horseWithoutSamurai = _context.Set<Horse>().Find(3);

            var horseWithSamurai = _context.Samurais.Include(s => s.Horse)
            .FirstOrDefault(s => s.Horse.Id == 3);

            var horsesWithSamurais = _context.Samurais
            .Where(s => s.Horse != null)
            .Select(s => new{ Horse = s.Horse, Samurai = s})
            .ToList();
        }

        private static void ReplaceAHorce()
        {
            //var samurai = _context.Samurais.Include(s => s.Horse).FirstOrDefault(s => s.Id == 17);
            var samurai = _context.Samurais.Find(1);
            samurai.Horse = new Horse{Name = "Trigger"};
            _context.SaveChanges();
        }

        private static void AddNewHorseToDisconectedSamuraiObject()
        {
            var samurai = _context.Samurais.AsNoTracking().FirstOrDefault(s => s.Id == 17);
            samurai.Horse = new Horse{ Name = "Mr. Ed"};
            using (var newContext = new SamuraiContext())
            {
                newContext.Attach(samurai);
                newContext.SaveChanges();
            }
        }

        private static void AddNewSamuraiWithHorseToSamuraiObject()
        {
            var samurai = _context.Samurais.Find(19);
            samurai.Horse = new Horse{ Name = "Black Beauty" };
            _context.SaveChanges();
        }

        private static void AddNewSamuraiWithHorseToSamuraiUsingId()
        {
            var horse = new Horse { Name = "Scout", SamuraiId = 2};
            _context.Add(horse);
            _context.SaveChanges();
        }

        private static void AddNewSamuraiWithHorse()
        {
            var samurai = new Samurai { Name = "Jina Ujichka" };
            samurai.Horse = new Horse { Name = "Silver" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void GetSamuraiWithBattles()
        {
            var samuraiWithBattle = _context.Samurais
            .Include(s => s.SamuraiBattles)
            .ThenInclude(sb => sb.Battle)
            .FirstOrDefault(samurai => samurai.Id == 2);

            var battleName = samuraiWithBattle.SamuraiBattles.FirstOrDefault().Battle.Name;

            var samuraiWithBattlesCleaner = _context.Samurais.Where(s => s.Id ==2)
            .Select(s => new
            {
                Samurai = s,
                Battles = s.SamuraiBattles.Select(sb => sb.Battle)
            })
            .FirstOrDefault();

            var battleName2 = samuraiWithBattlesCleaner.Battles.FirstOrDefault().Name;
        }

        private static void EnlistSamuraiIntoBattle()
        {
            var battle = _context.Battles.Find(1);
            battle.SamuraiBattles.Add(new SamuraiBattle{SamuraiId = 21});
            _context.SaveChanges();
        }

        private static void JoinBattleAndSamurai()
        {
            var sbJoin = new SamuraiBattle {SamuraiId = 1, BattleId = 3};
            _context.Add(sbJoin);
            _context.SaveChanges();
        }

        private static void ModifyingRelatedDataWhenNotTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 0);
            var quote = samurai.Quotes[0];
            quote.Text = " Did you hear that?";
            using (var newContext = new SamuraiContext())
            {
                newContext.Entry(quote).State =EntityState.Modified;
                //newContext.Quotes.Update(quote);
                _context.SaveChanges();
            }
        }

        private static void ModifyingRelatedDataWhenTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 2);
            //samurai.Quotes[0].Text = " Did you hear that?";
            _context.Quotes.Remove(samurai.Quotes[0]);
            _context.SaveChanges();
        }

        private static void FilteringWithRelatedData()
        {
            var samurais = _context.Samurais
                                    .Where(s => s.Quotes.Any(q => q.Text.Contains("happy")))
                                    .ToList();
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
        }*/

        private static void InsertMultipleSamurais()
        {
            /*var samurai = new Samurai { Name = "Liam"};
            var samurai2 = new Samurai { Name = "Wisdom"};
            var samurai3 = new Samurai { Name = "Poppy"};
            var samurai4 = new Samurai { Name = "Malcolm"};*/
            var _bizdata = new BusinnesDataLogic();
            var samuraiNames = new string[] { "Liam", "Wisdom", "Poppy", "Malcolm" };
            var newSamuraisCreated = _bizdata.AddMultipleSamurais(samuraiNames);
        }

        /*private static void GetSamurais(string text)
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
        }*/
    }
}
