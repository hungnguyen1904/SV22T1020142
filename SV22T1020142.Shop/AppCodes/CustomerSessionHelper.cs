namespace SV22T1020142.Shop.AppCodes
{
    public static class CustomerSessionHelper
    {
        private const string SessionKey = "SHOP_CUSTOMER";

        public static CustomerSessionData? GetLoggedInCustomer(HttpContext context)
        {
            return context.Session.GetSessionData<CustomerSessionData>(SessionKey);
        }

        public static bool IsLoggedIn(HttpContext context)
        {
            return GetLoggedInCustomer(context) != null;
        }

        public static void SignIn(HttpContext context, CustomerSessionData customer)
        {
            context.Session.SetSessionData(SessionKey, customer);
        }

        public static void SignOut(HttpContext context)
        {
            context.Session.Remove(SessionKey);
        }
    }
}
