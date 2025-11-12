using System.Collections;
using System.Collections.Generic;
using Core.Data;
using Core.UI;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CanvasHome : UICanvas
    {
        [SerializeField] TextMeshProUGUI txtCoin;
        public override void SetUp()
        {
            base.SetUp();
            Ex.EventManager.StartListening(Constant.EVENT_CHANGE_PLAYER_RESOURCE, ReloadCoin);
            ReloadCoin();
        }
        public override void Close()
        {
            base.Close();
            Ex.EventManager.StopListening(Constant.EVENT_CHANGE_PLAYER_RESOURCE, ReloadCoin);
        }
        void ReloadCoin()
        {
            txtCoin.text = $"Coin: {IPlayerResource.Instance.GetCommonResource(ECommonResource.Coin)}";
        }
    }
}