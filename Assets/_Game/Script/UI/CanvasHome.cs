using System.Collections;
using System.Collections.Generic;
using Core;
using Core.Data;
using Core.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Ex;

namespace UI
{
    public class CanvasHome : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI txtCoin;
        [SerializeField] Button butGoSceneGameplay;
        void Start()
        {
            EventManager.StartListening(Constant.EVENT_CHANGE_PLAYER_RESOURCE, ReloadCoin);
            ReloadCoin();
            butGoSceneGameplay.SetButton(ButtonGoSceneGameplay);
        }
        void ButtonGoSceneGameplay()
        {
            GameManager.Instance.GoSceneGameplay();
        }
        void OnDestroy()
        {
            Ex.EventManager.StopListening(Constant.EVENT_CHANGE_PLAYER_RESOURCE, ReloadCoin);
        }
        void ReloadCoin()
        {
            txtCoin.text = $"Coin: {IPlayerResourceManager.Instance.GetCommonResource(ECommonResource.Coin)}";
        }
    }
}