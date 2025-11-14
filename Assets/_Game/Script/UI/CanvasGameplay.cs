using System.Collections;
using System.Collections.Generic;
using Core.UI;
using Ex;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CanvasGameplay : UICanvas
    {
        [SerializeField] Button butGoMenu;
        void Start()
        {
            butGoMenu.SetButton(() => UIManager.Instance.OpenUI<CanvasMenu>());
        }
    }
}