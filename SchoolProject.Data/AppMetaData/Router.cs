namespace SchoolProject.Data.AppMetaData
{
    public static class RouterLinks
    {
        public const string Id = "{id}";
        public const string Root = "Api";
        public const string Version = "V1";
        public const string List = "List";

        public const string Rule = $"{Root}/{Version}";

        public static class StudentRouting
        {
            public const string Students = "Students";
            public const string Collection = $"{Rule}/{Students}";

            public const string GetById = $"{Collection}/{Id}";
        }
    }
}