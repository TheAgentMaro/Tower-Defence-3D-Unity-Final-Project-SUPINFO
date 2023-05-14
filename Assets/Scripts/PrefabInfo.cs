using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class PrefabInfo : MonoBehaviour
    {
        public string Category;
        public string PrefabName = string.Empty;
        public void Awake()
        {

            if (PrefabName == string.Empty || PrefabName == "")
            {
                string objName = this.name;

                PrefabName = objName.Replace("(Clone)", "");
            }

        }
    }
}
