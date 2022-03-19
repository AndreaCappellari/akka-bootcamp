namespace WinTail;

public class Messages
{
    /// <summary>
    ///     Marker class to continue processing.
    /// </summary>
    public class ContinueProcessing { }

    /// <summary>
    ///     Base class for signalling that user input was valid.
    /// </summary>
    public class InputSuccess
    {
        public string Reason { get; private set; }

        public InputSuccess(string reason)
        {
            Reason = reason;
        }
    }
    
    /// <summary>
    ///     Base class for signalling that user input was invalid.
    /// </summary>
    public class InputError
    {
        public string Reason { get; private set; }

        public InputError(string reason)
        {
            Reason = reason;
        }
    }
    
    /// <summary>
    ///     User provided blank input.
    /// </summary>
    public class NullInputError : InputError
    {
        public NullInputError(string reason) : base(reason)
        {
        }
    }
    
    /// <summary>
    ///     User provided invalid input (currently, input w/ odd # chars).
    /// </summary>
    public class ValidationError : InputError
    {
        public ValidationError(string reason) : base(reason)
        {
        }
    }
}