namespace PolicyAPI.Models
{
    public class Policymodels
    {
    }
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Cgpa { get; set; }
        public bool IsPlaced { get; set; }
        public decimal CurrentSalary { get; set; }
        public int CompaniesApplied { get; set; }
        public decimal DreamOffer { get; set; }
        public string DreamCompany { get; set; } = string.Empty;
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal SalaryOffered { get; set; }
        public string Category { get; set; } = string.Empty;
        public bool IsHighPaying { get; set; }
    }

    public class PolicyConfiguration
    {
        //public MaxCompaniesPolicy MaxCompanies { get; set; } = new()
        //{
        //    Enabled = true,
        //    MaxApplications = 3
        //};
        //public DreamOfferPolicy DreamOffer { get; set; } = new()
        //{
        //    Enabled = true
        //};
        //public DreamCompanyPolicy DreamCompany { get; set; } = new()
        //{
        //    Enabled = true
        //};
        //public CgpaThresholdPolicy CgpaThreshold { get; set; } = new()
        //{
        //    Enabled = true,
        //    MinimumCgpa = 7.0,
        //    HighSalaryThreshold = 2500000
        //};
        //public PlacementPercentagePolicy PlacementPercentage { get; set; } = new()
        //{
        //    Enabled = false,
        //    TargetPercentage = 80.0
        //};
        //public OfferCategoryPolicy OfferCategory { get; set; } = new()
        //{
        //    Enabled = true,
        //    L1Threshold = 3500000,
        //    L2Threshold = 2000000,
        //    RequiredHikePercentageForL2 = 20.0
        //};
        public MaxCompaniesPolicy MaxCompanies { get; set; } = new();
        public DreamOfferPolicy DreamOffer { get; set; } = new();
        public DreamCompanyPolicy DreamCompany { get; set; } = new();
        public CgpaThresholdPolicy CgpaThreshold { get; set; } = new();
        public PlacementPercentagePolicy PlacementPercentage { get; set; } = new();
        public OfferCategoryPolicy OfferCategory { get; set; } = new();
    }

    public class MaxCompaniesPolicy
    {
        public bool Enabled { get; set; }
        public int MaxApplications { get; set; }
    }

    public class DreamOfferPolicy
    {
        public bool Enabled { get; set; }
    }

    public class DreamCompanyPolicy
    {
        public bool Enabled { get; set; }
    }

    public class CgpaThresholdPolicy
    {
        public bool Enabled { get; set; }
        public double MinimumCgpa { get; set; }
        public decimal HighSalaryThreshold { get; set; }
    }

    public class PlacementPercentagePolicy
    {
        public bool Enabled { get; set; }
        public double TargetPercentage { get; set; }
    }

    public class OfferCategoryPolicy
    {
        public bool Enabled { get; set; }
        public decimal L1Threshold { get; set; }
        public decimal L2Threshold { get; set; }
        public double RequiredHikePercentageForL2 { get; set; }
    }

    public class EligibilityResult
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public bool IsEligible { get; set; }
        public List<string> Reasons { get; set; } = new();
        public string Decision { get; set; } = string.Empty;
    }

    public class EligibilityRequest
    {
        public List<Student> Students { get; set; } = new();
        public List<Company> Companies { get; set; } = new();
        public PolicyConfiguration Policies { get; set; } = new();
        public double CurrentPlacementPercentage { get; set; }
    }

    public class PlacementStats
    {
        public int TotalStudents { get; set; }
        public int PlacedStudents { get; set; }
        public double PlacementPercentage { get; set; }
    }

    public class SingleEligibilityRequest
    {
        public int StudentId { get; set; }
        public int CompanyId { get; set; }
        public List<Student> Students { get; set; } = new();
        public List<Company> Companies { get; set; } = new();
        public PolicyConfiguration Policies { get; set; } = new();
        public double CurrentPlacementPercentage { get; set; }
    }


}
