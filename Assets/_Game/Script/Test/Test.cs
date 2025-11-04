using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Config;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Test
{
    public class Test : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] List<WeaponSO> listWeaponSO;
        [Button]
        void Load()
        {
            // Load tất cả asset có label "Enemy" và kiểu GameObject
            Addressables.LoadAssetsAsync<WeaponSO>("WeaponSO", OnEnemyLoaded).Completed += OnAllEnemiesLoaded;
        }

        void OnEnemyLoaded(WeaponSO enemy)
        {
            //Debug.Log("Loaded enemy: " + enemy.name);
        }

        void OnAllEnemiesLoaded(AsyncOperationHandle<IList<WeaponSO>> handle)
        {
            listWeaponSO = handle.Result.ToList();
        }
        #endif
    }
}