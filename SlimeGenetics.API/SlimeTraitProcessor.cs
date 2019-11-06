using SRML.SR.SaveSystem.Data;
using System;
using UnityEngine;

namespace SlimeGenetics.API
{
    public abstract class SlimeTraitProcessor
    {
        public string ModID { get; protected set; }
        public string TraitID { get; protected set; }
        public string FullID => ModID + ":" + TraitID;

        public readonly int DefaultDisplayPriority = 1000;

        public abstract Type TraitType { get; }

        public abstract string GetTraitDisplayName(SlimeTrait trait);
        public abstract Color GetTraitDisplayColor(SlimeTrait trait);
        public virtual int GetTraitDisplayPriority(SlimeTrait trait) => DefaultDisplayPriority;
        public abstract string GetTraitDescription(SlimeTrait trait);
        
        public abstract bool TraitNeedsToBeSaved(SlimeTrait Trait);

        public abstract void SerializeTrait(SlimeTrait trait, CompoundDataPiece piece);
        public abstract void DeserializeTrait(SlimeTrait trait,CompoundDataPiece piece);

        /// <summary>
        /// Apply the trait to an object
        /// </summary>
        /// <param name="trait"></param>
        /// <param name="obj"></param>
        public abstract void ApplyToGameObject(SlimeTrait trait, GameObject obj);

        /// <summary>
        /// Provide a new trait
        /// </summary>
        /// <returns></returns>
        public abstract SlimeTrait CreateDefaultTrait();

        /// <summary>
        /// Configure the trait based on the state of the object
        /// </summary>
        /// <param name="trait"></param>
        /// <param name="obj"></param>
        public abstract void ConfigureSlimeTrait(SlimeTrait trait, GameObject obj);

        /// <summary>
        /// Augment the trait in someway after the host object is transformed in some way
        /// </summary>
        /// <param name="trait"></param>
        /// <param name="obj">The object after transformation</param>
        public abstract void MaybeTransformSlimeTrait(SlimeTrait trait, GameObject obj);

        public abstract SlimeTrait CombineTraits(SlimeTrait original, SlimeTrait other, SpliceSettings settings);

        public virtual void AssembleSpliceSettings(SlimeTrait trait, SpliceSettings settings) { }


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

        protected abstract void Deserialize(T trait, CompoundDataPiece piece);
        public override void DeserializeTrait(SlimeTrait trait,CompoundDataPiece piece) => Deserialize((T)trait,piece);

        protected abstract void Serialize(T trait, CompoundDataPiece piece);
        public override void SerializeTrait(SlimeTrait trait, CompoundDataPiece piece) => Serialize((T)trait, piece);

        protected abstract void ConfigureSlimeTrait(T trait, GameObject obj);
        public override void ConfigureSlimeTrait(SlimeTrait trait, GameObject obj) => ConfigureSlimeTrait((T)trait,obj);

        protected abstract void MaybeTransformSlimeTrait(T trait, GameObject obj);
        public override void MaybeTransformSlimeTrait(SlimeTrait trait, GameObject obj) => MaybeTransformSlimeTrait((T)trait, obj);

        public override string GetTraitDisplayName(SlimeTrait trait) => GetTraitDisplayName((T)trait);
        protected abstract string GetTraitDisplayName(T trait);

        public override Color GetTraitDisplayColor(SlimeTrait trait) => GetTraitDisplayColor((T)trait);
        protected abstract Color GetTraitDisplayColor(T trait);

        public override bool TraitNeedsToBeSaved(SlimeTrait trait)=>TraitNeedsToBeSaved((T)trait);
        protected abstract bool TraitNeedsToBeSaved(T trait);

        protected virtual int GetTraitDisplayPriority(T trait) => DefaultDisplayPriority;
        public override int GetTraitDisplayPriority(SlimeTrait trait) => GetTraitDisplayPriority((T)trait);

        protected abstract T CombineTraits(T original, T other, SpliceSettings settings);
        public override SlimeTrait CombineTraits(SlimeTrait original, SlimeTrait other, SpliceSettings settings) => CombineTraits((T)original, (T)other, settings);

        protected virtual void AssembleSpliceSettings(T trait, SpliceSettings settings) { }
        public override void AssembleSpliceSettings(SlimeTrait trait, SpliceSettings settings) => AssembleSpliceSettings((T)trait, settings);

        public override string GetTraitDescription(SlimeTrait trait) => GetTraitDescription((T)trait);
        protected abstract string GetTraitDescription(T trait);
    }

    public abstract class BooleanSlimeTraitProcessor : SlimeTraitProcessor<BooleanSlimeTrait>
    {
        public readonly int DefaultBooleanDisplayPriority = 2000;

        protected override void Deserialize(BooleanSlimeTrait trait, CompoundDataPiece piece)
        {
            trait.Enabled = piece.GetValue<bool>("enabled");
        }

        protected override void Serialize(BooleanSlimeTrait trait, CompoundDataPiece piece)
        {
            piece.SetValue<bool>(trait.Enabled);
        }

        public override void ApplyToGameObject(BooleanSlimeTrait trait, GameObject obj)
        {
            if (trait.Enabled) ApplyBooleanTraitToGameObject(obj);
        }

        protected abstract void ApplyBooleanTraitToGameObject(GameObject obj);

        public override SlimeTrait CreateDefaultTrait() => new BooleanSlimeTrait(this, false);

        protected override bool TraitNeedsToBeSaved(BooleanSlimeTrait trait) => trait.Enabled;

        protected override string GetTraitDisplayName(BooleanSlimeTrait trait) => displayName;

        protected override Color GetTraitDisplayColor(BooleanSlimeTrait trait) => displayColor;

        protected override int GetTraitDisplayPriority(BooleanSlimeTrait trait) => DefaultBooleanDisplayPriority;

        string displayName;
        Color displayColor;

        public BooleanSlimeTraitProcessor(string modid, string traitid, string displayname, Color displayColor) : base(modid,traitid)
        {
            displayName = displayname;
            this.displayColor = displayColor;
        }
    }

    public abstract class ValuedSlimeTraitProcessor<T> : SlimeTraitProcessor<ValuedSlimeTrait<T>>
    {
        public ValuedSlimeTraitProcessor(string modid, string traitid) : base(modid, traitid)
        {
            DataPiece.GetTypeID(typeof(T));
        }

        protected override void Deserialize(ValuedSlimeTrait<T> trait, CompoundDataPiece piece)
        {
            trait.Value = piece.GetValue<T>("value");
        }

        protected override void Serialize(ValuedSlimeTrait<T> trait, CompoundDataPiece piece)
        {
            piece.SetValue("value", trait.Value);
        }

        protected override bool TraitNeedsToBeSaved(ValuedSlimeTrait<T> trait) => true;

        public override SlimeTrait CreateDefaultTrait() => new ValuedSlimeTrait<T>(this, default(T));
    }
}
