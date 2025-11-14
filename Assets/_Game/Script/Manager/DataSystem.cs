using System.Collections;
using System.Collections.Generic;
using Config;
using UnityEngine;
using Ex;
using Sirenix.OdinInspector;
namespace Core
{
    public class DataSystem : Singleton<DataSystem>
    {
        [ShowInInspector] public Dictionary<ECommonResource, CommonResourceSO> dicConfigRefCommonResource;
        [ShowInInspector] public Dictionary<EWeaponType, WeaponSO> dicConfigRefWeapon;
        [ShowInInspector] public Dictionary<EArtifactType, ArtifactSO> dicConfigRefArtifact;
        public WeaponDataSO dataWeapon;
        public ArtifactDataSO dataArtifact;
        public GeneralSO dataGeneral;
        public GameSettingSO dataGameSetting;
        int coroutineRemain;
        public IEnumerator IEInit()
        {
            List<IEnumerator> listLoadCoroutine = new();

            listLoadCoroutine.Add(IELoadCommonResourceConfigRefSO(() => coroutineRemain--));
            listLoadCoroutine.Add(IELoadWeaponResourceConfigRefSO(() => coroutineRemain--));
            listLoadCoroutine.Add(IELoadArtifactResourceConfigRefSO(() => coroutineRemain--));

            coroutineRemain = listLoadCoroutine.Count;
            listLoadCoroutine.ForEach(x => StartCoroutine(x));
            yield return new WaitUntil(() => coroutineRemain == 0);
        }
        IEnumerator IELoadCommonResourceConfigRefSO(System.Action onComplete = null)
        {
            return Extension.LoadListByLabel<CommonResourceSO>(
                Constant.ADDRESSABLES_LABEL_COMMONRESOURCE_SO,
                x =>
                {
                    dicConfigRefCommonResource = new();
                    foreach (var item in x)
                    {
                        dicConfigRefCommonResource.Add(item.type, item);
                    }
                    onComplete?.Invoke();
                }
            );
        }
        IEnumerator IELoadWeaponResourceConfigRefSO(System.Action onComplete = null)
        {
            return Extension.LoadListByLabel<WeaponSO>(
                Constant.ADDRESSABLES_LABEL_WEAPON_SO,
                x =>
                {
                    dicConfigRefWeapon = new();
                    foreach (var item in x)
                    {
                        dicConfigRefWeapon.Add(item.type, item);
                    }
                    onComplete?.Invoke();
                }
            );
        }
        IEnumerator IELoadArtifactResourceConfigRefSO(System.Action onComplete = null)
        {
            return Extension.LoadListByLabel<ArtifactSO>(
                Constant.ADDRESSABLES_LABEL_ARTIFACT_SO,
                x =>
                {
                    dicConfigRefArtifact = new();
                    foreach (var item in x)
                    {
                        dicConfigRefArtifact.Add(item.type, item);
                    }
                    onComplete?.Invoke();
                }
            );
        }
    }
}