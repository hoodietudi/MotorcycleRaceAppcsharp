using MotorcycleContest.service;
using System.Windows.Forms;
using Persistance;


namespace MotorcycleContest
{
    class Program
    {
        public static void Main(string[] args)
        {
            // user
            var userRepo = new UserDbRepository();
            var userService = new UserService(userRepo);
            
            // race
            var raceRepo = new RaceDbRepository();
            var raceService = new RaceService(raceRepo);
            
            // entry 
            var entryRepo = new EntryDbRepository();
            var entryService = new EntryService(entryRepo);
            
            // team
            var teamRepo = new TeamDbRepository();
            var teamService = new TeamService(teamRepo);
            
            // participant
            var participantRepo = new ParticipantDbRepository();
            var participantService = new ParticipantService(participantRepo);
            
            // motorcycle
            var motorcycleRepo = new MotorcycleDbRepository();
            var motorcycleService = new MotorcycleService(motorcycleRepo);
            
            var allService = new AllService(userService, raceService, 
                entryService, teamService, participantService, motorcycleService);
            
            // var loginForm = new LoginForm();
            // var mainForm = new MainForm();
            //
            // loginForm.SetService(allService, mainForm);
            // mainForm.Set(allService, loginForm);
            
            // Application.Run(loginForm);
        }
    }
}