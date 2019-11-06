using SlimeGenetics.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlimeGenetics
{
    public interface IGeneticsProvider
    {
        SlimeGenome Genome { get; }
    }
}
