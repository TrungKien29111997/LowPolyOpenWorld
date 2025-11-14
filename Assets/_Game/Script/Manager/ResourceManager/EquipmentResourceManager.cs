using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.Data
{
    public interface IEquipmentResourceManager : IController<IEquipmentResourceManager>
    {
        public static IEquipmentResourceManager Instance = new EquipmentResourceManager();
        public void AddResource(List<BaseGameResource> lstResource, EResourceFrom resourceFrom, bool updateSever = false, Action actionSuccess = null, Action actionError = null);
        public List<EquipmentResource> GetListOwnedWeapon();
    }

    public class EquipmentResourceManager : BaseLocalController<EquipmentResourceManager, EquipmentResourceData>, IEquipmentResourceManager
    {
        public override string KeyData => "EquipmentJsonData";

        public override string KeyEvent => Constant.EVENT_CHANGE_EQUIPMENT_RESOURCE;

        public void AddResource(List<BaseGameResource> lstResource, EResourceFrom resourceFrom, bool updateSever = false, Action actionSuccess = null, Action actionError = null)
        {
            cachedData.AddResource(lstResource);
            OnValueChange();
        }
        public List<EquipmentResource> GetListOwnedWeapon() => cachedData.GetListOwnedWeapon();
    }
    public class EquipmentResourceData : BaseControllerCacheData
    {
        List<EquipmentResource> _listOwnedWeapon;
        public List<string> listOwnedWeapon;

        public override void OnNewData()
        {
            listOwnedWeapon = new();
        }
        public override void FirstTimeInit()
        {
            _listOwnedWeapon = new();
            if (listOwnedWeapon.Count > 0)
            {
                listOwnedWeapon.ForEach(x =>
                {
                    EquipmentResource equipment = BaseGameResource.ParseExistResource($"{Constant.RESORUCE_CODE_EQUIPMENT_RESOURCE}|{x}") as EquipmentResource;
                    _listOwnedWeapon.Add(equipment);
                });
            }
        }
        public List<EquipmentResource> GetListOwnedWeapon() => _listOwnedWeapon;
        public void AddResource(List<BaseGameResource> lstResource)
        {
            lstResource.ForEach(resource =>
            {
                if (resource is EquipmentResource)
                {
                    EquipmentResource eq = (EquipmentResource)resource;
                    _listOwnedWeapon.Add(eq);
                    listOwnedWeapon.Add(eq.GetJsonData());
                }
            });
        }
    }
}