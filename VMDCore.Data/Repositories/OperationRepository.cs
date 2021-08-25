using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMDCore.Data.Interfaces;
using VMDCore.Data.Models;

namespace VMDCore.Data.Repositories
{
    public class OperationRepository : BaseRepository<Operation>, IOperationRepository
    {
        public OperationRepository(VmdDbContext context) : base(context)
        {
        }


    }
}
