using ClientManagement.Domain.Common;

namespace ClientManagement.Domain;

public class Client : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
}