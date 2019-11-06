using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlimeGenetics.API
{
    public class SpliceSettings
    {
        public float MutationFactor = 0.1f; // How likely there is for there to be a mutation
        public float OriginalGeneKeepFactor = .5f; // 1 = parent gene will always keep, 0 = the other gene will always

        public  T PickBetween<T>(T original, T other) => UnityEngine.Random.value < OriginalGeneKeepFactor ? other : original;

        public T PickBetweenMany<T>(T original, params Func<T>[] traits)
        {
            if (UnityEngine.Random.value > OriginalGeneKeepFactor) return original;
            return traits[(int)(UnityEngine.Random.value * traits.Length)]();
        }

        public static T PickRandom<T>(params T[] args) => args[(int)(UnityEngine.Random.value * args.Length)];

        public T MaybeMutate<T>(T original, Func<T,T> mutation) => UnityEngine.Random.value < MutationFactor ? mutation(original) : original;
    }
}
