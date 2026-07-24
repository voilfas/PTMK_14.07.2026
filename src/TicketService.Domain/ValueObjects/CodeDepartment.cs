namespace TicketService.Domain.ValueObjects;

public record CodeDepartment
{
    public string Value { get; }

    private CodeDepartment(string value)
    {
        Value = value;
    }

    public static CodeDepartment GenerateNext(CodeDepartment? code)
    {
        if (code is null)
            return new CodeDepartment("IT-0001");
        
        var lastCode = code.Value;
        
        int firstDigitIndex = lastCode.IndexOfAny(['0', '1', '2', '3', '4', '5', '6', '7', '8', '9']);
        
        string prefix = lastCode[..firstDigitIndex];
        string digitsPart = lastCode[firstDigitIndex..];
        
        int nextNumber = int.Parse(digitsPart) + 1;
        
        string newDigitsPart = nextNumber.ToString().PadLeft(digitsPart.Length, '0');
        
        var newCode = $"{prefix}{newDigitsPart}";
        
        return new CodeDepartment(newCode);
    }
    
    public static CodeDepartment FromDatabase(string code)
    {
        return new CodeDepartment(code);
    }
    
    public override string ToString() => Value;
}