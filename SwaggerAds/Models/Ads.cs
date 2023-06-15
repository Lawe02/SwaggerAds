using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerAds.Models
{
    public class Ads
    {
        public int Id {get; set;}
        public string? Description {get; set;}
        public string? Title {get; set;}
        public decimal Price {get; set;}
        public string? ImageUrl {get; set;}
        public DateTime StartDate {get; set;}
        public DateTime? SellDAte {get; set;}
    }
}