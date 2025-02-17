using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacilityManager : MonoBehaviour
{
    public static FacilityManager Instance { get; private set; }

    public Dictionary<FacilityType, FacilityPanelManager> techTypeDictionary = new Dictionary<FacilityType, FacilityPanelManager>();//所有建筑节点

    public GameObject content;

    public GameObject resourceAddition;//资源的加成

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        //进行初始化
        FacilityPanelManager[] facilityPanels = content.GetComponentsInChildren<FacilityPanelManager>(true);
        foreach (FacilityPanelManager facility in facilityPanels)
        {
            techTypeDictionary[facility.FacilityType] = facility;
          
        }
    }
    /// <summary>
    /// 根据类型获取到面板管理
    /// </summary>
    /// <param name="type">类型</param>
    /// <returns></returns>
    public FacilityPanelManager GetFacilityPanel(FacilityType type)
    {
        return techTypeDictionary[type];
    }




}
