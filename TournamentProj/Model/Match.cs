using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace TournamentProj.Model
{
    public enum Status
    {
        CLOSED,
        OPEN,
        ACTIVE,
        FINISHED
    }
    public class Match
    {

        [Key]
        public int Id { get; set; }

        public int DrawId { get; set; }
        
        public Draw Draw { get; set; }
        
        [Required]
        public int P1Id { get; set; }
        
        [Required]
        public int P2Id { get; set; }
        
        public int round { get; set; }
        
        public Status Status { get; set; }
        
        public bool P1Won { get; set; }
        
        public int P1Games { get; set; }
        public int P2Games { get; set; }
        
        [NotMapped]
        public int[] P2PointsArray
        {
            get
            {
                string[] tab = InternalP2PointsArray.Split(',');
                return Array.ConvertAll(InternalP2PointsArray.Split(';'), int.Parse);
            }
            set
            {
                var data = value;
                InternalP2PointsArray = String.Join(";", data.Select(p => p.ToString()).ToArray());
            }
        }

        public string InternalP2PointsArray { get; set; }
        
        [NotMapped]
        public int[] P1PointsArray
        {
            get
            {
                string[] tab = InternalP1PointsArray.Split(',');
                return Array.ConvertAll(InternalP1PointsArray.Split(';'), int.Parse);
            }
            set
            {
                var data = value;
                InternalP1PointsArray = String.Join(";", data.Select(p => p.ToString()).ToArray());
            }
        }

        public string InternalP1PointsArray { get; set; }

        public int P1DependencyId { get; set; }
        
        public int P2DependencyId { get; set; }
        
        public static Match Clone(Match match)
        {
            return new ()
            {
                Id = match.Id,
                DrawId = match.DrawId,
                P1Id = match.P1Id,
                P2Id = match.P2Id,
                Status = match.Status,
                P1Won = match.P1Won,
                P1Games = match.P1Games,
                P1PointsArray = match.P1PointsArray,
                P2Games = match.P2Games,
                P2PointsArray = match.P2PointsArray,
                P1DependencyId = match.P1DependencyId,
                P2DependencyId = match.P2DependencyId,
                round = match.round
            };
        }

        public void UpdateStatus()
        {
            //Set correct status of match
            if (P1Id == 0 || P2Id == 0)
            { //If one of the players is not yet known
                Status = Status.CLOSED;
            } 
            else if (P1Games == 0 && P2Games == 0)
            { //If points have not yet been reported
                Status = Status.OPEN;
            }
            else
            { //If points have been reported correctly
                Status = Status.FINISHED;
            }
        }
        
    }
}