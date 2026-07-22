namespace TicketService.Domain.ValueObjects;

public record CodeDepartment
{
    private string Code { get; }

    private CodeDepartment(string code)
    {
        Code = code;
    }

    public static CodeDepartment Generate(string codeName)
    {
        //var guid = id.ToString("N");
        
        var code = $"IT-{codeName[..3]}";

        return new CodeDepartment(code);
    }
    
    public override string ToString() => Code;
}