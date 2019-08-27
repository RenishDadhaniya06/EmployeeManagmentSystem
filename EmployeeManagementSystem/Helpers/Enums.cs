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

        public enum OpeningStatus
        {
            Active = 0,
            Expired = 1,
            OnHold = 2
        }

        public enum InterviewStatus
        {
            Rejected = 0,
            ResumeonHold = 1,
            Offered = 2,
            ProcessForNextRound = 3,
            Pending = 4
        }
    }
}
