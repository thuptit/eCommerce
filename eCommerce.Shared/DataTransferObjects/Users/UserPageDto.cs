using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Shared.DataTransferObjects.Users
{
    public class UserPageDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string AvatarUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
