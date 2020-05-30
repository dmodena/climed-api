using CliMed.Api.Models;
using System.Collections.Generic;

namespace CliMed.Api.Repositories
{
    public interface IRoleRepository
    {
        IList<Role> GetlAllItems();
        Role GetByValue(string value);
    }
}
