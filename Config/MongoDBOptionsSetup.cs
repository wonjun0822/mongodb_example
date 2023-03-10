using Microsoft.Extensions.Options;

namespace mongodb_example.Config;

public class MongoDBoptionsSetup : IConfigureOptions<MongoDBOptions>
{
    private const string SectionName = "MongoDB";
    private readonly IConfiguration _configuration;

    public MongoDBoptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(MongoDBOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}