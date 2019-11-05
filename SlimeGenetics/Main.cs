using SRML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SlimeGenetics
{
    public class Main : ModEntryPoint
    {
        public override void Load()
        {
            base.Load();
        }

        public override void PostLoad()
        {
            base.PostLoad();
        }

        public override void PreLoad()
        {
            IntermodCommunication.RegisterIntermodMethod("print_out_epic_stuff", (string x) => Debug.Log($"Printing out {x} for {IntermodCommunication.CallingMod}"));
        }
    }
}
