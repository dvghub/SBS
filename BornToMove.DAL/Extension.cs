using System;
using System.Linq;

namespace BornToMove.DAL {
    public static class Extension {
        public static MoveRating Ratings(this Move move) {
            var crud = new MoveCrud();
            var ratings = crud.ReadRatings(move.Id);
            var vote = ratings.Count > 0 ? (int) Math.Round(ratings.Select(r => r.Vote).Average()) : 1;
            var rating = ratings.Count > 0 ? (int) Math.Round(ratings.Select(r => r.Rating).Average()) : 1;
            return new MoveRating {Move_Id = move.Id, Vote = vote, Rating = rating};
        }
    }
}