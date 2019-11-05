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
        public abstract String DisplayName { get; }
        public abstract Color DisplayColor { get;  }
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

        string name;
        public override string DisplayName => name + " " + Value;

        public override Color DisplayColor { get; }
        public ValuedSlimeTrait(SlimeTraitProcessor processor, T value,string name, Color color) : base(processor)
        {
            this.name = name;
            Value = value;
            DisplayColor = color;
        }
    }

    public class BooleanSlimeTrait : SlimeTrait
    {
        public BooleanSlimeTrait(SlimeTraitProcessor processor,bool enabled,string name, Color displayColor)  : base(processor)
        {
            Enabled = enabled;
            DisplayName = name;
            DisplayColor = displayColor;
        }

        public override string DisplayName { get; }

        public override Color DisplayColor { get; }

        public bool Enabled { get; set; }

        public override bool ShouldDisplay => Enabled;

        public override bool ShouldApply => Enabled;
    }
}
