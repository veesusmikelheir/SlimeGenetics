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

        public abstract string GetTraitDisplayName(SlimeTrait trait);
        public abstract Color GetTraitDisplayColor(SlimeTrait trait);
        public abstract string GetTraitDescription(SlimeTrait trait);

        public abstract void SerializeTrait(SlimeTrait trait, CompoundDataPiece piece);
        public abstract void DeserializeTrait(SlimeTrait trait,CompoundDataPiece piece);

        public abstract void ApplyToGameObject(SlimeTrait trait, GameObject obj);

        public abstract SlimeTrait GetDefaultTrait();

        public abstract void ConfigureSlimeTrait(SlimeTrait trait, SlimeTraitSettings settings);

        public SlimeTraitProcessor(string modid, string traitid)
        {
            ModID = modid;
            TraitID = traitid;
        }
    }

    public abstract class SlimeTraitProcessor<T> : SlimeTraitProcessor where T : SlimeTrait
    {
        public SlimeTraitProcessor(string modid, string traitid) : base(modid, traitid)
        {
        }

        public override Type TraitType => typeof(T);

        public override void ApplyToGameObject(SlimeTrait trait, GameObject obj) => ApplyToGameObject((T)trait, obj);
        public abstract void ApplyToGameObject(T trait, GameObject obj);

        public abstract void Deserialize(T trait, CompoundDataPiece piece);
        public override void DeserializeTrait(SlimeTrait trait,CompoundDataPiece piece) => Deserialize((T)trait,piece);

        public abstract void Serialize(T trait, CompoundDataPiece piece);
        public override void SerializeTrait(SlimeTrait trait, CompoundDataPiece piece) => Serialize((T)trait, piece);

        public abstract void ConfigureSlimeTrait(T trait, SlimeTraitSettings settings);
        public override void ConfigureSlimeTrait(SlimeTrait trait, SlimeTraitSettings settings) => ConfigureSlimeTrait((T)trait,settings);


        public override string GetTraitDisplayName(SlimeTrait trait) => GetTraitDisplayName((T)trait);
        public abstract string GetTraitDisplayName(T trait);

        public override Color GetTraitDisplayColor(SlimeTrait trait) => GetTraitDisplayColor((T)trait);
        public abstract Color GetTraitDisplayColor(T trait);



        public override string GetTraitDescription(SlimeTrait trait) => GetTraitDescription((T)trait);
        public abstract string GetTraitDescription(T trait);
    }

    public abstract class BooleanSlimeTraitProcessor : SlimeTraitProcessor<BooleanSlimeTrait>
    {

        public override void Deserialize(BooleanSlimeTrait trait, CompoundDataPiece piece)
        {
            trait.Enabled = piece.GetValue<bool>("enabled");
        }

        public override void Serialize(BooleanSlimeTrait trait, CompoundDataPiece piece)
        {
            piece.SetValue<bool>(trait.Enabled);
        }

        public override SlimeTrait GetDefaultTrait() => new BooleanSlimeTrait(this, false, displayName, displayColor);
        

        string displayName;
        Color displayColor;

        public BooleanSlimeTraitProcessor(string modid, string traitid, string displayname, Color displayColor) : base(modid,traitid)
        {
            displayName = displayname;
            this.displayColor = displayColor;
        }
    }


}
