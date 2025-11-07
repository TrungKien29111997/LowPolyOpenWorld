using System.Collections;
using System.Collections.Generic;
using Ex;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UIElements;
namespace Config
{
    [CreateAssetMenu(fileName = "CharacterDataSO", menuName = "Config/Data/CharacterDataSO")]
    public class CharacterDataSO : SerializedScriptableObject
    {
        public string url;
        public ECharacterType type;
        public int maxLevel;
        public Dictionary<EStat, Vector2> dicMainStat;
        public Dictionary<ESkillType, List<ConfigCharacterSkill>> dicSkill;

#if UNITY_EDITOR
        [Button()]
        void LoadData()
        {
            dicMainStat = new();
            dicSkill = new();
            System.Action<string> readCharacterConfigAction = new((string str) =>
            {
                var data = CSVReader.ReadCSV(str);
                ECharacterType type = data[2][0].ToEnum<ECharacterType>();
                for (int j = 3; j < data.Count; j++)
                {
                    var _data = data[j];
                    if (!string.IsNullOrEmpty(_data[0]))
                    {
                        if (_data[0] == "#MainStat")
                        {
                            if (_data[1].TryToEnum(out EStat eStat))
                            {
                                string jsonValue = _data[3];
                                string[] splitValue = jsonValue.Split('/');
                                Vector2 minMaxValue = new();
                                minMaxValue.x = Extension.ParseFloat(splitValue[0]);
                                minMaxValue.y = splitValue.Length > 1 ? Extension.ParseFloat(splitValue[1]) : 0f;
                                dicMainStat.Add(eStat, minMaxValue);
                            }
                            else if (_data[1] == "MaxLevel")
                            {
                                maxLevel = _data[3].StringToInt();
                            }
                        }
                        else
                        {
                            ReadConfigSkill(_data[0], _data[1], _data[2], _data, 3);
                        }
                    }
                }

                UnityEditor.EditorUtility.SetDirty(this);
            });
            EditorCoroutine.start(Extension.IELoadData(url, readCharacterConfigAction));
        }

        void ReadConfigSkill(string id, string skillType, string desc, string[] line, int startIndex)
        {
            List<string> info = new();
            for (int i = startIndex; i < line.Length; i++)
            {
                if (!string.IsNullOrEmpty(line[i]))
                {
                    info.Add(line[i]);
                }
            }
            ESkillType eSkillType = skillType.ToEnum<ESkillType>();
            ConfigCharacterSkill config = new ConfigCharacterSkill()
            {
                id = id,
                skillType = eSkillType,
                desc = desc,
                info = info
            };
            if (!dicSkill.ContainsKey(eSkillType))
            {
                dicSkill.Add(eSkillType, new List<ConfigCharacterSkill>());
            }
            dicSkill[eSkillType].Add(config);
        }
#endif
    }
    [System.Serializable]
    public class ConfigCharacterSkill
    {
        public string id;
        public ESkillType skillType;
        public string desc;
        public List<string> info;
    }
}