namespace Cal2CapBlazor.Application.Common.Security;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RequireRoleAttribute(string role) : Attribute
{
    public string Role { get; } = role;
}

[AttributeUsage(AttributeTargets.Class)]
public class GuestOnlyAttribute : Attribute;