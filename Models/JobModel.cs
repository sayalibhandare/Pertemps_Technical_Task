using System;

namespace TechnicalTest.Models
{
    public class Job
    {
        public string? link { get; set; }
        public string? job_title { get; set; }
        public string? job_description { get; set; }
        public string? location { get; set; }
        public int salary_from { get; set; }
        public int salary_to { get; set; }
        public DateTime posted_date { get; set; }
        public string? consultant_name { get; set; }
        public string? consultant_email { get; set; }
        public string? category { get; set; }
        public string? id { get; set; }
        public string? postAge { get; set; }
    }

    public class Root
    {
        public List<Job>? jobs { get; set; }
    }
}
