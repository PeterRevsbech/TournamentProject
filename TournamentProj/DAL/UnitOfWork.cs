using System;
using TournamentProj.Context;
using TournamentProj.Model;

namespace TournamentProj.DAL
{
    public class UnitOfWork : IDisposable
    {
        private TournamentContext context = new TournamentContext();
        private Repository<Draw> drawRepository;
        private Repository<Match> matchRepository;
        private Repository<Tournament> tournamentRepository;
        private Repository<Player> playerRepository;

        public Repository<Draw> DrawRepository
        {
            get
            {

                if (this.drawRepository == null)
                {
                    this.drawRepository = new Repository<Draw>(context);
                }
                return drawRepository;
            }
        }
        
        public Repository<Match> MatchRepository
        {
            get
            {

                if (this.matchRepository == null)
                {
                    this.matchRepository = new Repository<Match>(context);
                }
                return matchRepository;
            }
        }

        
        public Repository<Player> PlayerRepository
        {
            get
            {

                if (this.playerRepository == null)
                {
                    this.playerRepository = new Repository<Player>(context);
                }
                return playerRepository;
            }
        }

        
        public Repository<Tournament> TournamentRepository
        {
            get
            {

                if (this.tournamentRepository == null)
                {
                    this.tournamentRepository = new Repository<Tournament>(context);
                }
                return tournamentRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}