using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SlimeGenetics.API
{
    public abstract class SlimeTrait
    {
        public SlimeTraitProcessor Processor { get; }
        public virtual String DisplayName => Processor.GetTraitDisplayName(this);
        public virtual Color DisplayColor => Processor.GetTraitDisplayColor(this);
        public virtual bool ShouldDisplay { get; } = true;
        public virtual bool ShouldApply { get; } = true;

        public SlimeTrait(SlimeTraitProcessor processor)
        {
            Processor = processor;
        }
    }
    public class ValuedSlimeTrait<T> : SlimeTrait
    {
        public virtual T Value { get; set; }

        public override string DisplayName => base.DisplayName + " " + Value;

        public ValuedSlimeTrait(SlimeTraitProcessor processor, T value) : base(processor)
        {
            Value = value;
        }
    }

    public class BooleanSlimeTrait : SlimeTrait
    {
        public BooleanSlimeTrait(SlimeTraitProcessor processor,bool enabled)  : base(processor)
        {
            Enabled = enabled;
        }

        public bool Enabled { get; set; }

        public override bool ShouldDisplay => Enabled;

        public override bool ShouldApply => Enabled;
    }
}
