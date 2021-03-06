﻿using Alumini.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AluminiRepository.Interfaces
{
    public interface IFacultyWorkInfoRepository : GenericCRUDRepository<FacultyWorkInfo>
    {
        event OnUserRegistrationCompleted InsertCompletedEvent;
        FacultyWorkInfo UpdateWorkInfo(int id, FacultyWorkInfo WorkInfo);
    }
}
