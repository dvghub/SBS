using System.Collections.Generic;

namespace BornToMove.DAL {
    public class RatingsConverter : Comparer<Move> {
        public override int Compare(Move x, Move y) {
            var result = y.Ratings().Rating.CompareTo(x.Ratings().Rating);
            return result != 0 ? result : 0;
        }
    }
}
