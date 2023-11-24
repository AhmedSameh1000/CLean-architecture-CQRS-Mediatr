namespace SchoolProject.Data.AppMetaData
{
    public static class RouterLinks
    {
        public const string Id = "{Id}";
        public const string Root = "Api";
        public const string Version = "V1";
        public const string List = "List";
        public const string Add = "Create";
        public const string Rule = $"{Root}/{Version}";

        public static class StudentRouting
        {
            public const string Students = "Students";
            public const string Collection = $"{Rule}/{Students}";
            public const string GetById = $"{Collection}/{Id}";
            public const string Create = $"{Collection}/Create";
            public const string Update = $"{Collection}/Update";
            public const string Delete = $"{Collection}/Delete/{Id}";
        }

        public static class DepartmentRouting
        {
            public const string Departments = $"{Rule}/Departments/{Id}";
        }
    }
}