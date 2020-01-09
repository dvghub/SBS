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
        
        public UsableMove Read(int id) {
            var move = _context.Moves.Find(id);
            var moveRatings = _context.Ratings.ToList();
            var votes = new List<int>();
            var ratings = new List<int>();
            
            moveRatings.ForEach((r) => {
                if (!r.Move_Id.Equals(move.Id)) return;
                votes.Add(r.Vote);
                ratings.Add(r.Rating);
            });
            
            int vote;
            int rating;
                
            if (votes.Count != 0) {
                vote = (int) Math.Round(votes.Average());
                rating = (int) Math.Round(ratings.Average());
            } else {
                vote = 1;
                rating = 1;
            }
            
            var usable = new UsableMove {
                Id = move.Id,
                Name = move.Name,
                Description = move.Description,
                Vote = vote,
                Rating = rating
            };
            
            return usable;
        }
        
        public List<UsableMove> Read() {
            List<Move> moves = _context.Moves.ToList();
            var moveRatings = _context.Ratings.ToList();
            var votes = new List<int>();
            var ratings = new List<int>();
            var usables = new List<UsableMove>();
            
            moves.ForEach((move) => {
                moveRatings.ForEach((r) => {
                    if (!r.Move_Id.Equals(move.Id)) return;
                    votes.Add(r.Vote);
                    ratings.Add(r.Rating);
                });

                int vote;
                int rating;
                
                if (votes.Count != 0) {
                    vote = (int) Math.Round(votes.Average());
                    rating = (int) Math.Round(ratings.Average());
                } else {
                    vote = 1;
                    rating = 1;
                }
                
                var usable = new UsableMove {
                    Id = move.Id,
                    Name = move.Name,
                    Description = move.Description,
                    Vote = vote,
                    Rating = rating
                };
                
                usables.Add(usable);
            });
            return usables;
        }

        public UsableMove Update(UsableMove move) {
            var old = _context.Moves.Find(move.Id);
            old = move;
            _context.SaveChanges();
            return new UsableMove {
                Id = move.Id,
                Name = move.Name,
                Description = move.Description
            };
        }

        public void Update(int[] ratings) {
            var rating = new MoveRating {Move_Id = ratings[0], Vote = ratings[1], Rating = ratings[2]};
            _context.Ratings.Add(rating);
        }

        public int Delete(Move move) {
            return _context.Remove(move).Entity.Id;
        }
    }
}
