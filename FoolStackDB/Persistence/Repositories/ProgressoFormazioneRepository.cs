﻿using FoolStackDB.Core.Domain;
using FoolStackDB.Core.Repositories;
using FoolStaff;
using FoolStaff.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoolStackDB.Persistence.Repositories
{
    public class ProgressoFormazioneRepository : Repository<ProgressoFormazione>, IProgressoFormazioneRepository
    {
        public ProgressoFormazioneRepository(FoolStaffContext context) : base(context)
        {
        }
        public FoolStaffContext FoolStaffContext
        {
            get { return Context as FoolStaffContext; }
        }
    }
}
