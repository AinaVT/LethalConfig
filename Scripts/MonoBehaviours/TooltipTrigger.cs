using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LethalConfig.MonoBehaviours
{
    internal class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string tooltipText;

        public void OnPointerEnter(PointerEventData eventData)
        {
            TooltipSystem.Show(tooltipText);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipSystem.Hide();
        }
    }
}
