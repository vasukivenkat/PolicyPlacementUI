namespace PolicyAPI.Models
{
    public class PolicyEvaluationResult
    {
        public bool Passed { get; set; }
        public string Reason { get; set; } = string.Empty;
        public bool Blocking { get; set; }

        public static PolicyEvaluationResult Success(string reason = "", bool blocking = false) =>
            new PolicyEvaluationResult { Passed = true, Reason = reason, Blocking = blocking };

        public static PolicyEvaluationResult Failure(string reason, bool blocking = true) =>
            new PolicyEvaluationResult { Passed = false, Reason = reason, Blocking = blocking };
    }
}
