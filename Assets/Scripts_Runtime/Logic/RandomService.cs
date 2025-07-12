using RD = System.Random;
using UnityEngine;
using System.Linq;

public class RandomService {

    RD rd;
    public int seed;

    public RandomService() {
        rd = new RD(seed);
    }

    public int Next() {
        return rd.Next();
    }

    public int Next(int min, int max) {
        return rd.Next(min, max);
    }

    public int Next(int max) {
        return rd.Next(max);
    }

    public void SetSeed(int seed) {
        this.seed = seed;
        rd = new RD(seed);
    }

    public T GetWeightedRandom<T>(double[] weights, T[] values) {

        if (weights == null || values == null) {
            Debug.LogError("Weights or values array is null.");
        }
        if (weights.Length != values.Length) {
            Debug.LogError("Weights and values arrays must have the same length.");
        }
        if (weights.Length == 0) {
            Debug.LogError("Weights and values arrays cannot be empty.");
        }

        double totalWeight = weights.Sum();
        if (totalWeight <= 0) {
            Debug.LogError("Total weight must be greater than zero.");
            return default(T);
        }

        double randomNumber = rd.NextDouble() * totalWeight;
        double cumulativeWeight = 0.0;
        for (int i = 0; i < weights.Length; i++) {
            cumulativeWeight += weights[i];
            if (randomNumber <= cumulativeWeight) {
                return values[i];
            }
        }

        return values[values.Length - 1];
    }

    public void Reset() {
        rd = new RD(seed);
    }

}