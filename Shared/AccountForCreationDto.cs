using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class AccountForCreationDto
    {
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string AccountType { get; set; }
    }
}
