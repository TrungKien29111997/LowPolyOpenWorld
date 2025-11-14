using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ex;
using Config;
namespace Core.Data
{
    public abstract class BaseGameResource
    {
        public static BaseGameResource ParseExistResource(string input)
        {
            string[] resourceData = input.Split("|");
            switch (resourceData[0])
            {
                case Constant.RESORUCE_CODE_COMMON_RESOURCE:
                    if (resourceData[1].TryToEnum(out ECommonResource resourceCommon))
                    {
                        int resouceValue = resourceData[2].StringToInt();
                        CommonResource commonResource = new CommonResource()
                        {
                            type = resourceCommon,
                            amount = resouceValue
                        };
                        return commonResource;
                    }
                    return null;
                case Constant.RESORUCE_CODE_EQUIPMENT_RESOURCE:
                    if (resourceData[1].TryToEnum(out EWeaponType equipmentType))
                    {
                        EquipmentResource equipmentResource = new EquipmentResource()
                        {
                            typeId = equipmentType,
                            uid = resourceData[2],
                            level = resourceData[3].StringToInt()
                        };
                        return equipmentResource;
                    }
                    return null;
                case Constant.RESORUCE_CODE_ARTIFACT_RESOURCE:
                    if (resourceData[1].TryToEnum(out EArtifactType eArtifact))
                    {
                        ArtifactResource artifactResource = new ArtifactResource()
                        {
                            typeId = eArtifact,
                            uid = resourceData[2],
                            rarity = resourceData[3].ToEnum<ERarity>(),
                            level = resourceData[4].StringToInt()
                        };
                        return artifactResource;
                    }
                    return null;
            }

            Debug.LogError($"Null Parse Resource, {input}");
            return null;
        }
        public static BaseGameResource ParseNewResource(string input)
        {
            return null;
        }
        public abstract bool IsVisualInInventory { get; }
        public abstract Sprite GetIcon();
        public abstract Sprite GetBGImg();
        public abstract string GetName();
        public abstract string GetDesc();
        public abstract string GetTextValue();
        //public abstract List<GameResource> CorrectValue();
        //public abstract bool CompareType(GameResource source);
        //public abstract ResourceButton SetButton(Transform parent);
    }
    public interface ItemResource
    {
        public int amount { get; set; }
    }
    [System.Serializable]
    public class CommonResource : BaseGameResource, ItemResource
    {
        public ECommonResource type;
        public int amount { get; set; }
        CommonResourceSO config => DataSystem.Instance.dicConfigRefCommonResource[type];
        public override Sprite GetIcon()
        {
            return config.icon;
        }
        public override string GetName()
        {
            return config.GetName();
        }
        public override string GetDesc()
        {
            return config.GetDesc();
        }
        public string GetJsonData()
        {
            return $"{type.ExToString()}|{amount}";
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
        public override Sprite GetBGImg()
        {
            return config.GetBGImg();
        }
        public ERarity GetRarity()
        {
            return config.rarity;
        }

        public override string GetTextValue()
        {
            return amount.ToString();
        }

        public override bool IsVisualInInventory => config.IsVisualInInventory;

        // public override ResourceButton SetButton(Transform parent)
        // {
        //     ResourceButton tmpButton = PoolingSystem.Spawn<ResourceButton>(EPooling.ResourceButton, parent);
        //     tmpButton.SetUp(this, resourceValue.ToVisualString());
        //     return tmpButton;
        // }
    }
    [System.Serializable]
    public class EquipmentResource : BaseGameResource
    {
        public string uid;
        public EWeaponType typeId;
        public ERarity rarity => DataSystem.Instance.dataWeapon.dicWeapon[typeId].rarity;
        public int level;
        WeaponSO config => DataSystem.Instance.dicConfigRefWeapon[typeId];

        public override bool IsVisualInInventory => true;
        public void CreateNew(EWeaponType typeId)
        {
            this.typeId = typeId;
            uid = Ex.Extension.GetUID();
            level = 1;
        }

        public override Sprite GetBGImg()
        {
            return DataSystem.Instance.dataGeneral.dicRarityBG[rarity];
        }

        public override string GetDesc()
        {
            return config.GetDesc();
        }

        public override Sprite GetIcon()
        {
            return config.icon;
        }

        public string GetJsonData()
        {
            return $"{typeId.ExToString()}|{uid}|{level}";
        }

        public override string GetName()
        {
            return config.GetName();
        }

        public override string GetTextValue()
        {
            return config.GetTextValue();
        }
        // EquipmentConfig GetConfig()
        // {
        //     return DataSystem.Instance.dataEquipment.dicEquipmentConfig[typeId];
        // }
        // Stat GetBaseMainStat()
        // {
        //     return DataSystem.Instance.dataEquipment.dicRarityConfig[rarity].baseStats[type];
        // }
        // public Stat GetMainStat()
        // {
        //     return new Stat(GetBaseMainStat().type, GetBaseMainStat().isFlat, GetBaseMainStat().value + (IEquipmentController.Instance.CheckEquip(this) ? (IEquipmentController.Instance.GetSlotLevel(type) * DataSystem.Instance.dataEquipment.dicRarityConfig[rarity].baseStatIncreasePerLevel) : 0));
        // }
        // public override bool CompareType(GameResource source)
        // {
        //     return false;
        // }
        // public override bool VisualResource()
        // {
        //     return true;
        // }
        // public override string GetTextValue()
        // {
        //     return $"{LocalizationManager.GetTranslation("Lv")}.{(IEquipmentController.Instance.CheckEquip(this) ? IEquipmentController.Instance.GetSlotLevel(type) : 1)}";
        // }
        // public override List<GameResource> CorrectValue()
        // {
        //     return new List<GameResource> { this };
        // }
        // public List<Stat> GetListUniqueStatConfigs()
        // {
        //     return DataSystem.Instance.dataEquipment.GetListUniqueStats(rarity, typeId);
        // }
        // public int GetFragment()
        // {
        //     return DataSystem.Instance.dataEquipment.dicRarityConfig[rarity].fragment;
        // }
        // public EquipmentSetConfig GetSet()
        // {
        //     return DataSystem.Instance.dataEquipment.GetSet(GetConfig().setID);
        // }
        // public override ResourceButton SetButton(Transform parent)
        // {
        //     EquipmentButton tmpButton = PoolingSystem.Spawn<EquipmentButton>(EPooling.EquipmentButton, parent);
        //     tmpButton.SetUp(this);
        //     return tmpButton;
        // }
        // public void ApplyStats(PlayerStats stats, string keyGlobal)
        // {
        //     // apply main stat 
        //     stats.ApplyStats(GetMainStat().type, GetMainStat().value, keyGlobal, typeId, false);

        //     // apply unique stat
        //     List<Stat> lstUniqueStats = GetListUniqueStatConfigs();
        //     for (int i = 0; i < lstUniqueStats.Count; i++)
        //     {
        //         lstUniqueStats[i].ApplyStat(stats, keyGlobal, typeId + lstUniqueStats[i].type.ToString());
        //     }
        // }
    }
    [System.Serializable]
    public class ArtifactResource : BaseGameResource
    {
        public string uid;
        public EArtifactType typeId;
        public ERarity rarity;
        public int level;
        ArtifactSO config => DataSystem.Instance.dicConfigRefArtifact[typeId];

        public override bool IsVisualInInventory => true;
        public void CreateNew(EArtifactType typeId, ERarity rarity)
        {
            this.typeId = typeId;
            uid = Ex.Extension.GetUID();
            this.rarity = rarity;
            level = 1;
        }

        public override Sprite GetBGImg()
        {
            return DataSystem.Instance.dataGeneral.dicRarityBG[rarity];
        }

        public override string GetDesc()
        {
            return config.GetDesc();
        }

        public override Sprite GetIcon()
        {
            return config.icon;
        }

        public string GetJsonData()
        {
            return $"{typeId.ExToString()}|{uid}|{rarity.ExToString()}|{level}";
        }

        public override string GetName()
        {
            return config.GetName();
        }

        public override string GetTextValue()
        {
            return config.GetTextValue();
        }
    }
}