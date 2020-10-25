using Program;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;


namespace Tests
{
    [TestClass]
    class BizDataLogicTests
    {
        [TestMethod]
        public void AddMultipleSamuraisReturnsCorrectNumberOfInsertedRows()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("AddMultipleSamurais");
            using (var context = new SamuraiContext(builder.Options))
            {
                var bizlogic = new BusinnesDataLogic(context);
                var nameList = new string[] { "Kikuchiyo", "Kyuzo", "Rikchi" };
                var result = bizlogic.AddMultipleSamurais(nameList);
                Assert.AreEqual(nameList.Count(), result);
            }
        }

        [TestMethod]
        public void CanInsertSingleSamurai()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("InsertNewSamurai");

            using (var context = new SamuraiContext(builder.Options))
            {
                var bizlogic = new BusinnesDataLogic(context);
                bizlogic.InsertNewSamurai(new Samurai());
            };
            using (var context2 = new SamuraiContext(builder.Options))
            {
                Assert.AreEqual(1, context2.Samurais.Count());
            }
        }

        [TestMethod]
        public void CanInsertSamuraiWithQuotes()
        {
            var samuraiGraph = new Samurai
            {
                Name = "Kyuzo",
                Quotes = new List<Quote>
                {
                    new Quote { Text = "Watch out of my sharp sword!" },
                    new Quote { Text = "I told you to watch out of my sharp sword!" }
                }
            };
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("SamuraiWithQuotes");
            using(var context = new SamuraiContext(builder.Options))
            {
                var bizlogic = new BusinnesDataLogic(context);
                var result = bizlogic.InsertNewSamurai(samuraiGraph);
            }
            using(var context2 = new SamuraiContext(builder.Options))
            {
                var samuraiWithQuotes = context2.Samurais.Include(s => s.Quotes).FirstOrDefault();
                Assert.AreEqual(2, samuraiWithQuotes.Quotes.Count);
            }
            }
        }
    }
}
