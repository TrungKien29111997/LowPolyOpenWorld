using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Ex;

namespace Config
{
    [CreateAssetMenu(fileName = "WeaponSO", menuName = "Config/Ref/WeaponSO")]
    public class WeaponSO : SerializedScriptableObject
    {
        [PreviewField] public Sprite icon;
        public EWeaponType type;
        public ERarity rarity;
        public GameObject prefab;
        public bool IsVisualInInventory;

        public Color GetBGColor()
        {
            return Color.white;
        }
        public string GetName()
        {
            return type.ExToString();
        }
        public string GetDesc()
        {
            return string.Empty;
        }
        public string GetTextValue()
        {
            return string.Empty;
        }
    }
}