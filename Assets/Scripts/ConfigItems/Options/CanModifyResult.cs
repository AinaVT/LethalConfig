namespace LethalConfig.ConfigItems.Options
{
    /// <summary>
    /// Struct that wraps a bool <see cref="_result"/> and a string <see cref="Reason"/> that is used to determine if an entry can be modified or not.
    /// <remarks>Can implicitly cast from a bool and a tuple (bool, string)</remarks>
    /// Valid uses:
    /// <code>
    ///     return false;
    /// </code>
    /// <code>
    ///     return (false, "Example reason message");
    /// </code>
    /// <code>
    ///     return CanModifyResult.False("Example reason message");
    /// </code>
    /// </summary>
    public struct CanModifyResult
    {
        private readonly bool _result;
        public string Reason { get; }
        
        /// <param name="result">bool that determines if the entry can be modified or not.</param>
        /// <param name="reason">text that is displayed to the user if the entry cannot be modified.</param>
        private CanModifyResult(bool result, string reason)
        {
            _result = result;
            Reason = reason;
        }

        public static CanModifyResult True() => new(true, "");
        public static CanModifyResult False(string reason) => new(false, reason);

        public static implicit operator CanModifyResult(bool result) => new(result, "No reason provided.");
        public static implicit operator CanModifyResult((bool result, string reason) tuple) => new(tuple.result, tuple.reason);
        public static implicit operator bool(CanModifyResult canModify) => canModify._result;
    }
}