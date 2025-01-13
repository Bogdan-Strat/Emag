using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public class CurrentUserDTO
    {
        public Guid Id { get; set; }
        public int RoleId { get; set; }
    }
}
