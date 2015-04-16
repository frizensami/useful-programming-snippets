using System;
using NamedPipeWrapper;

namespace NamedPipeWrapper
{
    class MyServer
    {

        NamedPipeServer<string> server;
        public MyServer(string pipeName)
        {
            server = new NamedPipeServer<string>(pipeName);
            server.ClientConnected += OnClientConnected;
            server.ClientDisconnected += OnClientDisconnected;
            server.ClientMessage += OnClientMessage;
            server.Error += OnError;
            server.Start();
        }

        private void OnClientConnected(NamedPipeConnection<string, string> connection)
        {
            Console.WriteLine("Client {0} is now connected!", connection.Id);
            connection.PushMessage("Welcome!");
            Console.WriteLine("Just pushed welcome message");
        }

        private void OnClientDisconnected(NamedPipeConnection<string, string> connection)
        {
            Console.WriteLine("Client {0} disconnected", connection.Id);
        }

        private void OnClientMessage(NamedPipeConnection<string, string> connection, string message)
        {
            Console.WriteLine("Client {0} says: {1}", connection.Id, message);
        }

        private void OnError(Exception exception)
        {
            Console.Error.WriteLine("ERROR: {0}", exception);
        }

        public void SendMessage(string message)
        {
            server.PushMessage(message);
        }
    }
}