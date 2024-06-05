using System;

namespace PRG.EVA01.anthony.mohamed1.Models
{
    public class GameLog
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string PlayerName { get; set; }
        public string LocationLetter { get; set; }
        public string LocationNumber { get; set; }
        public string Result { get; set; }
        public DateTime CreatedOn { get; set; }

        public Game Game { get; set; }

        // Constructor to initialize the CreatedOn property with DateTime.Now

    }
}
