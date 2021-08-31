using System;
using System.Net.Sockets;
using System.Threading;
using Networking;
using Persistance;
using protobuf;
using Services;

namespace Server
{
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

            IContestServices server = new ContestServerImpl(userRepo, 
                raceRepo, entryRepo, teamRepo, participantRepo, motorcycleRepo);
        
            //var scs = new ProtoContestServer("127.0.0.1", 55555, server); 
            SerialContestServer scs = new SerialContestServer("127.0.0.1", 55555, server);
            
            scs.Start();
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
    
    
    public class ProtoContestServer : ConcurrentServer
    {
        private readonly IContestServices _server;
        private ProtoContestWorker _worker;
        public ProtoContestServer(string host, int port, IContestServices server)
            : base(host, port)
        {
            this._server = server;
            Console.WriteLine("ProtoContestServer...");
        }

        protected override Thread CreateWorker(TcpClient client)
        {
            _worker = new ProtoContestWorker(_server, client);
            return new Thread(new ThreadStart(_worker.Run));
        }
    }
    
    
    /*public class SerialChatServer: ConcurrentServer 
    {
        private IContestServices server;
        private ContestClientObjectWorker worker;
        public SerialChatServer(string host, int port, IContestServices server) : base(host, port)
        {
            this.server = server;
            Console.WriteLine("SerialChatServer...");
        }
        protected override Thread CreateWorker(TcpClient client)
        {
            worker = new ContestClientObjectWorker(server, client);
            return new Thread(new ThreadStart(worker.run));
        }
    }*/
}