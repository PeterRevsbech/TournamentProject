using System.Collections.Generic;
using System.Linq;
using TournamentProj.Exceptions;
using TournamentProj.Model;

namespace TournamentProj.Services.DrawCreationLogic
{
    public static class DrawCreator
    {
        public static Draw GenerateDraw(DrawCreation drawCreation)
        {
            //TODO add logic here for recognizing specific score types, e.g. tennis, squash so on 
            Draw draw = new Draw();

            switch (drawCreation.DrawType)
            {
                case DrawType.KO:
                    break;
                case DrawType.RR:
                    ConfigureMonrad(draw, drawCreation);
                    break;
                case DrawType.MONRAD:
                    break;
                default:
                    throw new TournamentSoftwareException("Tried to create a draw with no DrawType specified.");
            }
            
            draw.Name = drawCreation.Name;
            draw.Sets = drawCreation.Sets;
            draw.Games = drawCreation.Games;
            draw.Points = drawCreation.Points;
            draw.TournamentId = drawCreation.TournamentId;
            draw.DrawType = drawCreation.DrawType;
            draw.TieBreaks = drawCreation.TieBreaks;
            
            return draw;
        }

        public static void ConfigureMonrad(Draw draw, DrawCreation drawCreation )
        {
            //Everybody plays against everybody
            
            //All matches are open from the beginning
            //Highest seeded players should play each other in the last round, but this is up
            //to the user, since the matches are open anyway

            int[] playerIds = drawCreation.playerIds.ToArray();
            
            IEnumerable<Match> matches = draw.Matches;
            matches = new List<Match>();
            
            for (int i = 0; i< playerIds.Length; i++)
            {
                for (int j = i+1; j < playerIds.Length; j++)
                {
                    matches.Append(
                        new Match()
                        {
                            P1Id = playerIds[i],
                            P2Id = playerIds[j],
                            //DrawId = drawCreation TODO find out if this causes trouble - that the id is not specified - alternatively create the draw first and then add matches
                            Status = Status.OPEN
                        }
                    );
                }
            }
        }
        
    }
}