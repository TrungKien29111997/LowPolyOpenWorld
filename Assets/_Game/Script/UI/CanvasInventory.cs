using System.Collections;
using System.Collections.Generic;
using Core;
using Core.Data;
using Core.UI;
using Ex;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
namespace UI
{
    public class CanvasInventory : UICanvas
    {
        [SerializeField] Button butCommonResource, butWeapon, butArtifact, butClose;
        List<ResourceButton> listCacheButton;
        [SerializeField] ResourceButton prefabButton;
        [SerializeField] RectTransform rectMainBoard;
        void Start()
        {
            listCacheButton = new();
            butWeapon.SetButton(ButtonWeapon);
            butArtifact.SetButton(ButtonArtifact);
            butCommonResource.SetButton(ButtonCommonResource);
            butClose.SetButton(Close);
            butCommonResource.onClick.Invoke();
        }
        void ButtonCommonResource()
        {
            ReleaseCacheButton();
            List<CommonResource> listCommonResource = IPlayerResourceManager.Instance.GetListCommonResource();
            for (int i = 0; i < listCommonResource.Count; i++)
            {
                SetResourceButton(listCommonResource[i]);
            }
        }
        void ButtonWeapon()
        {
            ReleaseCacheButton();
            List<EquipmentResource> listEquipemt = IEquipmentResourceManager.Instance.GetListOwnedWeapon();
            for (int i = 0; i < listEquipemt.Count; i++)
            {
                SetResourceButton(listEquipemt[i]);
            }
        }
        void ButtonArtifact()
        {
            ReleaseCacheButton();
            List<ArtifactResource> listArtifact = IArtifactResourceManager.Instance.GetListOwnedArtifact();
            for (int i = 0; i < listArtifact.Count; i++)
            {
                SetResourceButton(listArtifact[i]);
            }
        }
        void SetResourceButton(BaseGameResource resource)
        {
            ResourceButton button = PoolingSystem.Spawn(prefabButton, default, default, rectMainBoard);
            button.SetUp(resource);
            listCacheButton.Add(button);
        }
        void ReleaseCacheButton()
        {
            listCacheButton.ForEach(x => PoolingSystem.Despawn(x));
            listCacheButton.Clear();
        }
    }
}