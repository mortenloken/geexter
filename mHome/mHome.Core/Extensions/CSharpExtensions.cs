using System;
using System.Collections.Generic;
using System.Linq;

namespace mHome.Core.Extensions {
    public static class CSharpExtensions {
        public static T Random<T>(this IEnumerable<T> enumerable) where T : class {
            var lst = enumerable.ToList();
            if(!lst.Any()) throw new ApplicationException("Cannot randomize empty enumerable.");
            return lst[new Random().Next(0, lst.Count)];
        }

        public static IEnumerable<T> Arrayify<T>(this T s) {
            return new[] {s};
        }
    }
}