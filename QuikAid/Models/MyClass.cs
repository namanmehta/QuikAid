using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace QuikAid.Models
{
    public class Custom
    {
        [DisplayName("ID number")]
        public string FirstName { get; set; }
        [DisplayName("Count of Ids for this client")]
        public int CountOfClients { get; set; }
    }
}