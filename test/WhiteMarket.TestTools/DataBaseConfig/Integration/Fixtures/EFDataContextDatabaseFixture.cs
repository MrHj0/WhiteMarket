using WhiteMarket.Presistence.EF;
using Xunit;

namespace WhiteMarket.TestTools.DataBaseConfig.Integration.Fixtures;

[Collection(nameof(ConfigurationFixture))]
public class EFDataContextDatabaseFixture : DatabaseFixture
{
    protected static EFDataContext CreateDataContext(string tenantId)
    {
        var connectionString =
            new ConfigurationFixture().Value.ConnectionString;


        return new EFDataContext(connectionString);
    }
}