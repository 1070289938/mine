using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using NPOI.SS.Formula.Functions;
public class DataProcessing : MonoBehaviour
{

    public static List<TechnologyBean> technologies;

    void Awake()
    {
        technologies = GetTechnologyBeans();
    }
    /// <summary>
    /// 获取所有的科技
    /// </summary>
    /// <returns>包含所有科技信息的 TechnologyBean 列表</returns>
    public List<TechnologyBean> GetTechnologyBeans()
    {
        // 获取指定路径和工作表索引的工作表
        ISheet sheet = GetISheet("/Data/Technology.xls", 0);
        if (sheet == null)
        {
            return null;
        }

        // 定义要获取数据的目标列名
        List<string> targetColumnNames = new List<string> { "id", "studyName", "title", "details", "successful", "stage", "resources", "advanceResources", "advanceTechType", "monitorTechType", "type", "revenue", "string" };
        // 用于存储列名和对应列索引的映射
        Dictionary<string, int> columnIndexMap = new Dictionary<string, int>();

        // 找到目标列名对应的列索引
        IRow headerRow = sheet.GetRow(0);
        if (headerRow != null)
        {
            for (int j = 0; j < headerRow.LastCellNum; j++)
            {
                ICell cell = headerRow.GetCell(j);
                if (cell != null)
                {
                    string columnName = cell.ToString();
                    if (targetColumnNames.Contains(columnName))
                    {
                        columnIndexMap[columnName] = j;
                    }
                }
            }
        }

        // 检查是否所有目标列都找到了对应的列索引
        foreach (string columnName in targetColumnNames)
        {
            if (!columnIndexMap.ContainsKey(columnName))
            {
                Debug.LogError($"未找到 '{columnName}' 列");
                return null;
            }
        }

        // 用于存储解析后的科技信息
        List<TechnologyBean> technologyBeans = new List<TechnologyBean>();

        // 从第三行开始遍历数据行
        for (int i = 2; i <= sheet.LastRowNum; i++)
        {
            IRow row = sheet.GetRow(i);
            if (row != null)
            {
                // 创建一个新的 TechnologyBean 对象来存储当前行的数据
                TechnologyBean bean = new TechnologyBean();

                if (row.GetCell(1) == null)
                {
                    continue;
                }
                string id = row.GetCell(1).ToString();

                // 遍历目标列，将对应单元格的值填充到 TechnologyBean 对象中
                foreach (KeyValuePair<string, int> entry in columnIndexMap)
                {
                    string columnName = entry.Key;
                    int columnIndex = entry.Value;
                    ICell cell = row.GetCell(columnIndex);
                    if (cell != null)
                    {
                        string cellValue = cell.ToString();
                        switch (columnName)
                        {
                            case "id":
                                bean.id = int.Parse(cellValue);
                                break;
                            case "studyName":
                                bean.studyName = cellValue;
                                break;
                            case "details":
                                string data = cellValue.Replace("\\n", "\n");
                                bean.details = data;
                                break;
                            case "successful":
                                bean.successful = cellValue;
                                break;
                            case "stage":
                                bean.stage = int.Parse(cellValue);
                                break;
                            case "resources":
                                Dictionary<ResourceType, double> resources = ParseStringToResourcesDictionary(id, cellValue);
                                bean.resources = resources;
                                break;
                            case "advanceResources":
                                List<ResourceType> resourceTypes = ParseStringToResourcesList(id, cellValue);
                                bean.advanceResources = resourceTypes;
                                break;
                            case "advanceTechType":
                                List<TechType> advanceTechType = ParseStringToTechTypeList(id, cellValue);
                                bean.advanceTechType = advanceTechType;
                                break;
                            case "monitorTechType":
                                List<TechType> monitorTechType = ParseStringToTechTypeList(id, cellValue);
                                bean.monitorTechType = monitorTechType;
                                break;
                            case "revenue":
                                Dictionary<string, double> revenue = ParseStringToRevenueDictionary(id, cellValue);
                                bean.revenue = revenue;
                                break;
                            case "title":
                                bean.studyTitle = cellValue;
                                break;
                            case "type":
                                bean.type = int.Parse(cellValue);
                                break;
                            case "string":
                                TechType techType = TechTypeHelper.StringToTechType(cellValue);
                                if (techType == TechType.empty)
                                {
                                    Debug.Log("科技解析错误");
                                }
                                bean.techType = techType;
                                break;
                        }
                    }
                }

                // 将填充好数据的 TechnologyBean 对象添加到列表中
                technologyBeans.Add(bean);
            }
        }


        RearPosition(technologyBeans);
        return technologyBeans;
    }

    /// <summary>
    /// 调整后置科技
    /// </summary>
    void RearPosition(List<TechnologyBean> technologyBeans)
    {

        //遍历所有的科技
        foreach (TechnologyBean technology in technologyBeans)
        {
            //获取前置监听
            List<TechType> techTypes = technology.advanceTechType;
            if(techTypes==null){
                continue;
            }
            if (techTypes.Count != 0)
            {
                foreach (TechnologyBean bean in technologyBeans)
                {
                    if (bean.techType ==techTypes[0]){
                        //增加后置监听
                        if(bean.monitorTechType==null){
                            bean.monitorTechType = new List<TechType>();
                        }
                        bean.monitorTechType.Add(technology.techType);
                    }
            }
            }
        }
    }





    /// <summary>
    /// 解析资源字符串
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Dictionary<ResourceType, double> ParseStringToResourcesDictionary(string id, string input)
    {
        Dictionary<ResourceType, double> dictionary = new();
        string[] pairs = input.Split(';');

        foreach (string pair in pairs)
        {
            if (!string.IsNullOrEmpty(pair))
            {
                string[] keyValue = pair.Split(',');
                if (keyValue.Length == 2)
                {
                    string key = keyValue[0];
                    ResourceType resourceType = ResourceTypeHelper.StringToResourceType(key);
                    if (resourceType == ResourceType.none)
                    {
                        Debug.LogError("id:" + id + "资源类型错误");
                    }
                    string value = keyValue[1];
                    dictionary[resourceType] = AssetsUtil.ParseNumber(value);
                }
            }
        }

        return dictionary;
    }

    /// <summary>
    /// 解析收益字符串
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Dictionary<string, double> ParseStringToRevenueDictionary(string id, string input)
    {
        Dictionary<string, double> dictionary = new();
        string[] pairs = input.Split(';');
        if (string.IsNullOrEmpty(input))
        {
            return dictionary;
        }
        foreach (string pair in pairs)
        {
            if (!string.IsNullOrEmpty(pair))
            {
                string[] keyValue = pair.Split(',');
                if (keyValue.Length == 2)
                {
                    string value = keyValue[1];
                    dictionary[keyValue[0]] = double.Parse(value);
                }
            }
        }

        return dictionary;
    }
    /// <summary>
    /// 解析前置资源
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    List<ResourceType> ParseStringToResourcesList(string id, string input)
    {
        List<ResourceType> list = new();
        if (string.IsNullOrEmpty(input))
        {
            return list;
        }
        string[] items = input.Split(';');

        foreach (string item in items)
        {
            if (string.IsNullOrEmpty(item))
            {
                continue;
            }
            ResourceType resourceType = ResourceTypeHelper.StringToResourceType(item);
            if (resourceType == ResourceType.none)
            {
                Debug.LogError("id:" + id + "前置资源类型错误:" + input + "-" + item);
            }
            list.Add(resourceType);
        }

        return list;
    }

    /// <summary>
    /// 解析科技
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    List<TechType> ParseStringToTechTypeList(string id, string input)
    {
        List<TechType> list = new();
        string[] items = input.Split(';');

        foreach (string item in items)
        {
            if (string.IsNullOrEmpty(item))
            {
                continue;
            }
            TechType techType = TechTypeHelper.StringToTechType(item);
            if (techType == TechType.empty)
            {
                Debug.LogError("id:" + id + "科技类型错误");
            }
            list.Add(techType);
        }

        return list;
    }
    /// <summary>
    /// 获取工作表
    /// </summary>
    /// <returns></returns>
    public ISheet GetISheet(string path, int id)
    {
        string filePath = Application.dataPath + path;
        if (File.Exists(filePath))
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                HSSFWorkbook workbook = new HSSFWorkbook(fs);
                ISheet sheet = workbook.GetSheetAt(id); // 获取第一个工作表
                return sheet;
            }
        }
        else
        {
            Debug.LogError("文件未找到: " + filePath);
            return null;
        }
    }



}
