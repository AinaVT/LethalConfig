using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LethalConfig.MonoBehaviours
{
    internal class DescriptionBox : MonoBehaviour
    {
        public TextMeshProUGUI textMesh;

        public void SetDescription(string description)
        {
            textMesh.text = description;
        }
    }
}
