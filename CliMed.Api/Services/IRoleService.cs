using CliMed.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CliMed.Api.Services
{
    public interface IRoleService
    {
        IList<Role> GetAllItems();
    }
}
