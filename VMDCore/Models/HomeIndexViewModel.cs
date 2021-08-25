using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VMDCore.Data.Models;

namespace VMDCore.Models
{
    public class HomeIndexViewModel
    {
        public List<Drink> Drinks { get; set; }
        public List<Coin> Coins { get; set; }
        public Operation Operation { get; set; }

    }
}
