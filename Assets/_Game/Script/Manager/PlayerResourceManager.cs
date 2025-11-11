using System.Collections;
using System.Collections.Generic;
using Ex;
using UnityEngine;
namespace Core.Data
{
    public class PlayerResourceManager : MonoBehaviour
    {

    }
    [System.Serializable]
    public class PlayerResourceData : ControllerCachedData
    {
        Dictionary<ECommonResource, int> _commonResources;
        public Dictionary<string, int> dicItemResource = new();
        //List<ItemResource> lstVisuals = new();
        public override void OnNewData()
        {
            base.OnNewData();
            dicItemResource = new();
            List<ECommonResource> lstCommon = Extension.GetListEnum<ECommonResource>();
            foreach (var item in lstCommon)
            {
                string key = "cm" + item.ExToString();
                if (!dicItemResource.ContainsKey(key))
                    dicItemResource.Add(key, 0);
            }
        }
        public override void FirstTimeInit()
        {
            _commonResources = new();
            foreach (var item in dicItemResource)
            {
                _commonResources.Add(item.Key.ToEnum<ECommonResource>(), item.Value);
            }
        }
        // bool CheckResource(GameResource resource)
        // {
        //     if (resource is CommonResource)
        //     {
        //         CommonResource cr = (CommonResource)resource;
        //         if (_commonResources[cr.resourceType] + cr.resourceValue < 0)
        //         {
        //             return false;
        //         }
        //     }
        //     else if (resource is HeroShardResource)
        //     {
        //         HeroShardResource hr = (HeroShardResource)resource;
        //         string keyId = $"{GetKeyId<HeroShardResource>()}{hr.typeId}";
        //         if (!dicItemResource.ContainsKey(keyId)) dicItemResource.Add(keyId, 0);
        //         if (dicItemResource[keyId] + hr.resourceValue < 0)
        //         {
        //             return false;
        //         }
        //     }
        //     return true;
        // }
        // bool CheckListResource(List<GameResource> lstResource)
        // {
        //     foreach (var resource in lstResource)
        //     {
        //         if (!CheckResource(resource))
        //             return false;
        //     }
        //     return true;
        // }
        // public bool AddResource(BaseGameResource resource)
        // {
        //     bool isCheck = CheckResource(resource);
        //     if (isCheck)
        //         if (resource is ItemResource)
        //         {
        //             if (resource is CommonResource)
        //             {
        //                 CommonResource cr = (CommonResource)resource;
        //                 if (cr.type == ECommonResource.Energy)
        //                 {
        //                     int lastEnergy = GetCommonResource(ECommonResource.Energy);
        //                     int nextEnergy = lastEnergy + cr.resourceValue;
        //                     if (lastEnergy >= DataSystem.Instance.dataGeneral.maxEnegy && nextEnergy < DataSystem.Instance.dataGeneral.maxEnegy)
        //                     {
        //                         ITimerController.Instance.SetNextEnergyRegen(DateTime.UtcNow.AddSeconds(DataSystem.Instance.dataGeneral.enegyRegenTime));
        //                     }
        //                 }
        //                 _commonResources[cr.type] += cr.resourceValue;
        //                 dicItemResource[$"{GetKeyId<CommonResource>()}{cr.type}"] = _commonResources[cr.type];
        //             }
        //             else if (resource is HeroShardResource)
        //             {
        //                 HeroShardResource hr = (HeroShardResource)resource;
        //                 dicItemResource[$"{GetKeyId<HeroShardResource>()}{hr.typeId.ToString()}"] += hr.resourceValue;
        //             }
        //             ItemResource itemResource = (ItemResource)resource;
        //             foreach (var item in lstVisuals)
        //             {
        //                 if (item.CompareType(resource))
        //                 {
        //                     item.resourceValue += itemResource.resourceValue;
        //                     break;
        //                 }
        //             }
        //         }
        //     return isCheck;
        // }
        // public bool AddResource(List<BaseGameResource> lstResource)
        // {
        //     bool isCheck = CheckListResource(lstResource);
        //     if (isCheck)
        //         foreach (BaseGameResource resource in lstResource)
        //         {
        //             AddResource(resource);
        //         }
        //     return isCheck;
        // }
        // CustomBigValue GetResource(string key)
        // {
        //     if (!dicItemResource.ContainsKey(key))
        //         return 0;
        //     return dicItemResource[key];
        // }

        // public CustomBigValue GetCommonResource(ECommonResource resouceType)
        // {
        //     return GetResource($"{GetKeyId<CommonResource>()}{resouceType.ToString()}");
        // }

        // public CustomBigValue GetFragmentResource(EEquipmentType resouceType)
        // {
        //     return GetResource($"{GetKeyId<FragmentResource>()}{resouceType.ToString()}");
        // }
        // public CustomBigValue GetHeroShardResource(EHero typeId)
        // {
        //     return GetResource($"{GetKeyId<HeroShardResource>()}{typeId}");
        // }
        // string GetKeyId<T>() where T : ItemResource
        // {
        //     if (typeof(T) == typeof(CommonResource))
        //     {
        //         return "cm";
        //     }
        //     else if (typeof(T) == typeof(FragmentResource))
        //     {
        //         return "fr";
        //     }
        //     else if (typeof(T) == typeof(HeroShardResource))
        //     {
        //         return "hr";
        //     }
        //     return string.Empty;
        // }
    }
}