namespace Webapi
{
    public class Site
    {
        public int ID { get;  set; }
        public string? Name { get;  set; }
        public string? ServerPath { get; set; }
        public DateTime CreateDate { get; set; }

        public DateTime LastCheckForLicens { get; set; }
    }
}