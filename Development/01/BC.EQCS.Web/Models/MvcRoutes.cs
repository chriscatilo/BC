namespace BC.EQCS.Web.Models
{
    public class MvcRoutes
    {
        public static class UkviImmediateReportByIncidentId
        {
            public const string Name = "UkviImmediateReportByIncidentId";
            public const string Route = "incident/{id:int}/report/ukviimmediate";
        }

        public static class AccountLoginView
        {
            public const string Name = "AccountLoginView";
            public const string Route = "account/login";
        }

        public static class AccountLoginAutoView
        {
            public const string Name = "AccountLoginAutoView";
            public const string Route = "account/loginauto";
        }

        public static class AccountLogin
        {
            public const string Name = "AccountLogin";
            public const string Route = "account/login";
        }

        public static class AccountLogOff
        {
            public const string Name = "AccountLogOff";
            public const string Route = "account/logoff";
        }

        public static class AccountForgorPasswordView
        {
            public const string Name = "AccountForgorPasswordView";
            public const string Route = "account/forgotpassword";
        }

        public static class AccountForgorPassword
        {
            public const string Name = "AccountForgorPassword";
            public const string Route = "account/forgotpassword";
        }

        public static class AccountForgorPasswordConfirmationView
        {
            public const string Name = "AccountForgorPasswordConfirmationView";
            public const string Route = "account/forgotpasswordconfirmation";
        }


        public static class AccountResetPasswordView
        {
            public const string Name = "AccountResetPasswordView";
            public const string Route = "account/resetpassword";
        }

        public static class AccountResetPassword
        {
            public const string Name = "AccountResetPassword";
            public const string Route = "account/resetpassword";
        }

        public static class AccountResetPasswordConfirmationView
        {
            public const string Name = "AccountResetPasswordConfirmationView";
            public const string Route = "account/resetpasswordconfirmation";
        }
    }
}