using SlimeGenetics.API;
using SRML.SR.SaveSystem;
using SRML.SR.SaveSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SlimeGenetics
{
    public class SlimeGeneticsHandler : GenomeHolder {
        HashSet<string> toConfigure = new HashSet<string>();
        bool configured;
        public override void ReadData(CompoundDataPiece piece)
        {
            configured = true;
            foreach(var v in Genome.AllTraits.Select(x => x.FullID).Where(x=>!piece.DataList.Any(y=>y.key==x)))
            {
                toConfigure.Add(v);
            }
            base.ReadData(piece);
        }

        public void Start()
        {
            if (!configured)
            {
                
                configured = true;

                Genome.ConfigureGenome(gameObject);
            }
            else
            {
                foreach(var v in toConfigure)
                {
                    Genome[v].Processor.ConfigureSlimeTrait(Genome[v], gameObject);
                }
            }
            Genome.ApplyGenome(gameObject);

        }
    }
}
