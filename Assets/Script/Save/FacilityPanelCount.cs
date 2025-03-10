using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 面板的对象
/// </summary>
[Serializable]
public class FacilityPanelCount
{

    public int resourceQuantity;//物体数量(正常)

    public int operationQuantity;//运行数量

    public int progressBarCount;//进度条

    public Dictionary<ResourceType, OutputForecast> output;//面板预计每秒产出

}
