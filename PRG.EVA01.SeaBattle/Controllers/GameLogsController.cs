﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRG.EVA01.SeaBattle.Models;
using PRG.EVA01.anthony.mohamed1.Models;

namespace PRG.EVA01.anthony.mohamed1.Controllers
{
    public class GameLogsController : Controller
    {
        private readonly SeaBattleDbContext _context;

        public GameLogsController(SeaBattleDbContext context)
        {
            _context = context;
        }

        // GET: GameLogs
        // Retrieves all the game logs and displays them in the view
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameLogs.ToListAsync());
        }

        // GET: GameLogs/Details/5
        // Retrieves the details of a specific game log based on the provided ID
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameLog = await _context.GameLogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameLog == null)
            {
                return NotFound();
            }

            return View(gameLog);
        }

       


        // GET: GameLogs/Delete/5
        // Retrieves the details of a specific game log based on the provided ID for deletion
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameLog = await _context.GameLogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gameLog == null)
            {
                return NotFound();
            }

            return View(gameLog);
        }

        // POST: GameLogs/Delete/5
        // Deletes the specified game log from the database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameLog = await _context.GameLogs.FindAsync(id);
            if (gameLog != null)
            {
                _context.GameLogs.Remove(gameLog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameLogExists(int id)
        {
            return _context.GameLogs.Any(e => e.Id == id);
        }
    }
}
