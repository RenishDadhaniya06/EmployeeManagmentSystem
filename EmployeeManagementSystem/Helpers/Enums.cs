
namespace Helpers
{
    #region Using
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    #endregion


    /// <summary>
    /// Enums
    /// </summary>
    public static class Enums
    {
        /// <summary>
        /// LeaveType
        /// </summary>
        public enum LeaveType
        {
            Paid = 0,
            SeekLeave = 1,
            EmergancyLeave = 2
        }


        /// <summary>
        /// LeaveStatus
        /// </summary>
        public enum LeaveStatus
        {
            Approved = 0,
            Rejected = 1,
            Pending = 2
        }

        /// <summary>
        /// OpeningStatus
        /// </summary>
        public enum OpeningStatus
        {
            Active = 0,
            Expired = 1,
            OnHold = 2
        }

        /// <summary>
        /// InterviewStatus
        /// </summary>
        public enum InterviewStatus
        {
            Rejected = 0,
            ResumeonHold = 1,
            Offered = 2,
            ProcessForNextRound = 3,
            Pending = 4
        }

        public enum ProjectStatus
        {
            Pending = 0,
            Active = 1,
            Closed = 2
        }

        public enum ProjectType
        {
            //[Description("Fixed Price Project (FPP)")]
            [Display(Name = "Fixed Price Project (FPP)")]
            FixedPriceProject = 0,
            //[Description("Hire Based Project")]
            [Display(Name = "Hire Based Project")]
            HireBasedProject = 1
        }

        public enum IsCurrentlyWorking
        {
            Working = 1,
            [Description("Not Working")]
            NotWorking = 0
        }

        public enum BloodGroups
        {
            [Display(Name ="A+")]
            APositive = 0,

            [Display(Name ="A-")]
            ANegetive = 1,

            [Display(Name ="B+")]
            BPositive = 2,

            [Display(Name ="B-")]
            BNegetive = 3,

            [Display(Name ="O+")]
            OPositive = 4,

            [Display(Name ="O-")]
            ONegetive = 5,

            [Display(Name ="AB+")]
            ABPositive = 6,

            [Display(Name ="AB-")]
            ABNegetive = 7
        }
        
    }
}
