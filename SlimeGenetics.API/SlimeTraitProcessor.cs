using SRML.SR.SaveSystem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SlimeGenetics.API
{
    public abstract class SlimeTraitProcessor
    {
        public string ModID { get; protected set; }
        public string TraitID { get; protected set; }
        public string FullID => ModID + ":" + TraitID;

        public abstract Type TraitType { get; }

        public abstract void SerializeTrait(SlimeTrait trait, CompoundDataPiece piece);
        public abstract SlimeTrait DeserializeTrait(CompoundDataPiece piece);

        public abstract void ApplyToGameObject(SlimeTrait trait, GameObject obj);

        public abstract void AddToGenome(SlimeGenome genome,GameObject obj);
    }

    public abstract class SlimeTraitProcessor<T> : SlimeTraitProcessor where T : SlimeTrait
    {
        public override Type TraitType => typeof(T);

        public override void ApplyToGameObject(SlimeTrait trait, GameObject obj) => ApplyToGameObject((T)trait, obj);
        public abstract void ApplyToGameObject(T trait, GameObject obj);

        public abstract T Deserialize(CompoundDataPiece piece);
        public override SlimeTrait DeserializeTrait(CompoundDataPiece piece) => Deserialize(piece);

        public abstract void Serialize(T trait, CompoundDataPiece piece);
        public override void SerializeTrait(SlimeTrait trait, CompoundDataPiece piece) => Serialize((T)trait, piece);
    }

}
