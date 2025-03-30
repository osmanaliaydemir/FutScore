using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutScore.Domain
{
    public class ProcessResult
    {
        public ProcessResult()
        {
            ValidationResults = new List<ValidationResult>();
            Output = new ExpandoObject();
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object EntityId { get; set; }
        public List<ValidationResult> ValidationResults { get; set; }
        public dynamic Output { get; set; }
    }
}
