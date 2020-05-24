using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    public class Movie
    {
        public string Tittle { get; set; }
        public float Rating { get; set; }
        int _year;
        public int Year 
        { get 
            {
                Console.WriteLine($"Returning {_year} for {Tittle}");
                return _year;
            }
          set 
            {
                _year = value;
            } 
        }
    }
}
