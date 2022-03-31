using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13Assignment.Models
{
    public class EFBowlerRepository : IBowlersRepository
    {
        private BowlersDbContext _context { get; set; }

        public EFBowlerRepository(BowlersDbContext temp)
        {
            _context = temp;
        }

        //Creates IQueryables of bowlers and teams for the repository to access
        public IQueryable<Bowler> Bowlers => _context.bowlers.Include(x => x.Team);
        public IQueryable<Team> Teams => _context.teams;

        //Function that saves new bowlers to the database
        public void SaveBowler(Bowler bowler)
        {
            _context.Add(bowler);
            _context.SaveChanges();
        }

        //Function that updates the bowler information when edited
        public void EditBowler(Bowler bowler)
        { 
            _context.Update(bowler);
            _context.SaveChanges();
        }

        //Function to delete bowlers from the database
        public void DeleteBowler(Bowler bowler)
        {
            _context.Remove(bowler);
            _context.SaveChanges();
        }
    }
}
