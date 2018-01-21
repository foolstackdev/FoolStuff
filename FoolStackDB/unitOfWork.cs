using FoolStackDB.Core.Repositories;
using FoolStackDB.Persistence.Repositories;
using FoolStaff.Core;
using FoolStaff.Core.Repositories;
using FoolStaff.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStaff
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FoolStaffContext _context;

        public IUserRepository Users { get; private set; }
        public ITesoreriaRepository Tesoreria { get; private set; }
        public IEffortRepository Efforts { get; private set; }
        public IEventoRepository Eventi { get; private set; }
        public IMessaggioRepository Messaggi { get; private set; }
        public IRispostaRepository Risposte { get; private set; }
        public ICorsoRepository Corsi { get; private set; }
        public ICapitoloRepository Capitoli { get; private set; }
        public ITagRepository Tags { get; private set; }
        public IProgressoFormazioneRepository ProgressiFormazione { get; private set; }

        public UnitOfWork(FoolStaffContext context)
        {
            _context = context;

            Users = new UserRepository(_context);
            Tesoreria = new TesoreriaRepository(_context);
            Efforts = new EffortRepository(_context);
            Eventi = new EventoRepository(_context);
            Messaggi = new MessaggioRepository(_context);
            Risposte = new RispostaRepository(_context);
            Corsi = new CorsoRepository(_context);
            Capitoli= new CapitoloRepository(_context);
            Tags = new TagRepository(_context);
            ProgressiFormazione = new ProgressoFormazioneRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
