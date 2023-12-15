using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy_1_RestfulBooker.Models
{
    internal class BookingDates
    {
        [JsonProperty("checkin")]
        public string? CheckinDate { get; set; }

        [JsonProperty("checkout")]
        public string? CheckoutDate { get; set; }

    }
}
