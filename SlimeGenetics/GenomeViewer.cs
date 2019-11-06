using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using static SlimeGenetics.Main;

namespace SlimeGenetics
{
    public class GenomeViewer : MonoBehaviour
    {
        GameObject geneticUI;
        public void Update()
        {
            if (ModActions.ProbeGenetics.WasPressed&&!geneticUI)
            {
                if(Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward,out var hit))
                {
                    var genomeHolder = hit.transform?.gameObject.GetComponent<IGeneticsProvider>();
                    if (genomeHolder != null)
                    {
                        geneticUI = GeneticUI.GenerateGeneticsUI(hit.transform.gameObject);
                    }
                }
            }
        }
    }
}
