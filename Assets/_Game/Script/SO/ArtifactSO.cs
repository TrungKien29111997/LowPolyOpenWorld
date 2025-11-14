using System.Collections;
using System.Collections.Generic;
using Ex;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "ArtifactSO", menuName = "Config/Ref/ArtifactSO")]
    public class ArtifactSO : SerializedScriptableObject
    {
        [PreviewField(120)] public Sprite icon;
        public EArtifactType type;
        public GameObject prefab;

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
