using System.Collections.Generic;
using MotorcycleContest.model;
using MotorcycleContest.model.validators;
using MotorcycleContest.repository.interfaces;

namespace MotorcycleContest.repository
{
    public class TeamDbRepository : AbstractRepository<long, Team>, ITeamRepository
    {
        public TeamDbRepository(IValidator<Team> validator) : base(validator)
        {
        }

        public List<Team> FilterByName(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}