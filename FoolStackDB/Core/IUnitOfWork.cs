using FoolStaff.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStaff.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        ITesoreriaRepository Tesoreria { get; }
        IEffortRepository Efforts { get; }
        int Complete();
    }
}
