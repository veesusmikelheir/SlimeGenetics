using InControl;
using SlimeGenetics.API;
using SRML;
using SRML.SR;
using SRML.SR.SaveSystem;
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
            
        }

        public override void PostLoad()
        {
            ModActions.ProbeGenetics.AddDefaultBinding(Key.R);
        }

        public override void PreLoad()
        {

            //SlimeTraitRegistry.Register(new ClimateTraitProcessor(SRModInfo.GetCurrentInfo().Id, "diet"));

            SRCallbacks.OnActorSpawn += SRCallbacks_OnActorSpawn;
            SRCallbacks.OnSaveGameLoaded += (s) => s.Player.AddComponent<GenomeViewer>();
            SaveRegistry.RegisterDataParticipant<GenomeHolder>();
            SaveRegistry.RegisterDataParticipant<SlimeGeneticsHandler>();

            BindingRegistry.RegisterActions(typeof(ModActions));
            TranslationPatcher.AddUITranslation("key.probegenetics", "Probe Genetic Data");

            GeneticUI.SetupGeneticUI();
        }

        private void SRCallbacks_OnActorSpawn(Identifiable.Id id, GameObject obj, MonomiPark.SlimeRancher.DataModel.ActorModel model)
        {
            if (Identifiable.IsSlime(id)&&!obj.GetComponent<SlimeGeneticsHandler>()) obj.AddComponent<SlimeGeneticsHandler>();
        }

        public static class ModActions
        {
            public static PlayerAction ProbeGenetics;
        }
    }
}
