using DbContext = Peyton.Core.Repository.DbContext;

namespace Peyton.Core
{
    public class DbInitializer 
    {
        public static void Seed(DbContext context)
        {
            SeedImportanceLevel(context);
            SeedErrorMessage(context);
            SeedLogFormat(context);
            SeedSetting(context);
            SeedResultCode(context);
            SeedRoleType(context);
            SeedUserRoleType(context);
            SeedCreditCardType(context);
        }

        private static void SeedImportanceLevel(DbContext context)
        {
            context.Set<ImportanceLevel>().Add(new ImportanceLevel { Name = "Critical"});
            context.Set<ImportanceLevel>().Add(new ImportanceLevel { Name = "Important" });
            context.Set<ImportanceLevel>().Add(new ImportanceLevel { Name = "Normal" });
            context.Set<ImportanceLevel>().Add(new ImportanceLevel { Name = "Optional" });
            context.SaveChanges();
        }

        private static void SeedErrorMessage(DbContext context)
        {
            context.Set<ErrorMessage>().Add(new ErrorMessage { Name = "ApplicationID", Description = "Application ID is required" });
            context.Set<ErrorMessage>().Add(new ErrorMessage { Name = "ConfirmPasswordInvalid", Description = "Password and confirm password are mismatched" });
            context.Set<ErrorMessage>().Add(new ErrorMessage { Name = "DisagreeTerms", Description = "Please agree the terms of use" });
            context.Set<ErrorMessage>().Add(new ErrorMessage { Name = "EmailInvalid", Description = "Email address is invalid format" });
            context.Set<ErrorMessage>().Add(new ErrorMessage { Name = "EmailRequired", Description = "Email is required" });
            context.Set<ErrorMessage>().Add(new ErrorMessage { Name = "FirstNameRequired", Description = "First Name is required" });
            context.Set<ErrorMessage>().Add(new ErrorMessage { Name = "InvalidApplication", Description = "Cannot find Application" });
            context.Set<ErrorMessage>().Add(new ErrorMessage { Name = "InvalidAuthToken", Description = "Authentication Token is invalid or expired" });
            context.Set<ErrorMessage>().Add(new ErrorMessage { Name = "InvalidUser", Description = "Cannot find User" });
            context.Set<ErrorMessage>().Add(new ErrorMessage { Name = "LastNameRequired", Description = "Last Name is required" });
            context.Set<ErrorMessage>().Add(new ErrorMessage { Name = "LoginFail", Description = "Invalid username or password" });
            context.Set<ErrorMessage>().Add(new ErrorMessage { Name = "NameRequired", Description = "Name is required" });
            context.Set<ErrorMessage>().Add(new ErrorMessage { Name = "PasswordRequired", Description = "Password is required" });
            context.Set<ErrorMessage>().Add(new ErrorMessage { Name = "PasswordMismatch", Description = "The new password and confirmation password do not match." });
            context.Set<ErrorMessage>().Add(new ErrorMessage { Name = "PasswordLenght", Description = "The password must be at least {0} characters long." });
            context.Set<ErrorMessage>().Add(new ErrorMessage { Name = "UsernameRequired", Description = "Username is required" });
            context.Set<ErrorMessage>().Add(new ErrorMessage { Name = "UsernameUnavalaible", Description = "Username is unavailable. Please choose reset password if you own this account" });
            context.SaveChanges();
        }

        private static void SeedLogFormat(DbContext context)
        {
            context.Set<LogFormat>().Add(new LogFormat { Name = "ChangeFormat", Description = "{0} changed from {1} to {2}" });
            context.Set<LogFormat>().Add(new LogFormat { Name = "RequestFormat", Description = "Request {0} From Application ID {1}, User ID {2}" });
            context.SaveChanges();
        }

        private static void SeedSetting(DbContext context)
        {
            context.Set<Setting>().Add(new Setting { Name = "Setting_EmailRegex", Description = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*@((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$" });
            context.Set<Setting>().Add(new Setting { Name = "Setting_MinDate", Description = "01/01/1900" });
            context.SaveChanges();
        }

        private static void SeedResultCode(DbContext context)
        {
            context.Set<ResultCode>().Add(new ResultCode { Name = "Success", Description = "" });
            context.Set<ResultCode>().Add(new ResultCode { Name = "MessageInvalid", Description = "" });
            context.Set<ResultCode>().Add(new ResultCode { Name = "Warning", Description = "" });
            context.Set<ResultCode>().Add(new ResultCode { Name = "AuthenticationError", Description = "" });
            context.Set<ResultCode>().Add(new ResultCode { Name = "TransactionError", Description = "" });
            context.Set<ResultCode>().Add(new ResultCode { Name = "ConnectionError", Description = "" });
            context.Set<ResultCode>().Add(new ResultCode { Name = "SystemError", Description = "" });
            context.Set<ResultCode>().Add(new ResultCode { Name = "UnknownError", Description = "" });
            context.SaveChanges();
        }

        private static void SeedRoleType(DbContext context)
        {
            context.Set<RoleType>().Add(new RoleType { Name = "System", Description = "" });
            context.Set<RoleType>().Add(new RoleType { Name = "Application", Description = "" });
            context.Set<RoleType>().Add(new RoleType { Name = "Custom", Description = "" });
            context.SaveChanges();
        }

        private static void SeedUserRoleType(DbContext context)
        {
            context.Set<UserRoleType>().Add(new UserRoleType { Name = "Role", Description = "" });
            context.Set<UserRoleType>().Add(new UserRoleType { Name = "Delegated", Description = "" });
            context.Set<UserRoleType>().Add(new UserRoleType { Name = "Cascade", Description = "" });
            context.Set<UserRoleType>().Add(new UserRoleType { Name = "Group", Description = "" });
            context.SaveChanges();
        }

        private static void SeedCreditCardType(DbContext context)
        {
            context.Set<CreditCardType>().Add(new CreditCardType { Name = "Visa" });
            context.Set<CreditCardType>().Add(new CreditCardType { Name = "Master" });
            context.Set<CreditCardType>().Add(new CreditCardType { Name = "American Express" });
            context.SaveChanges();
        }
    }
}