namespace TicketService.Domain.ValueObjects;

public record CodeDepartment
{
    public string Code { get; }

    private CodeDepartment(string code)
    {
        Code = code;
    }

    public static CodeDepartment Generate(string codeName)
    {
        var code = $"IT-{codeName[..3]}";

        return new CodeDepartment(code);
    }
    
    public static CodeDepartment FromDatabase(string code)
    {
        return new CodeDepartment(code);
    }
    
    public override string ToString() => Code;
}