﻿using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository.Interfaces
{
    public interface ISaluationRepository : GenericCRUDRepository<Salutation>
    {
        IEnumerable<Salutation> GetSaluations();
    }
}
