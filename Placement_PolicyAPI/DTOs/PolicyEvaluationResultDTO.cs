namespace PolicyAPI.DTOs
{
    public class PolicyEvaluationResultDTO
    {
        public bool Passed { get; set; }
        public string Reason { get; set; } = string.Empty;
        public bool Blocking { get; set; }

        public static PolicyEvaluationResultDTO Success(string reason = "", bool blocking = false) =>
            new PolicyEvaluationResultDTO { Passed = true, Reason = reason, Blocking = blocking };

        public static PolicyEvaluationResultDTO Failure(string reason, bool blocking = true) =>
            new PolicyEvaluationResultDTO { Passed = false, Reason = reason, Blocking = blocking };
    }
}
