using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "WeaponSO", menuName = "Config/WeaponSO")]
    public class WeaponSO : ScriptableObject
    {
        [PreviewField] public Sprite icon;
        public EWeaponType type;
        public GameObject prefab;
    }
}