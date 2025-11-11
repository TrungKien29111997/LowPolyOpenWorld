using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Config
{
    [CreateAssetMenu(fileName = "GeneralSO", menuName = "Config/Data/GeneralSO")]
    public class GeneralSO : SerializedScriptableObject
    {
        [PreviewField(120)] public Dictionary<ECommonResource, Sprite> dicComonResourceIcon;
    }
}