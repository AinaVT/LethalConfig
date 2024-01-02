using LethalConfig.ConfigItems;
using LethalConfig.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LethalConfig.MonoBehaviours.Components
{
    internal abstract class ModConfigController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        protected BaseConfigItem baseConfigItem;

        internal ConfigMenuAudioManager audioManager;

        public delegate void OnHoverHandler();
        public event OnHoverHandler OnHoverEnter;
        public event OnHoverHandler OnHoverExit;

        public virtual bool SetConfigItem(BaseConfigItem configItem)
        {
            this.baseConfigItem = configItem;
            this.OnSetConfigItem();
            return true;
        }

        protected abstract void OnSetConfigItem();
        public abstract void UpdateAppearance();

        public virtual void ResetToDefault()
        {
            audioManager.PlayConfirmSFX();
            baseConfigItem.ChangeToDefault();
            UpdateAppearance();
        }

        public virtual string GetDescription()
        {
            if (baseConfigItem.RequiresRestart) return $"{baseConfigItem.Name}\n*REQUIRES RESTART*\n\n{baseConfigItem.Description}";
            return $"{baseConfigItem.Name}\n\n{baseConfigItem.Description}";
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHoverEnter();
            audioManager.PlayHoverSFX();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnHoverExit();
        }
    }

    internal abstract class ModConfigController<T, V> : ModConfigController where T : BaseValueConfigItem<V>
    {
        public T ConfigItem => (T)baseConfigItem;

        public override string GetDescription()
        {
            return $"{base.GetDescription()}\n\nDefault: {ConfigItem.Defaultvalue}";
        }

        public override bool SetConfigItem(BaseConfigItem configItem)
        {
            if (configItem is not T)
            {
                LogUtils.LogError($"Expected config item of type {typeof(T).Name}, but got {configItem.GetType().Name} instead.");
                return false;
            }

            return base.SetConfigItem(configItem);
        }
    } 
}
