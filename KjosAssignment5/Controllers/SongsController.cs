using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KjosAssignment5.Data;
using KjosAssignment5.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using System.CodeDom;
using Microsoft.IdentityModel.Tokens;

namespace KjosAssignment5.Controllers
{
    public class SongsController : Controller
    {
        private readonly KjosAssignment5Context _context;
        private static IEnumerable<Song> cart = new List<Song>();

        public SongsController(KjosAssignment5Context context)
        {
            _context = context;
        }

        // GET: Songs
        public async Task<IActionResult> Index(string songGenre, string songArtist)
        {
            if (_context.Song == null)
            {
                return Problem("erm...");
            }

            IQueryable<string> genreQuery = from m in _context.Song
                                            orderby m.Genre
                                            select m.Genre;
            IQueryable<string> artistQuery = from m in _context.Song
                                             orderby m.Artist
                                             select m.Artist;
            var songs = from m in _context.Song
                           select m;

            if (!string.IsNullOrEmpty(songGenre))
            {
                songs = songs.Where(s => s.Genre == songGenre);
                artistQuery = from m in _context.Song
                              where m.Genre == songGenre
                              orderby m.Artist
                              select m.Artist;
            }
            if (!string.IsNullOrEmpty(songArtist))
            {
                songs = songs.Where(s => s.Artist == songArtist);
            }

            var songGenreVM = new SongGenreViewModel
            {
                Artists = new SelectList(await artistQuery.ToListAsync()),
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Songs = await songs.ToListAsync()
            };
            return View(songGenreVM);
        }

        // GET: Admin
        public async Task<IActionResult> Admin(string songGenre, string songArtist)
        {
            if (_context.Song == null)
            {
                return Problem("erm...");
            }

            IQueryable<string> genreQuery = from m in _context.Song
                                            orderby m.Genre
                                            select m.Genre;
            IQueryable<string> artistQuery = from m in _context.Song
                                             orderby m.Artist
                                             select m.Artist;
            var songs = from m in _context.Song
                        select m;

            if (!string.IsNullOrEmpty(songGenre))
            {
                songs = songs.Where(s => s.Genre == songGenre);
                artistQuery = from m in _context.Song
                              where m.Genre == songGenre
                              orderby m.Artist
                              select m.Artist;
            }
            if (!string.IsNullOrEmpty(songArtist))
            {
                songs = songs.Where(s => s.Artist == songArtist);
            }

            var songGenreVM = new SongGenreViewModel
            {
                Artists = new SelectList(await artistQuery.ToListAsync()),
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Songs = await songs.ToListAsync()
            };
            return View(songGenreVM);
        }

        // GET: Cart
        public async Task<IActionResult> Cart()
        {
            var SongGenreVM = new SongGenreViewModel
            {
                Songs = cart.ToList()
            };
            return View(SongGenreVM);
        }

        // POST: AddCart
        [HttpPost]
        public IActionResult AddCart(string[] CartList)
        {
            IEnumerable<Song> songs = new List<Song>();
            foreach (string i in CartList)
            {
                songs = songs.Union(from m in _context.Song where m.Id == Convert.ToInt32(i) select m);
            }
            if (!songs.IsNullOrEmpty())
            {
                IEnumerable<Song> SongList = songs.ToList();
                if (!cart.IsNullOrEmpty())
                {
                    IEnumerable<Song> temp = cart;
                    SongList = SongList.Union(temp).DistinctBy(s => s.Id);
                }
                cart = SongList;
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Remove
        public async Task<IActionResult> Remove()
        {
            return View();
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Artist,Price")] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Admin));
            }
            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Artist,Price")] Song song)
        {
            if (id != song.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Admin));
            }
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Song.FindAsync(id);
            if (song != null)
            {
                _context.Song.Remove(song);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Admin));
        }

        private bool SongExists(int id)
        {
            return _context.Song.Any(e => e.Id == id);
        }
    }
}
