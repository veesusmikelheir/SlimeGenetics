using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlimeGenetics.API
{
    public static class SlimeTraitRegistry
    {
        internal static List<SlimeTraitProcessor> Processors = new List<SlimeTraitProcessor>();

        public static void Register(SlimeTraitProcessor processor) => Processors.Add(processor);
    }
}
