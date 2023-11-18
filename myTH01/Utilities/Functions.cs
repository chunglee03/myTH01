namespace myTH01.Utilities
{
    public class Functions
    {
        public static string TitleSlugGeneration(string type, string title, long id)
        {
            string sTitle = type + "-" + SlugGenerator.SlugGenerator.GenerateSlug(title) + "-" + id.ToString() + ".html";
            return sTitle;
        }
        public static string getCurrentDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
