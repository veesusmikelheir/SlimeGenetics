using SlimeGenetics.API;
using SRML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SlimeGenetics.SlimeTraits
{
    class TemperamentTraitProcessor : ValuedSlimeTraitProcessor<float>
    {
        public override void ApplyToGameObject(ValuedSlimeTrait<float> trait, GameObject obj)
        {
        
        }

        protected override ValuedSlimeTrait<float> CombineTraits(ValuedSlimeTrait<float> original, ValuedSlimeTrait<float> other, SpliceSettings settings)
        {
            return UnityEngine.Random.value > .5 ? original : other;
        }

        public static readonly Dictionary<Identifiable.Id, Temperament> SlimeTemperamentMap = new Dictionary<Identifiable.Id, Temperament>()
        {
            {Identifiable.Id.PINK_SLIME , Temperament.Jubilant },
            {Identifiable.Id.HUNTER_SLIME,Temperament.Aggressive },
            {Identifiable.Id.ROCK_SLIME,Temperament.Irritable },
            {Identifiable.Id.TABBY_SLIME,Temperament.Agreeable },
            {Identifiable.Id.RAD_SLIME,Temperament.Jubilant }
        };

        static TemperamentTraitProcessor()
        {
            IntermodCommunication.RegisterIntermodMethod("setslimetemperament", (Identifiable.Id id, int temperament) => SlimeTemperamentMap[id] = (Temperament)temperament);
        }

        public TemperamentTraitProcessor(string modid, string traitid) : base(modid, traitid)
        {
        }

        static int TryGetFromTemperamentMap(Identifiable.Id id) => SlimeTemperamentMap.TryGetValue(id, out var val) ? (int)val : 0;

        public static int GetSlimeTemperament(Identifiable.Id id)
        {
            if (!Identifiable.IsSlime(id)) throw new Exception($"{id} is not a slime!");
            if (!Identifiable.IsLargo(id)) return TryGetFromTemperamentMap(id);
            return GameContext.Instance.SlimeDefinitions.GetSlimeByIdentifiableId(id).BaseSlimes.Select(x => x.IdentifiableId).Select(x => GetSlimeTemperament(x)).Sum();
        }

        protected override void ConfigureSlimeTrait(ValuedSlimeTrait<float> trait, GameObject obj)
        {
            trait.Value = GetSlimeTemperament(Identifiable.GetId(obj));
        }

        protected override string GetTraitDescription(ValuedSlimeTrait<float> trait) => "How quickly this slime gets agitated";

        protected override Color GetTraitDisplayColor(ValuedSlimeTrait<float> trait)
        {
            if (trait.Value > 0) return new Color(0, Mathf.Clamp01(trait.Value / 2f), 0);
            if (trait.Value < 0) return new Color(Mathf.Clamp01(Mathf.Abs(trait.Value / 2f)), 0,0);
            return Color.black;
        }

        protected override string GetTraitDisplayName(ValuedSlimeTrait<float> trait)
        {
            return "Temperament: "+GetTemperamentName((int)trait.Value);
        }

        protected override void MaybeTransformSlimeTrait(ValuedSlimeTrait<float> trait, GameObject obj)
        {
            ConfigureSlimeTrait(trait, obj);
        }

        public static string GetTemperamentName(int temperament)
        {
            var min = Enum.GetValues(typeof(Temperament)).Cast<int>().Min();
            var max = Enum.GetValues(typeof(Temperament)).Cast<int>().Max();
            bool isPlus = temperament < min || temperament > max;
            temperament = Mathf.Clamp(temperament,min,max );

            return Enum.GetName(typeof(Temperament),((Temperament)temperament)) + (isPlus?"+":"");
        }

        public enum Temperament 
        {
            Jubilant=2,
            Agreeable = 1,
            Vibing=0,
            Irritable=-1,
            Aggressive = -2
        }
    }
}
