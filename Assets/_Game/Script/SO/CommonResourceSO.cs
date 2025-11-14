using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Ex;
using Core;
namespace Config
{
    [CreateAssetMenu(fileName = "CommonResourceSO", menuName = "Config/Ref/CommonResourceSO")]
    public class CommonResourceSO : SerializedScriptableObject
    {
        [PreviewField(120)] public Sprite icon;
        public ECommonResource type;
        public ERarity rarity;
        public bool IsVisualInInventory;
        public string GetName()
        {
            return type.ExToString();
        }
        public string GetDesc()
        {
            return string.Empty;
        }
        public Sprite GetBGImg()
        {
            return DataSystem.Instance.dataGeneral.dicRarityBG[rarity];
        }
    }
}