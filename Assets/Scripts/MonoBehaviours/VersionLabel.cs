using TMPro;
using UnityEngine;

namespace LethalConfig.MonoBehaviours
{
    internal class VersionLabel : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            GetComponent<TextMeshProUGUI>().text = $"{PluginInfo.Name} v{PluginInfo.Version}";
        }
    }
}