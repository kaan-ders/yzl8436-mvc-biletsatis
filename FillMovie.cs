using static System.Net.WebRequestMethods;
using Flurl.Http;
using System.Text.Json;
using BiletSatis.Models;

namespace BiletSatis
{
    public class FillMovie
    {
        public void Run(TicketDbContext dbContext)
        {
            //Movie tablosu içerisinde herhangi bir kayıt yoksa
            if(dbContext.Movies.Any() == false)
            {
                //themoviedb'ye bağlan, oradaki listeyi al ve dbye yaz

                string url = "https://api.themoviedb.org/3/movie/now_playing?api_key=837fd3c1b9dffbbe7b69a4a979ab2fe3";
                var list = url.GetStringAsync().Result;
                var movies = JsonSerializer.Deserialize<MovieDbListDto>(list);
                
                foreach (var movie in movies.results)
                {
                    Movie dbMovie = new Movie
                    {
                        MovieDbId = movie.id,
                        Overview = movie.overview,
                        Poster = "https://image.tmdb.org/t/p/w500" + movie.poster_path,
                        Title = movie.title,
                        IsDeleted = false,
                    };

                    dbContext.Movies.Add(dbMovie);
                }

                dbContext.SaveChanges();
            }
        }
    }

    public class MovieDbListDto
    {
        public List<MovieDbDto> results { get; set; }
    }

    public class MovieDbDto
    {
        public int id { get; set; }
        public string overview { get; set; }
        public string title { get; set; }
        public string poster_path { get; set; }
    }
}