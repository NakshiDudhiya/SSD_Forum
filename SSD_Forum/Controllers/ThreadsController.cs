using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SSD_Forum.Data;
using SSD_Forum.Models;

namespace SSD_Forum.Controllers
{
    public class ThreadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThreadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Threads
        public async Task<IActionResult> Index()
        {
            return View(await _context.Threads.ToListAsync());
        }

        // GET: Threads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thread = await _context.Threads
                .FirstOrDefaultAsync(m => m.ThreadId == id);
            if (thread == null)
            {
                return NotFound();
            }

            return View(thread);
        }

        // GET: Threads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Threads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("ThreadId,UserWhoUploaded,Title,Content,DateUploaded")] Thread thread)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thread);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(thread);
        }

        // GET: Threads/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thread = await _context.Threads.FindAsync(id);
            if (thread == null)
            {
                return NotFound();
            }
            return View(thread);
        }

        // POST: Threads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("ThreadId,UserWhoUploaded,Title,Content,DateUploaded")] Thread thread)
        {
            if (id != thread.ThreadId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thread);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThreadExists(thread.ThreadId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(thread);
        }

        // GET: Threads/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var thread = await _context.Threads
                .FirstOrDefaultAsync(m => m.ThreadId == id);
            if (thread == null)
            {
                return NotFound();
            }

            return View(thread);
        }

        // POST: Threads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var thread = await _context.Threads.FindAsync(id);
            _context.Threads.Remove(thread);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThreadExists(int id)
        {
            return _context.Threads.Any(e => e.ThreadId == id);
        }
    }
}
