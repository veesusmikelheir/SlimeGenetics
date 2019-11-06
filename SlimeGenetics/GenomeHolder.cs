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
    public class GenomeHolder : MonoBehaviour, ExtendedData.Participant, IGeneticsProvider
    {
        public SlimeGenome Genome { get; protected set; } = SlimeGenome.GetBaseGenome();
        public virtual void ReadData(CompoundDataPiece piece)
        {

            Genome.ReadGenome(piece);
        }

        public virtual void WriteData(CompoundDataPiece piece)
        {

            Genome.WriteGenome(piece);
        }
    }
}
