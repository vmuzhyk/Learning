using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            var movies = new List<Movie>
            {
                new Movie{ Tittle = "The Dirk Night", Rating =  8.9f, Year = 2008},
                new Movie{ Tittle = "The King's Speech", Rating =  8.9f, Year = 2010},
                new Movie{ Tittle = "Casablanca", Rating =  8.9f, Year = 1942},
                new Movie{ Tittle = "Star Wars V", Rating =  8.9f, Year = 1980}
            };
            var query = movies.Filter(m => m.Year > 2000);

            foreach (var movie in query)
            {
                Console.WriteLine(movie.Tittle);
            }
            Console.ReadKey();
        }
    }
}
