using System.Collections;
using System.Collections.Generic;
using Ex;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Config
{
    [CreateAssetMenu(fileName = "CharacterDataSO", menuName = "Config/Data/CharacterDataSO")]
    public class CharacterDataSO : SerializedScriptableObject
    {
        const string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vSzACtDdF_vLdersgFezPWRmNXz3vxJjSyLfEEJJbJWCJgwzAYZOhM5QpWeQlWuOlyJTGTWWU2st-DP/pub?gid=311581878&single=true&output=csv";
        public Dictionary<ECharacterType, ConfigCharacter> dicCharacter;

#if UNITY_EDITOR
        [Button()]
        void LoadData()
        {
            dicCharacter = new();
            System.Action<string> readCharacterConfigAction = new((string str) =>
            {
                var data = CSVReader.ReadCSV(str);
                for (int j = 2; j < data.Count; j++)
                {
                    var _data = data[j];
                    if (!string.IsNullOrEmpty(_data[0]))
                    {
                        ReadConfig(_data[0], _data[1], _data[2], data[1], _data, 3, 4);
                    }
                }

                UnityEditor.EditorUtility.SetDirty(this);
            });
            EditorCoroutine.start(Extension.IELoadData(url, readCharacterConfigAction));
        }

        void ReadConfig(string id, string charName, string maxLevel, string[] arrStatType, string[] arrStatValue, int startIndex, int amountStat)
        {
            Dictionary<EStat, Vector2> dicStat = new();
            ECharacterType type = id.ToEnum<ECharacterType>();
            int maxLv = maxLevel.StringToInt();
            for (int i = startIndex; i < startIndex + amountStat; i++)
            {
                if (!string.IsNullOrEmpty(arrStatValue[i]))
                {
                    EStat stat = arrStatType[i].ToEnum<EStat>();
                    string[] stringValue = arrStatValue[i].Split('/');
                    Vector2 minMaxStat = new Vector2(Extension.ParseFloat(stringValue[0]), Extension.ParseFloat(stringValue[1]));
                    dicStat.Add(stat, minMaxStat);
                }
            }
            ConfigCharacter config = new ConfigCharacter()
            {
                type = type,
                name = charName,
                maxLevel = maxLv,
                dicStat = dicStat
            };
            dicCharacter.Add(type, config);
        }
#endif
    }
    [System.Serializable]
    public class ConfigCharacter
    {
        public ECharacterType type;
        public string name;
        public int maxLevel;
        public Dictionary<EStat, Vector2> dicStat;
    }
}