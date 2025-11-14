using System.Collections;
using System.Collections.Generic;
using Ex;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "ArtifactDataSO", menuName = "Config/Data/ArtifactDataSO")]
    public class ArtifactDataSO : SerializedScriptableObject
    {
        const string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vSzACtDdF_vLdersgFezPWRmNXz3vxJjSyLfEEJJbJWCJgwzAYZOhM5QpWeQlWuOlyJTGTWWU2st-DP/pub?gid=723546717&single=true&output=csv";
        public Dictionary<EArtifactType, ConfigArtifact> dicArtifact;

#if UNITY_EDITOR
        [Button()]
        void LoadData()
        {
            dicArtifact = new();
            System.Action<string> readArtifactConfigAction = new((string str) =>
            {
                var data = CSVReader.ReadCSV(str);
                for (int j = 2; j < data.Count; j++)
                {
                    var _data = data[j];
                    if (!string.IsNullOrEmpty(_data[0]))
                    {
                        ReadConfig(_data[0], _data[1], data[1], _data, 2, 4);
                    }
                }

                UnityEditor.EditorUtility.SetDirty(this);
            });
            EditorCoroutine.start(Extension.IELoadData(url, readArtifactConfigAction));
        }

        void ReadConfig(string id, string artifactName, string[] arrStatType, string[] arrStatValue, int startIndex, int amountStat)
        {
            Dictionary<EStat, Vector2> dicStat = new();
            EArtifactType type = id.ToEnum<EArtifactType>();
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
            ConfigArtifact config = new ConfigArtifact()
            {
                type = type,
                name = artifactName,
                dicStat = dicStat
            };
            dicArtifact.Add(type, config);
        }
#endif
    }
    [System.Serializable]
    public class ConfigArtifact
    {
        public EArtifactType type;
        public string name;
        public Dictionary<EStat, Vector2> dicStat;
    }
}