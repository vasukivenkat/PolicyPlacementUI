﻿namespace PolicyAPI.DTOs
{
    public class OfferCategoryPolicyDTO
    {
        public bool Enabled { get; set; }
        public decimal L1Threshold { get; set; }
        public decimal L2Threshold { get; set; }
        public double RequiredHikePercentageForL2 { get; set; }
    }
}
