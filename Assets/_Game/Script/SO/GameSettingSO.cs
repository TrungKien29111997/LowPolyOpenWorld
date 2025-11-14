using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Config
{
    [CreateAssetMenu(fileName = "GameSettingSO", menuName = "Config/GameSettingSO")]
    public class GameSettingSO : SerializedScriptableObject
    {
        public EDirectScreen directScreen;
    }
}