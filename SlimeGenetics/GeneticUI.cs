using SlimeGenetics.API;
using SRML.SR.UI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SlimeGenetics
{
    public class GeneticUI : BaseUI
    {
        public static GameObject mainPanelPrefab;
        public static GameObject textPrefab;

        public static void SetupGeneticUI()
        {
            var bundle = AssetBundle.LoadFromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(Main),"slimegenetics"));
            mainPanelPrefab = bundle.LoadAsset<GameObject>("Canvas");
            UIUtils.FixStyles(mainPanelPrefab);
            mainPanelPrefab.AddComponent<GeneticUI>();
            mainPanelPrefab.AddComponent<UIInputLocker>();
            textPrefab = bundle.LoadAsset<GameObject>("textholder");
        }

        public static GameObject GenerateGeneticsUI(GameObject obj)
        {
            var geneticsCanvas = GenerateGeneticPanel(obj.GetComponent<IGeneticsProvider>().Genome);
            geneticsCanvas.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = Identifiable.GetName(Identifiable.GetId(obj));
            return geneticsCanvas;
        }

        public static GameObject GenerateGeneticPanel(SlimeGenome genome)
        {
            var obj = GameObject.Instantiate(mainPanelPrefab);
            var contentPanel = obj.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0);
            foreach(var v in genome.AllTraits.Where(x=>x.ShouldDisplay).OrderBy(x=>x.DisplayPriority))
            {
                CreateTraitInstance(v).transform.SetParent(contentPanel);
            }
            return obj;
        }

        public static GameObject CreateTraitInstance(SlimeTrait trait)
        {
            var obj = GameObject.Instantiate(textPrefab);
            var text = obj.GetComponent<Text>();
            text.color = trait.DisplayColor;
            text.text = trait.DisplayName;
            return obj;
        }

        public void Start()
        {
            Debug.Log("GAGA WEE");
        }
    }
}
