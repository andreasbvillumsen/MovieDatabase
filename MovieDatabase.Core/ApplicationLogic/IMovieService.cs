using MovieDatabase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDatabase.Core.ApplicationLogic
{
    interface IMovieService
    {
        public Movie Create(Movie movie);
        public List<Movie> Read();
        public Movie Read(int id);
        public Movie Update(int id, Movie movie);
        public Movie Delete(int id);
    }
}
