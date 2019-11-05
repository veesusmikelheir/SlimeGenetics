using MonomiPark.SlimeRancher.Regions;
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

        public static SlimeGenome GetBaseGenome()
        {
            var genome = new SlimeGenome();
            foreach(var p in SlimeTraitRegistry.Processors)
            {
                genome.AddTrait(p.GetDefaultTrait());
            }
            return genome;

        }

        private SlimeGenome() { }

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
                v.Processor.SerializeTrait(v, piece.GetCompoundPiece(v.Processor.FullID));
            }
        } 
    }
}
