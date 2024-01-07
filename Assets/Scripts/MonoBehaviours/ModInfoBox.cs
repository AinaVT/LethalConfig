using LethalConfig.Mods;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LethalConfig.MonoBehaviours
{
    internal class ModInfoBox : MonoBehaviour
    {
        public Image modIconImage;
        public TextMeshProUGUI modInfoText;

        public void SetModInfo(Mod mod)
        {
            modIconImage.sprite = mod.modInfo.Icon;
            var text = $"{mod.modInfo}";
            modInfoText.text = $"{mod.modInfo}";
        }
    }
}
