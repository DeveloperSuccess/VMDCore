using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMDCore.Data.Models
{
    public class Operation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key()]
        public int OperationId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Баланс не может быть отрицательным")]
        public int Balance { get; set; }

        public string Message { get; set; }

    }
}
