namespace SchoolProject.Infrustructure.CommonLocalizer
{
    public class LocalizeEntities
    {
        public static string GetLicalizedName(string TextAr, string TextEn)
        {

            var Culture = Thread.CurrentThread.CurrentCulture.Name;

            if (Culture.ToLower().Contains("ar"))
                return TextAr;
            else
                return TextEn;
        }


    }
}