using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Ex;
using Core;

namespace Config
{
    [CreateAssetMenu(fileName = "WeaponSO", menuName = "Config/Ref/WeaponSO")]
    public class WeaponSO : SerializedScriptableObject
    {
        [PreviewField(120)] public Sprite icon;
        public EWeaponType type;
        public GameObject prefab;
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