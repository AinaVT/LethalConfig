using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LethalConfig.MonoBehaviours
{
    internal class SectionHeader : MonoBehaviour
    {
        public TextMeshProUGUI textMesh;

        public void SetSectionName(string sectionName)
        {
            textMesh.text = $"[{sectionName}]";
        }
    }
}
