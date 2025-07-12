using System;
using System.Collections.Generic;
using UnityEngine;

public static class CommonExtensions {

    public static string ToRankString(this int rank) {
        if (rank < 1 || rank > 13) {
            throw new ArgumentOutOfRangeException(nameof(rank), "Rank must be between 1 and 13.");
        }
        return rank switch {
            1 => "A",
            11 => "J",
            12 => "Q",
            13 => "K",
            _ => rank.ToString()
        };
    }

    public static void Shuffle<T>(this IList<T> list, RandomService random) {
        if (list == null) throw new ArgumentNullException(nameof(list));
        if (random == null) throw new ArgumentNullException(nameof(random));

        int n = list.Count;
        for (int i = n - 1; i > 0; i--) {
            int j = random.Next(i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }

    public static void Shuffle<T>(this Span<T> span, RandomService random) {
        if (span == null) throw new ArgumentNullException(nameof(span));
        if (random == null) throw new ArgumentNullException(nameof(random));

        int n = span.Length;
        for (int i = n - 1; i > 0; i--) {
            int j = random.Next(i + 1);
            (span[i], span[j]) = (span[j], span[i]);
        }
    }

}