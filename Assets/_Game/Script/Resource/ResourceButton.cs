using System.Collections;
using System.Collections.Generic;
using Core;
using Core.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class ResourceButton : PoolingElement
    {
        [SerializeField] Image imgIcon, imgBG;
        [SerializeField] TextMeshProUGUI txtName, txtVisualText;
        public void SetUp(BaseGameResource baseGameResource)
        {
            imgIcon.sprite = baseGameResource.GetIcon();
            imgIcon.preserveAspect = true;
            imgBG.sprite = baseGameResource.GetBGImg();
            txtName.text = baseGameResource.GetName();
            txtVisualText.text = baseGameResource.GetTextValue();
        }
    }
}