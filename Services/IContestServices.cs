using Domain.model;

namespace Services
{
    public interface IContestServices
    {
        void Login(User user, IContestObserver client);
        void Logout(User user, IContestObserver client);
        RaceDTO[] GetRaces();
        string[] GetTeamsNames();
        ParticipantDTO[] GetTeamMembers(string team);

        Race[] GetRacesByEngineCapacity(EngineCapacity engineCapacity);

        void AddParticipantEntry(string name, string team, string engineCapacity, string raceName);
    }
}