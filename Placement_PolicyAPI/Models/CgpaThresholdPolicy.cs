namespace PolicyAPI.Models
{
    public class CgpaThresholdPolicy
    {
        public bool Enabled { get; set; }
        public double MinimumCgpa { get; set; }
        public decimal HighSalaryThreshold { get; set; }
    }
}
