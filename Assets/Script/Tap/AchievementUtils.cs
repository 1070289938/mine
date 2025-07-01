using System.Collections;
using System.Collections.Generic;
using TapSDK.Achievement;
using UnityEngine;

public class AchievementUtils
{



    /// <summary>
    /// 解锁成就
    /// </summary>
    /// <param name="achievement"></param>
    public static void Unlock(string achievement)
    {
        TapTapAchievement.Unlock(achievementId: achievement);
    }



    /// <summary>
    /// 保存成就
    /// </summary>
    public static void SaveAchievement()
    {
        try
        {
            int power = BattlePanelManager.Instance.GetPower();
            if (power >= AssetsUtil.ParseNumber("500k"))
            {
                Unlock(Achievement.MilitaryEmpire);
            }
            int minerCount = ResourceCountManager.Instance.GetMinerCount();
            if (minerCount >= 20)
            {
                Unlock(Achievement.MiningOperation);
            }
            if (minerCount >= 200)
            {
                Unlock(Achievement.boss);
            }
            if (minerCount >= 2000)
            {
                Unlock(Achievement.MineralEmpire);
            }

            double moneyCount = ResourceManager.Instance.GetResource(ResourceType.Currency);

            if (moneyCount >= AssetsUtil.ParseNumber("100k"))
            {
                Unlock(Achievement.affluence);
            }
            if (moneyCount >= AssetsUtil.ParseNumber("100M"))
            {
                Unlock(Achievement.rich);
            }
            if (moneyCount >= AssetsUtil.ParseNumber("100G"))
            {
                Unlock(Achievement.richMan);
            }
            if (ResourceManager.Instance.resourcesHistory.ContainsKey(ResourceType.Science))
            {
                double scienceCount = ResourceManager.Instance.resourcesHistory[ResourceType.Science];
                if (scienceCount >= AssetsUtil.ParseNumber("1M"))
                {
                    Unlock(Achievement.intellectual);
                }
                if (scienceCount >= AssetsUtil.ParseNumber("100G"))
                {
                    Unlock(Achievement.TechEmpire);
                }
                if (scienceCount >= AssetsUtil.ParseNumber("10P"))
                {
                    Unlock(Achievement.Technology);
                }

            }

            if (ResourceManager.Instance.resourcesHistory.ContainsKey(ResourceType.Stone))
            {

                double stoneCount = ResourceManager.Instance.resourcesHistory[ResourceType.Stone];
                if (stoneCount >= AssetsUtil.ParseNumber("500P"))
                {
                    Unlock(Achievement.DrillingThroughPlanet);
                }
            }

            if (ResourceManager.Instance.resourcesHistory.ContainsKey(ResourceType.Steel))
            {
                double steelCount = ResourceManager.Instance.resourcesHistory[ResourceType.Steel];
                if (steelCount >= AssetsUtil.ParseNumber("100k"))
                {
                    Unlock(Achievement.SteelMagnate);
                }
            }


            int bankCount = FacilityManager.Instance.GetFacilityPanel(FacilityType.Bank).GetMaxCount();
            if (bankCount >= 100)
            {
                Unlock(Achievement.lombard);
            }

        }
        catch (System.Exception)
        {
            LogManager.Instance.AddLog("保存成就时发生异常");
        }





    }




}
