using System.ComponentModel.DataAnnotations.Schema;
using Peyton.Core.Repository;

namespace Peyton.Core
{

    [Table("ImportanceLevel", Schema = "Enumeration")]
    public class ImportanceLevel: EnumEntity
    {
    }

    [Table("GroupLevel", Schema = "Enumeration")]
    public class GroupLevel : EnumEntity
    {
    }

    [Table("GroupType", Schema = "Enumeration")]
    public class GroupType : EnumEntity
    {
    }

    [Table("ErrorMessage", Schema = "Enumeration")]
    public class ErrorMessage : EnumEntity
    {
    }

    [Table("LogFormat", Schema = "Enumeration")]
    public class LogFormat : EnumEntity
    {
    }

    [Table("Setting", Schema = "Enumeration")]
    public class Setting : EnumEntity
    {
    }

    [Table("ResultCode", Schema = "Enumeration")]
    public class ResultCode : EnumEntity
    {
        public static implicit operator ResultCode(string val)
        {
            var result = new ResultCode();
            result.Name = val;
            return result;
        }
    }

    [Table("RoleType", Schema = "Enumeration")]
    public class RoleType : EnumEntity
    {
    }

    [Table("UserRoleType", Schema = "Enumeration")]
    public class UserRoleType : EnumEntity
    {
    }

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

    [Table("CreditCardType", Schema = "Enumeration")]
    public class CreditCardType : EnumEntity
    {
    }

    public enum Status : int
    {
        New,
        Active,
        Normal,
        Pending,
        Deactive,
        Deleted,
        Stopped,
        Maintain,
        UnderDevelop
    }

    public enum Priority : int
    {
        Urgent = 1,
        High,
        Medium,
        Low
    }

    public enum Severity : int
    {
        Critical = 1,
        Importance,
        Normal,
        Optional
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
