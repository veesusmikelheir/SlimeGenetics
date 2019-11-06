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
        bool configured;

        public override void ReadData(CompoundDataPiece piece)
        {
            configured = true;
            base.ReadData(piece);
        }

        public void Start()
        {
            if (!configured)
            {
                
                configured = true;

                Genome.ConfigureGenome(gameObject);
            }
            Genome.ApplyGenome(gameObject);

        }
    }
}
