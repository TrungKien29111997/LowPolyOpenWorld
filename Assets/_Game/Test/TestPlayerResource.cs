using System.Collections;
using System.Collections.Generic;
using Core.Data;
using Sirenix.OdinInspector;
using UnityEngine;

public class TestPlayerResource : MonoBehaviour
{
#if UNITY_EDITOR
    [Button]
    void HackCoin()
    {
        CommonResource testCoint = new CommonResource()
        {
            type = ECommonResource.Coin,
            amount = 1000
        };
        List<BaseGameResource> lstResource = new List<BaseGameResource>() { testCoint };
        IPlayerResourceManager.Instance.AddResource(lstResource, EResourceFrom.Hack);
    }
    [Button]
    void HackEquipment(EWeaponType eWeapon)
    {
        EquipmentResource testEquipment = new EquipmentResource();
        testEquipment.CreateNew(eWeapon);
        List<BaseGameResource> lstResource = new List<BaseGameResource>() { testEquipment };
        IEquipmentResourceManager.Instance.AddResource(lstResource, EResourceFrom.Hack);
    }
    [Button]
    void HackArtifact(EArtifactType eArtifact)
    {
        ArtifactResource testArtifact = new ArtifactResource();
        testArtifact.CreateNew(eArtifact, ERarity.Mythic);
        List<BaseGameResource> lstResource = new List<BaseGameResource>() { testArtifact };
        IArtifactResourceManager.Instance.AddResource(lstResource, EResourceFrom.Hack);
    }
#endif
}
