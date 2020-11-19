using System.Data;

namespace Retail.Persistence.Database
{
    public interface IDbConnectionFactory
    {
        IDbConnection Create();
    }
}
