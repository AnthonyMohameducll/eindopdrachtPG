using Microsoft.AspNetCore.Components.Routing;
using PRG.EVA01.anthony.mohamed1.Models;

namespace PRG.EVA01.Anthony.Mohamed.Models
{
    public class Boat
    {   
        public int Id { get; set; }
        public Location location { get; set; }
        public  Status  status { get; set; }
        public int GameId { get; set; }  
        public Game Game { get; set; }  
    }
}
