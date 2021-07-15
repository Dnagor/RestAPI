using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestAPI.Model
{
    public class User
    {
        public string id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public List<Address> address { get; set; }
        public string phone { get; set; }
        public string website { get; set; }
        public List<Company> company { get; set; }

    }
}
