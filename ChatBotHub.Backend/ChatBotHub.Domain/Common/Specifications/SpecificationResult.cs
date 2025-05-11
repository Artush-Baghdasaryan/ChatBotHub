namespace ChatBotHub.Domain.Common.Specifications;

public readonly struct SpecificationResult {
    private SpecificationResult(bool value, string message = "") {
        Value = value;
        Message = message;
    }

    public bool Value { get; init; }
    public string Message { get; init; }

    public static SpecificationResult False(string message = "") {
        return new SpecificationResult(false, message);
    }

    public static implicit operator SpecificationResult(bool value) {
        return new SpecificationResult(value);
    }
}
