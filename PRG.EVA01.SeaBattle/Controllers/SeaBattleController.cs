using Microsoft.AspNetCore.Mvc;
using PRG.EVA01.anthony.mohamed1.Models;
using PRG.EVA01.Anthony.Mohamed.Models;
using PRG.EVA01.SeaBattle.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PRG.EVA01.Anthony.Mohamed.Controllers
{
    public class SeaBattleController : Controller
    {
        private Game _game;
        private readonly SeaBattleDbContext _context;

        // Constructor to initialize the SeaBattleController with dependencies
        public SeaBattleController(SeaBattleDbContext context)
        {
            // Assigning the provided SeaBattleDbContext instance to the private _context member
            _context = context;

            // Initializing the _game object with default values
            _game = new Game
            {
                Id = 1,
                Boats = new List<Boat>
                {
                    new Boat { location = new Location { Letter = "A", Number = "2" }, status = Status.Active },
                    new Boat { location = new Location { Letter = "C", Number = "4" }, status = Status.Active },
                    new Boat { location = new Location { Letter = "D", Number = "5" }, status = Status.Active }
                },
                PlayerName = "Player1",
                StartedPlayingOn = DateTime.Now
            };

            // Load boats asynchronously
            LoadBoats();
        }

        // Action method to handle throwing bombs in the Sea Battle game
        public async Task<IActionResult> ThrowBomb(string letter, string number)
        {
            bool hit = false;
            bool invalid = false;

            // Validate the input letter and number
            if (!IsValidLetter(letter) || !IsValidNumber(number))
            {
                invalid = true;


            }

            // Convert letter to uppercase for uniform comparison
            string upletter = letter.ToUpper();

            // Check if the bomb hit any boat
            foreach (var boat in _game.Boats)
            {
                if (boat.location.Letter == upletter && boat.location.Number == number)
                {
                    boat.status = Status.Sunk;
                    hit = true;
                    break;
                }
            }
            if (invalid)
            {
                ViewBag.result = "invalid";

            }
            else
            {
                ViewBag.Result = hit ? "Hit" : "Miss";
            }
            // Set ViewBag properties for rendering in the view
            ViewBag.Boats = _game.Boats;
            ViewBag.GameId = _game.Id;
            ViewBag.SpelerNaam = _game.PlayerName;
            ViewBag.GestartMetSpelen = _game.StartedPlayingOn;
            ViewBag.Location = $"{upletter}{number}";


            ViewBag.SunkenBoatsCount = _game.Boats.Count(b => b.status == Status.Sunk);

            // Create a new GameLog instance to log the attempt
            GameLog log = new GameLog();
            log.LocationLetter = letter;
            log.LocationNumber = number;
            log.PlayerName = _game.PlayerName;
            log.CreatedOn = DateTime.Now;
            log.Result = ViewBag.Result;
            log.GameId = _game.Id;

            // Add the log to the context and save changes asynchronously
            _context.GameLogs.Add(log);
            await _context.SaveChangesAsync();

            return View();
        }

        // Method to load boats asynchronously from an external API
        public async Task LoadBoats()
        {
            using HttpClient httpClient = new HttpClient();
            /// Set the base address of the API
            httpClient.BaseAddress = new Uri("https://mgp32-api.azurewebsites.net/");




            for (int i = 0; i < 3; i++)
            {
                HttpResponseMessage response = await httpClient.GetAsync("randomlocation/get/6");

                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response to obtain boat locations
                    Location location = await response.Content.ReadFromJsonAsync<Location>();

                    // Add boats to the game object if location is not null
                    if (location != null)
                    {
                        await AddBoat(location);
                    }
                    else
                    {
                        // If the response did not contain a location, break the loop
                        break;
                    }
                }
            }
        }

        private async Task<bool> AddBoat(Location location)
        {
            // Check if any boat already exists at the given location
            bool isLocationUnique = _game.Boats.All(boat =>
                !(boat.location.Letter == location.Letter && boat.location.Number == location.Number));

            if (isLocationUnique)
            {
                // Add a new Boat object with the given location to the _game collection
                _game.Boats.Add(new Boat
                {
                    location = location,
                    status = Status.Active // Assuming all boats are initially active
                });

                // Return true indicating the boat was successfully added
                return true;
            }
            else
            {
                // Return false indicating the location is already occupied by another boat
                return false;
            }
        }

        private bool IsValidLetter(string letter)
        {
            return !string.IsNullOrEmpty(letter) && Regex.IsMatch(letter, @"^[A-Fa-f]$");
        }

        private bool IsValidNumber(string number)
        {
            int num;
            return int.TryParse(number, out num) && num >= 1 && num <= 6;
        }


    }
}