using System;
using System.Net.Sockets;
using System.Threading;
using Domain.model.validators;
using Networking;
using Persistance;
using Persistance.interfaces;
using Services;

namespace Server
{
    using Server;
    public class StartServer
    {
        public static void Main(string[] args)
        {
            var userRepo = new UserDbRepository();

            var raceRepo = new RaceDbRepository();

            var entryRepo = new EntryDbRepository();

            var teamRepo = new TeamDbRepository();

            var participantRepo = new ParticipantDbRepository();

            var motorcycleRepo = new MotorcycleDbRepository();

            IContestServices serviceImpl = new ContestServerImpl(userRepo, 
                raceRepo, entryRepo, teamRepo, participantRepo, motorcycleRepo);
        
            var server = new SerialContestServer("127.0.0.1",
                55555, serviceImpl);   
            
            server.Start();
            Console.WriteLine("Server started...");
            
        }
    }
    
    public class SerialContestServer: ConcurrentServer 
    {
        private readonly IContestServices _server;
        private ContestClientObjectWorker _worker;
        public SerialContestServer(string host, int port,
            IContestServices server) : base(host, port)
        {
            this._server = server;
            Console.WriteLine("SerialContestServer...");
        }
        
        protected override Thread CreateWorker(TcpClient client)
        {
            _worker = new ContestClientObjectWorker(_server, client);
            return new Thread(new ThreadStart(_worker.run));
        }
    }
}