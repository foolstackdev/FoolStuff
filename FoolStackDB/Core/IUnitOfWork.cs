using FoolStackDB.Core.Repositories;
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
        IEventoRepository Eventi { get; }
        IMessaggioRepository Messaggi { get; }
        IRispostaRepository Risposte { get; }
        ICapitoloRepository Capitoli { get; }
        ICorsoRepository Corsi { get; }
        ITagRepository Tags { get; }
        IProgressoFormazioneRepository ProgressiFormazione { get; }


        int Complete();
    }
}
