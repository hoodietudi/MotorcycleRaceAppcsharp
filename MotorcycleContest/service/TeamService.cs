using System.Collections.Generic;
using Domain.model;
using Persistance;

namespace MotorcycleContest.service
{
    public class TeamService
    {
        private readonly TeamDbRepository _repository;

        public TeamService(TeamDbRepository repository)
        {
            _repository = repository;
        }

        public List<Team> GetAllTeams()
        {
            return _repository.FindAll();
        }

        public Team FilterByName(string name)
        {
            return _repository.FilterByName(name);
        }
    }
}