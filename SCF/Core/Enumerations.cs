using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Repository;

namespace Peyton.Core
{

    public enum AccountType : int
    {
        Cheuque,
        Saving,
        Credit
    }

    public enum CardType : int
    {
        Visa = 0,
        MasterCard,
        Discover,
        AmericanExpress,
        DinersClub,
        JCB
    }

    public enum Status : int
    {
        New = 0,
        Inactive = 1,
        Active = 2,
        Normal = 3,
        Expired = 4,
        //Job Status {
        Overdue = 5,
        Waiting = 6,
        Cancelled = 7,
        Completed = 8,
        //} 
        Deleted = 9,
        Read  = 10
    }

    public enum Priority : int
    {
        Urgent = 1,
        High,
        Medium,
        Low
    }

    public enum ServiceResult : int
    {
        Success,
        Error,
        Invalid
    }

    public enum ServiceLogType : int
    {
        CloudService,
        Service,
        Adaptor,
        Router,
        Workflow

    }

    public enum MilestoneType
    {
        HourlyServiceReport,
        DailyServiceReport,
        MonthlyServiceReport,
        AnnuallyServiceReport
    }
}
