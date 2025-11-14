using System.Collections;
using System.Collections.Generic;
using Core;
using Core.UI;
using DG.Tweening;
using Ex;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class CanvasMenu : UICanvas
    {
        [SerializeField] RectTransform rectBG;
        Button[] arrAllButton;
        [SerializeField] Button butInventory, butClose, butGoHome;
        void Awake()
        {
            arrAllButton = GetComponentsInChildren<Button>();
        }
        void Start()
        {
            butInventory.SetButton(ButtonInventory);
            butClose.SetButton(Close);
            butGoHome.SetButton(() => GameManager.Instance.GoSceneHome());
        }
        public override void Open()
        {
            base.Open();
            arrAllButton.ForEach(x => x.interactable = false);
            rectBG.DOAnchorPos(new Vector2(0, 0), 1f).OnComplete(() =>
            {
                arrAllButton.ForEach(x => x.interactable = true);
            });
        }
        void ButtonInventory()
        {
            UIManager.Instance.OpenUI<CanvasInventory>();
        }
        public override void Close()
        {
            arrAllButton.ForEach(x => x.interactable = false);
            rectBG.DOAnchorPos(new Vector2(600, 0), 1f).OnComplete(() =>
            {
                base.Close();
            });
        }
    }
}