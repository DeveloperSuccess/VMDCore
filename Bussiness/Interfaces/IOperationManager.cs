using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMDCore.Data.Models;

namespace VMDCore.Bussiness.Interfaces
{
    public interface IOperationManager
    {
        Operation FindOperationById(int id);

        void SaveOperation(Operation operation);
    }
}
