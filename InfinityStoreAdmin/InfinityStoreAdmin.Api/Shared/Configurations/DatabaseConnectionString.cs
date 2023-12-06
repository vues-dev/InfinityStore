namespace InfinityStoreAdmin.Api.Shared.Configurations;

public class DatabaseConnectionString
{
    public string Host { get; set; }

    public string Port { get; set; }

    public string Database { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public override string ToString()
    {
        return $"Server={this.Host}; Port={this.Port}; Database={this.Database}; User Id={this.UserName}; Password={this.Password};";
    }
}