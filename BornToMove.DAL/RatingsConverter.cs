using System.Collections.Generic;

namespace BornToMove.DAL {
    public class RatingsConverter : Comparer<MoveRating> {
        public override int Compare(MoveRating x, MoveRating y) {
            return x.Rating < y.Rating ? -1 : x.Rating == y.Rating ? 0 : -1;
        }
    }
}