using System.Text.Json.Serialization;
using SportsApi.Models;

namespace SportsApi.Models {

    public class Player : DbItem {

        public string? FirstName {get; set;}

        public string? LastName {get; set;}

        public long? TeamId {get; set;}

        [JsonIgnore]
        public Team? Team {get; set;}
    }
}