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
        
        public int P1Match { get; set; }
        public int P1Games { get; set; }
        public int P1Sets { get; set; }
        public int P2Match { get; set; }
        public int P2Games { get; set; }
        public int P2Sets { get; set; }
        
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
        
    }
}