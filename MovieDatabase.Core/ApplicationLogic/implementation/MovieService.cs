using MovieDatabase.Core.DomainLogic;
using MovieDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDatabase.Core.ApplicationLogic.implementation
{
    public class MovieService : IMovieService
    {
        private IMovieRepository _repo;

        public MovieService(IMovieRepository repo)
        {
            _repo = repo;
        }

        public Movie Create(Movie movie)
        {
            return _repo.Create(movie);
        }

        public Movie Delete(int id)
        {
            return _repo.Delete(id);
        }

        public List<Movie> Read()
        {
            return _repo.Read();
        }

        public Movie Read(int id)
        {
            return _repo.Read(id);
        }

        public Movie Update(int id, Movie movie)
        {
            return _repo.Update(id, movie);
        }
    }
}
