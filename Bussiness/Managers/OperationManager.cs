using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMDCore.Bussiness.Interfaces;
using VMDCore.Data.Interfaces;
using VMDCore.Data.Models;

namespace VMDCore.Bussiness.Managers
{
  
    public class OperationManager : IOperationManager
    {
        private IOperationRepository operationRepository;


        public OperationManager(IOperationRepository operationRepository)
        {
            this.operationRepository = operationRepository;
        }

        public Operation FindOperationById(int id)
        {
            return operationRepository.FindById(id);
        }

        public void SaveOperation(Operation operation)
        {
            if (operation.OperationId != 0)
            {
                operationRepository.Update(operation);
            }           
        }       
    }
}
