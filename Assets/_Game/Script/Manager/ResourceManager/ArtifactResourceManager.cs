using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Core.Data
{
    public interface IArtifactResourceManager : IController<IArtifactResourceManager>
    {
        public static IArtifactResourceManager Instance = new ArtifactResourceManager();
        public void AddResource(List<BaseGameResource> lstResource, EResourceFrom resourceFrom, bool updateSever = false, Action actionSuccess = null, Action actionError = null);
        public List<ArtifactResource> GetListOwnedArtifact();
    }
    public class ArtifactResourceManager : BaseLocalController<ArtifactResourceManager, ArtifactResourceData>, IArtifactResourceManager
    {
        public override string KeyData => "ArtifactJsonData";

        public override string KeyEvent => Constant.EVENT_CHANGE_ARTIFACT_RESOURCE;

        public void AddResource(List<BaseGameResource> lstResource, EResourceFrom resourceFrom, bool updateSever = false, Action actionSuccess = null, Action actionError = null)
        {
            cachedData.AddResource(lstResource);
            OnValueChange();
        }
        public List<ArtifactResource> GetListOwnedArtifact() => cachedData.GetListOwnedArtifact();
    }
    [System.Serializable]
    public class ArtifactResourceData : BaseControllerCacheData
    {
        List<ArtifactResource> _listOwnedArtifact;
        public List<string> listOwnedAritifact;
        public override void OnNewData()
        {
            listOwnedAritifact = new();
        }
        public override void FirstTimeInit()
        {
            _listOwnedArtifact = new();
            if (listOwnedAritifact.Count > 0)
            {
                listOwnedAritifact.ForEach(x =>
                {
                    ArtifactResource equipment = BaseGameResource.ParseExistResource($"{Constant.RESORUCE_CODE_ARTIFACT_RESOURCE}|{x}") as ArtifactResource;
                    _listOwnedArtifact.Add(equipment);
                });
            }
        }
        public void AddResource(List<BaseGameResource> lstResource)
        {
            lstResource.ForEach(resource =>
            {
                if (resource is ArtifactResource)
                {
                    ArtifactResource ait = (ArtifactResource)resource;
                    _listOwnedArtifact.Add(ait);
                    listOwnedAritifact.Add(ait.GetJsonData());
                }
            });
        }
        public List<ArtifactResource> GetListOwnedArtifact() => _listOwnedArtifact;
    }
}