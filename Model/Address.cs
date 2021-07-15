using System.Collections.Generic;

namespace RestAPI.Model
{
    public class Address
    {
        public string street { get; set; }
        public string suite { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public List<Geo> geo { get; set; }

    }
}