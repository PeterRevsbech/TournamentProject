using System;

namespace TournamentProj.Exceptions
{
    public class TournamentSoftwareException : Exception
    {
        public TournamentSoftwareException(string message) : base(message)
        {
            Console.WriteLine(message);
        }
    }
}