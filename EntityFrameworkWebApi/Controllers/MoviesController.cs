using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EntityFrameWorkWebApi.Models;
using EntityFrameWorkWebApi.DTO;
using System.Linq.Expressions;

namespace EntityFrameWorkWebApi.Controllers
{
    [RoutePrefix("api/movies")]
    public class MoviesController : ApiController
    {
        private EntityFrameWorkWebApiContext db = new EntityFrameWorkWebApiContext();

        // return MovieDTO instance, instead of Movie instance
        private static readonly Expression<Func<Movie, MovieDTO>> AsMovieDTO = x => new MovieDTO
        {
            Title = x.Title,
            Genre = x.Genre.Name
        };
        // return MovieDetialDTO instance, instead of Movie instance
        private static readonly Expression<Func<Movie, MovieDetailDTO>> AsMovieDetailDTO = x => new MovieDetailDTO
        {
            Title = x.Title,
            Genre = x.Genre.Name,
            Description = x.Description
        };

        // GET: api/
        [Route("")]
        public IQueryable<MovieDTO> GetMovies()
        {
            // return original Movie instance
            //return db.Movies;

            // return MovieDTO instance
            return db.Movies.Include(m => m.Genre).Select(AsMovieDTO);
        }

        [Route("details")]
        [ResponseType(typeof(MovieDetailDTO))]
        public IQueryable<MovieDetailDTO> GetMovisWithDetail()
        {
            return db.Movies.Include(m => m.Genre).Select(AsMovieDetailDTO);
        }

        [Route("{genre}")]
        [ResponseType(typeof(MovieDetailDTO))]
        public IQueryable<MovieDetailDTO> GetMoviesByGenre(string genre)
        {
            return db.Movies.Include(m => m.Genre)
                .Where(m => m.Genre.Name.Equals(genre, StringComparison.OrdinalIgnoreCase))
                .Select(AsMovieDetailDTO);
        }


        // GET: api/Movies/5
        [Route("{id:int}")]
        [ResponseType(typeof(MovieDetailDTO))]
        public async Task<IHttpActionResult> GetMovie(int id)
        {
            // retun Movie instance
            //Movie movie = await db.Movies.FindAsync(id);
            //if (movie == null)
            //{
            //    return NotFound();
            //}

            //return Ok(movie);

            // return MovieDTO
            //MovieDTO movie = await db.Movies.Include(m => m.Genre)
            //    .Where(m => m.MovieId == id)
            //    .Select(AsMovieDTO)
            //    .FirstOrDefaultAsync();
            //if (movie == null)
            //{
            //    return NotFound();
            //}
            //return Ok(movie);

            MovieDetailDTO movie = await db.Movies.Include(m => m.Genre)
                .Where(m => m.MovieId == id)
                .Select(AsMovieDetailDTO)
                .FirstOrDefaultAsync();

            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }









        // PUT: api/Movies/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMovie(int id, Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movie.MovieId)
            {
                return BadRequest();
            }

            db.Entry(movie).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Movies
        [ResponseType(typeof(Movie))]
        public async Task<IHttpActionResult> PostMovie(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Movies.Add(movie);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = movie.MovieId }, movie);
        }

        // DELETE: api/Movies/5
        [ResponseType(typeof(Movie))]
        public async Task<IHttpActionResult> DeleteMovie(int id)
        {
            Movie movie = await db.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            db.Movies.Remove(movie);
            await db.SaveChangesAsync();

            return Ok(movie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovieExists(int id)
        {
            return db.Movies.Count(e => e.MovieId == id) > 0;
        }
    }
}