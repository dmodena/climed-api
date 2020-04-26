using CliMed.Api.Models;
using System.Collections.Generic;

namespace CliMed.Api.Services
{
    public interface IRoleService
    {
        IList<Role> GetAllItems();
    }
}
