namespace ClientManagement.Application.Features.Client.Queries.GetAllClients;

public class ClientDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
}
