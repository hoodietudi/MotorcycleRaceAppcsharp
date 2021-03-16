﻿using System.Collections.Generic;
using MotorcycleContest.model;

namespace MotorcycleContest.repository.interfaces
{
    public interface ITeamRepository : IRepository<int, Team>
    {
        List<Team> FilterByName(string name);
    }
}