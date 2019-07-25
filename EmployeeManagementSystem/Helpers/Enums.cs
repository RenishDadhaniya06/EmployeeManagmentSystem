using System;
using System.Collections.Generic;
using System.Text;
namespace Helpers
{
    public static class Enums
    {
        public enum LeaveType
        {
            Paid = 0,
            SeekLeave = 1,
            EmergancyLeave = 2
        }


        public enum LeaveStatus
        {
            Approved = 0,
            Rejected = 1,
            Pending = 2
        }
    }
}
