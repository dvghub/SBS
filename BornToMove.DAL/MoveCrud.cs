using System;
using System.Collections.Generic;
using System.Linq;

namespace BornToMove.DAL {
    public class MoveCrud {
        private readonly MoveContext _context = new MoveContext();
        
        public int Create(Move move) {
            var temp = _context.Add(move).Entity.Id;
            _context.SaveChanges();
            return temp;
        }
        
        public Move Read(int id) {
            return _context.Moves.Find(id);
        }
        
        public List<Move> Read() {
            return _context.Moves.ToList().OrderByDescending((move) => move.Ratings().Rating).ToList();
        }

        public List<MoveRating> ReadRatings(int id) {
            return _context.Ratings.Where(rating => rating.Move_Id == id).ToList();
        }

        public Move Update(Move move) {
            var old = _context.Moves.Find(move.Id);
            old.Name = move.Name;
            old.Description = move.Description;
            _context.SaveChanges();
            return move;
        }

        public void Update(int[] ratings) {
            var rating = new MoveRating {Move_Id = ratings[0], Vote = ratings[1], Rating = ratings[2]};
            _context.Ratings.Add(rating);
            _context.SaveChanges();
        }

        public int Delete(Move move) {
            return _context.Remove(move).Entity.Id;
        }
    }
}
