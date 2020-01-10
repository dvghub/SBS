﻿using System.Collections.Generic;

namespace Organiser {
    public class Comparer : IComparer<int> {
        public int Compare(int x, int y) {
            return x < y ? -1 : x == y ? 0 : 1;
        }
    }
}
