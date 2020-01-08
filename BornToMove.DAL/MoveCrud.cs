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
            return _context.Moves.ToList();
        }

        public Move Update(Move move) {
            var old = _context.Moves.Find(move.Id);
            old = move;
            _context.SaveChanges();
            return old;
        }

        public int Delete(Move move) {
            return _context.Remove(move).Entity.Id;
        }
    }
}
