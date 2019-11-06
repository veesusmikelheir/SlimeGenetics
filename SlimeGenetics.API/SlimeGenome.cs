
using SRML.SR.SaveSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SlimeGenetics.API
{
    public class SlimeGenome
    {
        List<SlimeTrait> Traits = new List<SlimeTrait>();

        public IEnumerable<SlimeTrait> AllTraits => Traits;

        internal void AddTrait(SlimeTrait trait) => Traits.Add(trait);

        public SlimeTrait this[string index]
        {
            get
            {
                return Traits.FirstOrDefault(x => x.Processor.FullID == index);
            }
            set
            {
                var trait = Traits.FirstOrDefault(x => x.Processor.FullID == index);
                if (trait == null) return;
                Traits.Remove(trait);
                Traits.Add(value);
            }
        }

        public static SlimeGenome GetBaseGenome()
        {
            var genome = new SlimeGenome();
            foreach(var p in SlimeTraitRegistry.Processors)
            {
                genome.AddTrait(p.CreateDefaultTrait());
            }
            return genome;

        }

        public static SlimeGenome CombineGenomes(SlimeGenome original, SlimeGenome other)
        {
            var spliceSettings = new SpliceSettings();
            var newGenome = GetBaseGenome();
            foreach (var trait in original.AllTraits) trait.Processor.AssembleSpliceSettings(trait, spliceSettings);
            foreach(var trait in original.AllTraits)
            {
                newGenome[trait.FullID] = trait.Processor.CombineTraits(trait, other[trait.FullID], spliceSettings);
            }
            return newGenome;
        }

        private SlimeGenome() { }

        public void ConfigureGenome(GameObject obj)
        {
            foreach(var v in Traits)
            {
                v.Processor.ConfigureSlimeTrait(v, obj);
            }
        }

        public void TransformGenome(GameObject obj)
        {
            foreach(var v in Traits)
            {
                v.Processor.MaybeTransformSlimeTrait(v, obj);   
            }
        }

        public void ApplyGenome(GameObject obj)
        {
            foreach(var v in Traits)
            {
                v.Processor.ApplyToGameObject(v, obj);
            }
        }

        public void ReadGenome(CompoundDataPiece piece)
        {
            foreach(var v in piece.DataList.Cast<CompoundDataPiece>())
            {
                var trait = Traits.FirstOrDefault(x => x.Processor.FullID == v.key);
                if (trait == null) break;
                trait.Processor.DeserializeTrait(trait, v);
            }
        }

        public void WriteGenome(CompoundDataPiece piece)
        {
            foreach(var v in Traits)
            {
                if (!v.ShouldBeSaved) continue;
                v.Processor.SerializeTrait(v, piece.GetCompoundPiece(v.Processor.FullID));
            }
        } 
    }
}
