using System.Collections;
using System.Collections.Generic;
using Ex;
using Sirenix.OdinInspector;
using UnityEngine;
namespace Config
{
    [CreateAssetMenu(fileName = "WeaponDataSO", menuName = "Config/Data/WeaponDataSO")]
    public class WeaponDataSO : SerializedScriptableObject
    {
        const string url = "https://docs.google.com/spreadsheets/d/e/2PACX-1vSzACtDdF_vLdersgFezPWRmNXz3vxJjSyLfEEJJbJWCJgwzAYZOhM5QpWeQlWuOlyJTGTWWU2st-DP/pub?gid=1585383468&single=true&output=csv";
        public Dictionary<EWeaponType, ConfigWeapon> dicWeapon;

#if UNITY_EDITOR
        [Button()]
        void LoadData()
        {
            dicWeapon = new();
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

        void ReadConfig(string id, string charName, string rarity, string[] arrStatType, string[] arrStatValue, int startIndex, int amountStat)
        {
            Dictionary<EStat, Vector2> dicStat = new();
            EWeaponType type = id.ToEnum<EWeaponType>();
            ERarity eRarity = rarity.ToEnum<ERarity>();
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
            ConfigWeapon config = new ConfigWeapon()
            {
                type = type,
                name = charName,
                rarity = eRarity,
                dicStat = dicStat
            };
            dicWeapon.Add(type, config);
        }
#endif
    }
    [System.Serializable]
    public class ConfigWeapon
    {
        public EWeaponType type;
        public string name;
        public ERarity rarity;
        public Dictionary<EStat, Vector2> dicStat;
    }
}