namespace PolicyAPI.DTOs
{
    public class CgpaThresholdPolicyDTO
    {
        public bool Enabled { get; set; }
        public double MinimumCgpa { get; set; }
        public decimal HighSalaryThreshold { get; set; }
    }
}
