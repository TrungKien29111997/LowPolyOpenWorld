using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Config
{
    [CreateAssetMenu(fileName = "GeneralSO", menuName = "Config/GeneralSO")]
    public class GeneralSO : SerializedScriptableObject
    {
        public Dictionary<ERarity, Sprite> dicRarityBG;
    }
}