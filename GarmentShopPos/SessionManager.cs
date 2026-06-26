namespace GarmentShopPos
{
    public static class SessionManager
    {
        public static Models.User? CurrentUser { get; set; }

        public static bool IsLoggedIn => CurrentUser != null;

        public static string ActiveShopMode { get; set; } = "Gents"; // "Gents" or "Ladies"

        public static string ShopName { get; set; } = "GARMENTS STORE";
        public static string ShopAddress { get; set; } = "123 Main Cloth Market, Gents & Ladies";
        public static string ShopPhone { get; set; } = "+92 300 1234567";
        public static string ShopTimeZone { get; set; } = "Pakistan Standard Time";
        public static string ShopLanguage { get; set; } = "English"; // "English" or "Urdu"

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
