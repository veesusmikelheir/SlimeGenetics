using SlimeGenetics.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SlimeGenetics.SlimeTraits
{
    public class AppetiteTraitProcessor : ValuedSlimeTraitProcessor<int>
    {
        public AppetiteTraitProcessor(string modid, string traitid) : base(modid, traitid)
        {
        }

        public override void ApplyToGameObject(ValuedSlimeTrait<int> trait, GameObject obj)
        {
            
        }

        protected override ValuedSlimeTrait<int> CombineTraits(ValuedSlimeTrait<int> original, ValuedSlimeTrait<int> other, SpliceSettings settings)
        {
            var trait = settings.PickBetween(original, other);
            return settings.MaybeMutate(trait, (x) =>
            {
                x.Value += (int)Mathf.Sign(UnityEngine.Random.value * 2 - 1);
                return x;
            });
        }

        protected override void ConfigureSlimeTrait(ValuedSlimeTrait<int> trait, GameObject obj)
        {
            
        }

        protected override string GetTraitDescription(ValuedSlimeTrait<int> trait)
        {
            throw new NotImplementedException();
        }

        protected override Color GetTraitDisplayColor(ValuedSlimeTrait<int> trait)
        {
            throw new NotImplementedException();
        }

        protected override string GetTraitDisplayName(ValuedSlimeTrait<int> trait)
        {
            
        }

        protected override void MaybeTransformSlimeTrait(ValuedSlimeTrait<int> trait, GameObject obj)
        {
            
        }
    }
}
