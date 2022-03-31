using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Mission13Assignment.Models
{
    //Interface for the other Repository file to inherit from.
    public interface IBowlersRepository
    {
        IQueryable<Bowler> Bowlers { get; }
        IQueryable<Team> Teams { get; }

        public void SaveBowler(Bowler bowler);
        public void EditBowler(Bowler bowler);
        public void DeleteBowler(Bowler bowler);
    }
}
