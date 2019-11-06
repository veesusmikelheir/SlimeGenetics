using SlimeGenetics.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SlimeGenetics.SlimeTraits
{
    public class DietTraitProcessor : ValuedSlimeTraitProcessor<SlimeEat.FoodGroup[]>
    {
        public DietTraitProcessor(string modid, string traitid) : base(modid, traitid)
        {
        }

        public override void ApplyToGameObject(ValuedSlimeTrait<SlimeEat.FoodGroup[]> trait, GameObject obj)
        {
            
        }

        public override SlimeTrait CreateDefaultTrait() => new ValuedSlimeTrait<SlimeEat.FoodGroup[]>(this, new SlimeEat.FoodGroup[0]);

        protected override ValuedSlimeTrait<SlimeEat.FoodGroup[]> CombineTraits(ValuedSlimeTrait<SlimeEat.FoodGroup[]> original, ValuedSlimeTrait<SlimeEat.FoodGroup[]> other, SpliceSettings settings)
        {
            var trait = CreateDefaultTrait() as ValuedSlimeTrait<SlimeEat.FoodGroup[]>;
            trait.Value = original.Value.Concat(other.Value).Distinct().ToArray();
            return trait;
        }

        protected override void ConfigureSlimeTrait(ValuedSlimeTrait<SlimeEat.FoodGroup[]> trait, GameObject obj)
        {
            trait.Value = obj.GetComponent<SlimeEat>()?.slimeDefinition.Diet.MajorFoodGroups ?? trait.Value;
        }

        protected override string GetTraitDescription(ValuedSlimeTrait<SlimeEat.FoodGroup[]> trait)
        {
            if (trait.Value.Contains(SlimeEat.FoodGroup.GINGER)) return "Only eats gilded gingers";
            return "The foods this slime will eat";
        }

        protected override Color GetTraitDisplayColor(ValuedSlimeTrait<SlimeEat.FoodGroup[]> trait)
        {
            if (trait.Value.Contains(SlimeEat.FoodGroup.GINGER)) return Color.yellow;
            return Color.black;
        }

        protected override string GetTraitDisplayName(ValuedSlimeTrait<SlimeEat.FoodGroup[]> trait)
        {
            if (trait.Value.Contains(SlimeEat.FoodGroup.GINGER)) return "Royal Diet";
            return $"Diet: {GameContext.Instance.MessageDirector.GetBundle("ui").Xlate(GameContext.Instance.SlimeDefinitions.Slimes.First().Diet.GetGroupsMsg(trait.Value))}";
        }

        protected override void MaybeTransformSlimeTrait(ValuedSlimeTrait<SlimeEat.FoodGroup[]> trait, GameObject obj)
        {
            var temp = trait.Value;
            ConfigureSlimeTrait(trait, obj);
            trait.Value = temp.Concat(trait.Value).Distinct().ToArray();
        }
    }
}
