using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDoApp.Persistence.Data
{
    public class User
    {
        [Key]
        [JsonProperty("id")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate{ get; set; }
    }
}
