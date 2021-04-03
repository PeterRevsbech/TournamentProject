using System.Collections.Generic;
using System.Linq;
using TournamentProj.Context;
using TournamentProj.DAL;
using TournamentProj.DAL.MatchDependencyRepository;
using TournamentProj.Model;

namespace TournamentProj.Services.MatchService
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMatchDependencyRepository _matchDependencyRepository;
        private readonly IDrawRepository _drawRepository;
        private readonly ITournamentContext _dbContext;

        public MatchService(ITournamentContext dbContext,
            IMatchRepository matchRepository,
            IMatchDependencyRepository matchDependencyRepository,
            IDrawRepository drawRepository)
        {
            _dbContext = dbContext;
            _matchRepository = matchRepository;
            _matchDependencyRepository = matchDependencyRepository;
            _drawRepository = drawRepository;
        }

        public Match Create(Match match)
        {
            _matchRepository.Insert(match);
            _dbContext.SaveChanges();
            return match;
        }

        public Match Get(int id)
        {
            return _matchRepository.FindById(id);
        }

        public IEnumerable<Match> GetByPlayerId (int playerId)
        {
            return _matchRepository.FindByPlayerId(playerId);
        }

        public IEnumerable<Match> GetByDrawId(int drawId)
        {
            return _matchRepository.FindByDrawId(drawId);
        }

        public IEnumerable<Match> GetAll()
        {
            return _matchRepository.FindAll();
        }

        public Match Delete(int id)
        {
            var match = _matchRepository.FindById(id);
            _matchRepository.Delete(match);
            _dbContext.SaveChanges();
            return match;
        }

        public Match Update(Match match)
        {
            Match oldMatch = _matchRepository.FindById(match.Id);
            
            if (oldMatch.Status != Status.FINISHED && match.Status == Status.FINISHED)
            { //If match was not finished before, but is now
                MatchFinished(match);
            }

            if ((match.P1Id == -1 || match.P2Id == -1)
                && (oldMatch.P1Id == 0 || oldMatch.P2Id == 0)
                && !(match.P1Id == 0 || match.P2Id == 0))
            { //If match has a bye and opponent was not previously known
                ByeMatchNowHasOpponent(match);
            }
            
            _matchRepository.Update(match);
            _dbContext.SaveChanges();
            return match;
        }

        private void MatchFinished(Match match)
        {
            //Match has been updated and is now Finished.
            //If any other matches are dependent on the result of this match - they should be updated
            var dependentMatches = _matchRepository
                .FindByDrawId(match.DrawId)
                .ToArray()
                .Where(m => (
                    (m.P1DependencyId != 0 && _matchDependencyRepository.FindById(m.P1DependencyId).DependencyId == match.Id)
                    || (m.P2DependencyId != 0 && _matchDependencyRepository.FindById(m.P2DependencyId).DependencyId == match.Id))
                    )
                .ToArray();
            
            //For each dependent match - update the players
            foreach (var dependentMatch in dependentMatches)
            {
                MatchDependency matchDependency;
                if (dependentMatch.P1DependencyId == match.Id)
                {
                    matchDependency = _matchDependencyRepository.FindById(dependentMatch.P1DependencyId);
                    dependentMatch.P1Id = FindResultingPlayerFromMatchDependency(matchDependency);
                }
                else
                {
                    matchDependency = _matchDependencyRepository.FindById(dependentMatch.P2DependencyId);
                    dependentMatch.P2Id = FindResultingPlayerFromMatchDependency(matchDependency);
                }
            }
        }

        private int FindResultingPlayerFromMatchDependency(MatchDependency matchDependency)
        {
            var match = _matchRepository.FindById(matchDependency.DependencyId);
            var wantedPosition = matchDependency.Position;
            if ((match.P1Won && wantedPosition == 1) || (!match.P1Won && wantedPosition == 2) )
            {
                return match.P1Id;
            }
            else
            {
                return match.P2Id;
            }
        }
        
        private void ByeMatchNowHasOpponent(Match match)
        {
            //Find the corresponding draw to find the scoring system
            Draw draw = _drawRepository.FindById(match.DrawId);

            //Opponent automatically wins the match and match is finished
            if (match.P1Id == -1)
            { //If P1 was the bye
                match.P1Won = false;
                match.P2Match = 1;
                match.P2Games = draw.Games;
                match.P2Sets = draw.Sets;
                match.P2Points = draw.Points;
            }
            else
            { //If P2 was the bye
                match.P1Won = true;
                match.P1Match = 1;
                match.P1Games = draw.Games;
                match.P1Sets = draw.Sets;
                match.P1Points = draw.Points;
            }
            
            MatchFinished(match);
        }
        
        
    }
}