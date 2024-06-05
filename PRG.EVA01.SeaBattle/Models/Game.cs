using PRG.EVA01.Anthony.Mohamed.Models;

namespace PRG.EVA01.anthony.mohamed1.Models
{
    public class Game
    {
        public int Id {  get; set; }
        public  List<Boat> Boats { get; set;}
        public  string PlayerName { get; set; }
        public DateTime StartedPlayingOn { get; set; }
        public List<GameLog> GameLogs { get; set; }
    }
}
