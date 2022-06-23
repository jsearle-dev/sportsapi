using System.Text.Json.Serialization;
using SportsApi.Models;

namespace SportsApi.Models {

    public class Team : DbItem {
        
        public string? TeamName {get; set;}

        public string? Location {get; set;}

        [JsonIgnore]
        public IEnumerable<Player>? Players {get; set;}
    }
}