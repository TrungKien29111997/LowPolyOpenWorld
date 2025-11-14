using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

namespace Ex
{
    public static class Extension
    {
#if UNITY_EDITOR
        public static IEnumerator IELoadData(string urlData, System.Action<string> actionComplete, bool showAlert = false)
        {
            var www = new WWW(urlData);
            float time = 0;
            //TextAsset fileCsvLevel = null;
            while (!www.isDone)
            {
                time += 0.001f;
                if (time > 10000)
                {
                    yield return null;
                    Debug.Log("Downloading...");
                    time = 0;
                }
            }
            if (!string.IsNullOrEmpty(www.error))
            {
                UnityEditor.EditorUtility.DisplayDialog("Notice", "Load CSV Fail", "OK");
                yield break;
            }
            yield return null;
            actionComplete?.Invoke(www.text);
            yield return null;
            UnityEditor.AssetDatabase.SaveAssets();
            if (showAlert)
                UnityEditor.EditorUtility.DisplayDialog("Notice", "Load Data Success", "OK");
            else
                Debug.Log("<color=yellow>Download Data Complete</color>");
        }
#endif
        public static int ParseInt(string value)
        {
            int val = 0;
            if (int.TryParse(value, out val))
                return val;
            Debug.LogError("Wrong Input " + value);
            return val;
        }

        public static bool ParseBool(string data)
        {
            bool val = false;
            if (bool.TryParse(data, out val))
                return val;
            Debug.LogError("Wrong Input" + data);
            return val;
        }
        public static float ParseFloat(string value)
        {
            float result;
            bool isCheck = float.TryParse(value, out result);
            if (!isCheck)
            {
                Debug.LogWarning(value + " not correct");
            }
            return result;
        }
        #region Enum
        static Dictionary<Type, Dictionary<Enum, string>> dicEnumToString = new();
        static Dictionary<Type, Dictionary<string, object>> dicStringToEnum = new();
        static Dictionary<string, int> dicStringToInt = new();
        public static int StringToInt(this string str)
        {
            if (!dicStringToInt.ContainsKey(str))
            {
                dicStringToInt.Add(str, int.Parse(str));
            }
            return dicStringToInt[str];
        }

        public static string ExToString(this Enum enumValue)
        {
            Type enumType = enumValue.GetType();
            if (!dicEnumToString.TryGetValue(enumType, out var enumDict))
            {
                enumDict = new Dictionary<Enum, string>();
                foreach (Enum val in Enum.GetValues(enumType))
                {
                    enumDict[val] = val.ToString();
                }
                dicEnumToString[enumType] = enumDict;
            }
            return dicEnumToString[enumType][enumValue];
        }

        public static T ToEnum<T>(this string value) where T : struct, Enum
        {
            Type enumType = typeof(T);

            if (!dicStringToEnum.TryGetValue(enumType, out var enumMap))
            {
                enumMap = new Dictionary<string, object>();
                dicStringToEnum[enumType] = enumMap;
            }

            if (!enumMap.TryGetValue(value, out var enumValue))
            {
                enumValue = Enum.Parse(enumType, value, true);
                enumMap[value] = enumValue;
            }

            return (T)enumValue;
        }
        public static bool TryToEnum<T>(this string value, out T _type) where T : struct
        {
            if (Enum.TryParse<T>(value, out _type))
            {
                return true;
            }
            Debug.LogWarning($"Fail to parse {value}");
            return false;
        }

        public static List<T> GetListEnum<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
        #endregion

        public static void ChangeAnim(this Animator tmpAnim, ref string tmpCurrent, string animID)
        {
            tmpAnim.ResetTrigger(tmpCurrent);
            tmpCurrent = animID;
            tmpAnim.SetTrigger(animID);
        }
        public static Vector3 ProjectOntoPlane(Vector3 a, Vector3 n)
        {
            return (a - Vector3.Project(a, n)).normalized;
        }
        public static IEnumerator LoadListByLabel<T>(
            string label,
            Action<List<T>> onCompleted,
            Action<T> onEachLoaded = null)
        {
            var handle = Addressables.LoadAssetsAsync<T>(label, onEachLoaded);

            // Đợi cho đến khi load xong
            yield return handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var list = new List<T>(handle.Result);
                onCompleted?.Invoke(list);
            }
            else
            {
                Debug.LogError($"❌ LoadListByLabel<{typeof(T).Name}> Fail (label: {label})");
                onCompleted?.Invoke(new List<T>());
            }
        }
        public static string GetUID()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray())
                    .Replace("/", "_")   // tránh ký tự gây lỗi JSON/URL
                    .Replace("+", "-")   // tránh ký tự đặc biệt
                    .Replace("|", "_")   // tránh ký tự đặc biệt
                    .Substring(0, 22);
        }
        public static void SetButton(this Button button, Action onClick)
        {
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onClick?.Invoke());
        }
    }
}