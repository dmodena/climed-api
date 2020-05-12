using System.Collections.Generic;

namespace CliMed.Api.Repositories
{
    public interface IReadableRepository<T>
    {
        IList<T> GetAllItems();
        T GetById(long id);
    }
}
