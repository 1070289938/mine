using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{


    MapListManager[] mapListManagers;

    void Awake()
    {
        mapListManagers = gameObject.GetComponentsInChildren<MapListManager>(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (MapListManager mapList in mapListManagers)
        {
            mapList.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        foreach (MapListManager mapList in mapListManagers)
        {
            if (!mapList.gameObject.activeSelf)
            {
                if (TechManager.Instance.GetTechFlag(mapList.techType))
                {
                    //如果研究完成就显示
                    mapList.gameObject.SetActive(true);
                }
            }
        }
    }


    int show = 5;

    int hide = 0;
    /// <summary>
    /// 显示指定类型的内容
    /// </summary>
    void ShowMap(MapType mapType)
    {
        foreach (MapListManager mapList in mapListManagers)
        {
            if (mapList.mapType == mapType)
            {
                mapList.transform.SetSiblingIndex(show);
            }
            else
            {
                mapList.transform.SetSiblingIndex(hide);
            }
        }
    }
    /// <summary>
    /// 显示矿洞
    /// </summary>
    public void ShowMining()
    {
        ShowMap(MapType.Mining);
    }

    /// <summary>
    /// 显示矿厂
    /// </summary>
    public void ShowMineField()
    {
        ShowMap(MapType.MineField);
    }

    /// <summary>
    /// 显示矿山
    /// </summary>
    public void ShowMineHill()
    {
        ShowMap(MapType.MineHill);
    }
    /// <summary>
    /// 显示地核
    /// </summary>
    public void ShowEarthCore()
    {
        ShowMap(MapType.EarthCore);
    }
    /// <summary>
    /// 显示太空
    /// </summary>
    public void ShowOuterSpace()
    {
        ShowMap(MapType.OuterSpace);
    }
    /// <summary>
    /// 显示外星域
    /// </summary>
    public void ShowExtraterrestrial()
    {
        ShowMap(MapType.Extraterrestrial);
    }
    /// <summary>
    /// 显示异世界
    /// </summary>
    public void ShowAnotherWorld()
    {
        ShowMap(MapType.AnotherWorld);
    }

}
