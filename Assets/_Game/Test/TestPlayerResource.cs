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
        IPlayerResource.Instance.AddResource(lstResource, EResourceFrom.Hack);
    }
    #endif
}
