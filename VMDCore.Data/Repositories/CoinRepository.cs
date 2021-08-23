using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMDCore.Data.Interfaces;
using VMDCore.Data.Models;

namespace VMDCore.Data.Repositories
{
    public class CoinRepository : BaseRepository<Coin>, ICoinRepository
    {
        public CoinRepository(VmdDbContext context) : base(context)
        {           
        }

        public int GetBalance()
        {
            return dbSet.Select(c => c.SumCoins).Sum();
        }
    }
}
