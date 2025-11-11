using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ex;
namespace Core.Data
{
    public abstract class BaseGameResource
    {
        public static BaseGameResource GetResource(string input)
        {
            string[] resourceData = input.Split("|");
            // switch (resourceData[0])
            // {
            //     case "CMR":
            //         if (resourceData[1].TryToEnum(out ECommonResource resourceCommon))
            //         {
            //             int resouceValue = resourceData[2].StringToInt();
            //             return new CommonResource(resourceCommon, resouceValue);
            //         }
            //         break;
            //     case "EQM":
            //         if (resourceData[1].TryToEnum(out ERarity equipmentRarity))
            //         {
            //             if (resourceData.Length <= 4)
            //             {
            //                 return new EquipmentResource(equipmentRarity, resourceData[2], DataSystem.Instance.dataEquipment.dicEquipmentConfig[resourceData[2]].type);
            //             }
            //             else
            //             {
            //                 return new EquipmentResource(equipmentRarity, resourceData[2], DataSystem.Instance.dataEquipment.dicEquipmentConfig[resourceData[2]].type, resourceData[4]);
            //             }
            //         }
            //         break;
            //     case "AIT":
            //         if (Helper.TryToEnum(resourceData[1], out EArtifact eArtifact))
            //         {
            //             if (DataSystem.Instance.dataArtifact.dicArtifactConfig.ContainsKey(eArtifact))
            //             {
            //                 return new ArtifactResource(eArtifact);
            //             }
            //         }
            //         break;
            // }

            Debug.LogError($"Null Parse Resource, {input}");
            return null;
        }
        public abstract bool IsVisual { get; }
        public abstract Sprite GetIcon();
        public abstract Color GetBGColor();
        public abstract string GetName();
        public abstract string GetDesc();
        public abstract string GetTextValue();
        //public abstract List<GameResource> CorrectValue();
        //public abstract bool CompareType(GameResource source);
        //public abstract ResourceButton SetButton(Transform parent);
    }
    public abstract class ItemResource : BaseGameResource
    {
        public int amount;
        //public abstract bool CanUseResource();
        public abstract bool IsVisualInInventory { get; }

        public override string GetTextValue()
        {
            return amount.ToString();
        }
    }
    public class CommonResource : ItemResource
    {
        public ECommonResource type;
        public override Sprite GetIcon()
        {
            return DataSystem.Instance.generalSO.dicComonResourceIcon[type];
        }
        public override string GetName()
        {
            return type.ExToString(); //Helper.GetName(type.ToString());
        }
        public override string GetDesc()
        {
            return string.Empty; // Helper.GetDesc(type.ToString());
        }
        // public override List<GameResource> CorrectValue()
        // {
        //     return new List<GameResource> { this };
        // }
        // public override bool CompareType(GameResource source)
        // {
        //     if (source is CommonResource)
        //     {
        //         CommonResource common = source as CommonResource;
        //         return common.type == type;
        //     }

        //     return false;
        // }
        public override Color GetBGColor()
        {
            return default; //DataSystem.Instance.dataGeneral.GetRarityColor(GetRarity());
        }
        // public ERarity GetRarity()
        // {
        //     switch (type)
        //     {
        //         case ECommonResource.Coin:
        //             return ERarity.Epic;
        //         case ECommonResource.Gem:
        //             return ERarity.Legendary;
        //         case ECommonResource.Energy:
        //             return ERarity.Rare;
        //     }
        //     return ERarity.Common;
        // }
        public override bool IsVisual => true;
        // public CommonResource(ECommonResource resourceType, CustomBigValue value)
        // {
        //     this.type = resourceType;
        //     resourceValue = value;
        // }

        public override bool IsVisualInInventory => true;

        // public override ResourceButton SetButton(Transform parent)
        // {
        //     ResourceButton tmpButton = PoolingSystem.Spawn<ResourceButton>(EPooling.ResourceButton, parent);
        //     tmpButton.SetUp(this, resourceValue.ToVisualString());
        //     return tmpButton;
        // }
    }
}