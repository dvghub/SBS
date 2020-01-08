using System;
using System.Collections.Generic;
using System.Linq;
using BornToMove.DAL;

namespace BornToMove.Business {
    public class BuMove {
        private readonly MoveCrud _crud = new MoveCrud();

        public int MakeThis(Move move) {
            return IsValidMove(move) ? _crud.Create(move) : 0;
        }

        public Move GimmeThis(int id) {
            return _crud.Read(id);
        }
        
        public Move GimmeRandom() {
            var random = new Random().Next(0, _crud.Read().Count());
            return _crud.Read().OrderBy((item) => item.Id).Skip(random).Take(1).First();
        }

        public List<Move> GimmeAll() {
            return _crud.Read();
        }

        public Move ChangeThis(Move move) {
            return IsValidMove(move) ? _crud.Update(move) : move;
        }

        public int GetRid(Move move) {
            return _crud.Delete(move);
        }

        private static bool IsValidMove(Move move) {
            var valid = !(move.Name.Length > 25);
            if (move.Description.Length > 99) valid = false;
            if (move.Rating < 1 || move.Rating > 5) valid = false;
            return valid;
        }
    }
}
