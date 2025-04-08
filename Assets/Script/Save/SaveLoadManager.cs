using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

// 优化后的保存加载管理器类
public class SaveLoadManager : MonoBehaviour
{
    public static bool Install = true;
    private const string saveFileName = "saveData.dat";
    private const string backupFileName = "saveData_backup.dat";
    private string saveFilePath;
    private string backupFilePath;

    // 引用各类管理器
    public ResourceManager resourceManager;
    public TechChecker techChecker;
    public TechManager techManager;
    public LogManager logManager;
    public GameObject FacilityPanelContent;
    public VoiceOverManager voiceOverManager;
    public TimeManager timeManager;
    public DailyBonusManager dailyBonusManager;
    public ProductionAccelerationManager productionAccelerationManager;


    public RegeneratedCrystalManager regeneratedCrystalManager;


    public TipsManager tipsManager;

    public GameObject voiceOver;//旁白面板

    /// <summary>
    /// 战斗面板
    /// </summary>
    public BattlePanelManager battlePanelManager;

    public static SaveLoadManager Instance { get; private set; }


    FacilityPanelManager[] facilities;

    /// <summary>
    /// 保存按钮
    /// </summary>
    public Button saveBtn;

    private void Awake()
    {
        Instance = this;

        facilities = FacilityPanelContent.GetComponentsInChildren<FacilityPanelManager>(true);

        saveFilePath = Path.Combine(Application.persistentDataPath, saveFileName);
        backupFilePath = Path.Combine(Application.persistentDataPath, backupFileName);
        TryLoadGame();
        StartCoroutine(AutoSave());
        saveBtn.onClick.AddListener(Save);
    }

    /// <summary>
    /// 重生,清除掉除了系统资源之外的所有的资源
    /// </summary>
    public void SecondLife()
    {
        int count = regeneratedCrystalManager.GetSecondLifeCount();
        regeneratedCrystalManager.SetSecondLifeCount(count + 1);

        ClearFacilities();
        ResetTech();
        ResetAdditions();
        ResetResources();
        ClearLogs();

        SaveGame(true);
    }

    public void Save()
    {

        SaveGame(false);
    }
    // 封装清除设施面板内容的方法
    private void ClearFacilities()
    {

        foreach (FacilityPanelManager facilityPanel in facilities)
        {
            facilityPanel.Clear();
        }
    }

    // 封装重置科技研究状态的方法
    private void ResetTech()
    {

        // 存储需要修改的键
        List<TechType> keysToUpdate = new List<TechType>();

        // 遍历 techTypeStudyFlag 并找出需要修改的键
        foreach (var item in techManager.techTypeStudyFlag)
        {
            if (!techManager.specialTech.ContainsKey(item.Key))
            {
                keysToUpdate.Add(item.Key);
            }
        }

        // 修改需要修改的键对应的值
        foreach (var key in keysToUpdate)
        {
            techManager.techTypeStudyFlag[key] = false;
        }
    }

    // 封装重置资源加成的方法
    private void ResetAdditions()
    {
        ResourceAdditionManager.Instance.Initialize();
    }

    // 封装重置资源的方法
    private void ResetResources()
    {
        ResourceManager.Instance.Initialize();
    }

    // 封装清除日志的方法
    private void ClearLogs()
    {
        LogManager.Instance.Initialize();
    }

    // 保存游戏数据
    public async void SaveGame(bool SecondLife)
    {
        try
        {
            // 备份现有存档
            BackupSaveFile();

            long time = LoadUtil.GetTimestampInMilliseconds(DateTime.Now);

            GameData gameData = CreateGameData(SecondLife);

            Debug.Log("创建对象" + (LoadUtil.GetTimestampInMilliseconds(DateTime.Now) - time));
            time = LoadUtil.GetTimestampInMilliseconds(DateTime.Now);
            await SaveGameDataToFileAsync(gameData);
            Debug.Log("保存对象" + (LoadUtil.GetTimestampInMilliseconds(DateTime.Now) - time));
            // 确认保存成功后删除备份文件
            DeleteBackupFile();

            Debug.Log("保存成功");
            LogManager.Instance.AddLog("保存成功");
        }
        catch (Exception ex)
        {
            Debug.LogError($"保存游戏数据时出错: {ex.Message}");

            // 出现错误时恢复备份文件
            RestoreBackupFile();
        }
    }
    // 将游戏数据异步保存到文件
    private async Task SaveGameDataToFileAsync(GameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(saveFilePath, FileMode.Create))
        {
            await Task.Run(() => formatter.Serialize(stream, gameData));
        }
    }
    // 创建游戏数据对象
    private GameData CreateGameData(bool secondLife)
    {
        var studyFlag = GetStudyFlagDictionary();
        var facility = GetFacilityPanelCountDictionary();

        return new GameData
        {
            secondLife = secondLife,
            resources = resourceManager.resources,
            resourcesMax = resourceManager.resourcesMax,
            resourceUnlocks = resourceManager.resourceUnlocks,
            StudyFlag = studyFlag,
            logs = logManager.GetAllLogs(),
            facility = facility,
            speedTime = timeManager.RemainingTime,
            dailyBonus = LoadUtil.GetTimestampInMilliseconds(dailyBonusManager.lastClickDate),
            productionAcceleration = LoadUtil.GetTimestampInMilliseconds(productionAccelerationManager.lastClickTime),
            saveTime = LoadUtil.GetTimestampInMilliseconds(DateTime.Now),
            militaryStrength = battlePanelManager.GetSoldierCount(),
            attackStrength = battlePanelManager.GetattackCount(),
            dangerValue = battlePanelManager.GetdangerCount(),
            autofill = battlePanelManager.GetAutofillCount(),
            secondLifeCount = regeneratedCrystalManager.GetSecondLifeCount(),
        };
    }

    // 获取科技研究标志字典
    private Dictionary<string, bool> GetStudyFlagDictionary()
    {
        Dictionary<string, bool> studyFlag = new Dictionary<string, bool>();
        foreach (var study in techManager.techTypeStudyFlag)
        {
            string type = TechTypeHelper.TechTypeToString(study.Key);
            studyFlag[type] = study.Value;
        }
        return studyFlag;
    }

    // 获取设施面板数量字典
    private Dictionary<FacilityType, FacilityPanelCount> GetFacilityPanelCountDictionary()
    {
        Dictionary<FacilityType, FacilityPanelCount> facility = new Dictionary<FacilityType, FacilityPanelCount>();

        foreach (FacilityPanelManager facilityPanel in facilities)
        {
            FacilityPanelCount panelCount = new FacilityPanelCount
            {
                operationQuantity = facilityPanel.GetCount(),
                resourceQuantity = facilityPanel.GetMaxCount(),
                progressBarCount = facilityPanel.GetScheduleCount(),
                output = facilityPanel.GetThisOutPut()
            };
            facility[facilityPanel.FacilityType] = panelCount;
        }
        return facility;
    }

    // 将游戏数据保存到文件
    private void SaveGameDataToFile(GameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(saveFilePath, FileMode.Create))
        {
            formatter.Serialize(stream, gameData);
        }
    }

    // 尝试加载游戏数据
    private void TryLoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            voiceOverManager.GameLoadVoiceOver();
            Install = false;

            GameData gameData = LoadGameDataFromFile();
            if (gameData != null)
            {
                ApplyGameData(gameData);
                //如果不是重生存档就计算离线资源，否则不计算
                if (!gameData.secondLife)
                {
                    CalculateOfflineProduction(gameData);
                }

            }

            Debug.Log("加载存档");
        }
        else
        {
            voiceOverManager.GameStartVoiceOver();
            techManager.techTypeStudyFlag = new Dictionary<TechType, bool>();
            Debug.Log("No save data found.");
        }
    }

    // 从文件中加载游戏数据
    private GameData LoadGameDataFromFile()
    {
        try
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(saveFilePath, FileMode.Open))
            {
                return (GameData)formatter.Deserialize(stream);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"加载游戏数据时出错: {ex.Message}");
            // 尝试从备份文件恢复
            if (RestoreBackupFile())
            {
                return LoadGameDataFromFile();
            }
            return null;
        }
    }

    // 应用加载的游戏数据
    private void ApplyGameData(GameData gameData)
    {
        resourceManager.resources = gameData.resources;
        resourceManager.resourcesMax = gameData.resourcesMax;
        resourceManager.resourceUnlocks = gameData.resourceUnlocks;

        var techTypeStudyFlag = GetTechTypeStudyFlagDictionary(gameData.StudyFlag);
        techManager.techTypeStudyFlag = techTypeStudyFlag;

        logManager.LoadAllLogs(gameData.logs);

        dailyBonusManager.lastClickDate = LoadUtil.FromMillisecondsTimestamp(gameData.dailyBonus);
        productionAccelerationManager.lastClickTime = LoadUtil.FromMillisecondsTimestamp(gameData.productionAcceleration);

        LoadFacilityPanelCounts(gameData.facility);

        timeManager.AddTime((int)gameData.speedTime);

        battlePanelManager.Install(gameData.militaryStrength, gameData.attackStrength, gameData.dangerValue, gameData.autofill);

        regeneratedCrystalManager.SetSecondLifeCount(gameData.secondLifeCount);
    }

    // 获取科技类型研究标志字典
    private Dictionary<TechType, bool> GetTechTypeStudyFlagDictionary(Dictionary<string, bool> studyFlag)
    {
        Dictionary<TechType, bool> techTypeStudyFlag = new Dictionary<TechType, bool>();
        foreach (var study in studyFlag)
        {
            TechType type = TechTypeHelper.StringToTechType(study.Key);
            techTypeStudyFlag[type] = study.Value;
        }
        return techTypeStudyFlag;
    }

    // 加载设施面板数量
    private void LoadFacilityPanelCounts(Dictionary<FacilityType, FacilityPanelCount> facility)
    {

        foreach (FacilityPanelManager facilityPanel in facilities)
        {
            if (facility.ContainsKey(facilityPanel.FacilityType))
            {
                facilityPanel.LoadSaveCount(facility[facilityPanel.FacilityType]);
            }
        }
    }

    // 计算离线产量
    private void CalculateOfflineProduction(GameData gameData)
    {
        long currentTime = LoadUtil.GetTimestampInMilliseconds(DateTime.Now);
        long elapsedTime = (currentTime - gameData.saveTime) / 1000;

        CalculatedProduction(gameData.facility, elapsedTime);
    }

    void Start()
    {
        ExecuteStudiedTechs();
        ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();

    }

    // 执行已研究完成的科技方法
    private void ExecuteStudiedTechs()
    {
        StudyItemManager[] studyItems = techManager.content.GetComponentsInChildren<StudyItemManager>(true);
        foreach (StudyItemManager itemManager in studyItems)
        {
            if (techManager.techTypeStudyFlag.ContainsKey(itemManager.techType) &&
            techManager.techTypeStudyFlag[itemManager.techType])
            {
                itemManager.Study.Invoke();
            }
        }

        StudyItemManager[] systemStudyItems = techManager.SystemContent.GetComponentsInChildren<StudyItemManager>(true);
        foreach (StudyItemManager itemManager in systemStudyItems)
        {
            if (techManager.techTypeStudyFlag.ContainsKey(itemManager.techType) &&
            techManager.techTypeStudyFlag[itemManager.techType])
            {
                itemManager.Study.Invoke();
            }
        }
    }

    // 自动保存协程
    private IEnumerator AutoSave()
    {
        while (true)
        {
            //10分钟自动保存一次
            yield return new WaitForSeconds(60 * 10); // 60 * 1
            if (!voiceOver.activeSelf)
            {
                SaveGame(false);
            }

        }
    }

    // 离线资源的倍数
    float offLineMultiple = 0.2f;

    /// <summary>
    /// 计算面板的离线产量
    /// </summary>
    /// <param name="facility"></param>
    private void CalculatedProduction(Dictionary<FacilityType, FacilityPanelCount> facility, long time)
    {
        Dictionary<ResourceType, double> directResources = new Dictionary<ResourceType, double>();
        Dictionary<ResourceType, double> manufacturedResources = new Dictionary<ResourceType, double>();

        // 统计各类资源产量
        foreach (var panel in facility)
        {
            FacilityPanelCount panelCount = panel.Value;
            if (panelCount.output != null)
            {
                foreach (var output in panelCount.output)
                {
                    if (output.Value.directBirth)
                    {
                        AddOrUpdateResource(directResources, output.Key, output.Value.val);
                    }
                    else
                    {
                        AddOrUpdateResource(manufacturedResources, output.Key, output.Value.val);
                    }
                }
            }
        }

        // 计算实际产量
        CalculateActualProduction(directResources, time);
        CalculateActualProduction(manufacturedResources, time);

        // 资源的消耗
        var manufactureExpend = new Dictionary<ResourceType, double>();
        // 统计制作资源的消耗
        foreach (var manufacture in manufacturedResources)
        {
            if (resourceManager.formula.ContainsKey(manufacture.Key))
            {
                var expendMap = resourceManager.formula[manufacture.Key];
                foreach (var expend in expendMap)
                {
                    AddOrUpdateResource(manufactureExpend, expend.Key, expend.Value * manufacture.Value);
                }
            }
        }

        Dictionary<ResourceType, double> resources = MergeDictionariesWithSum(directResources, manufacturedResources);

        // 计算实际可消耗的制造离线收益
        Dictionary<ResourceType, double> actualManufacturedResources = CalculateActualManufacturedResources(manufacturedResources, manufactureExpend, resources);

        // 计算消耗后的总收益
        Dictionary<ResourceType, double> finalResources = CalculateFinalResources(resources, manufactureExpend, actualManufacturedResources);

        Debug.Log("直产离线收益:" + JsonConvert.SerializeObject(directResources));
        Debug.Log("制造离线收益:" + JsonConvert.SerializeObject(manufacturedResources));
        Debug.Log("实际可消耗的制造离线收益:" + JsonConvert.SerializeObject(actualManufacturedResources));
        Debug.Log("离线总收益:" + JsonConvert.SerializeObject(resources));
        Debug.Log("制造离线消耗:" + JsonConvert.SerializeObject(manufactureExpend));
        Debug.Log("消耗后的总收益:" + JsonConvert.SerializeObject(finalResources));

        tipsManager.ShowRevenue(finalResources);



    }




    // 计算消耗后的总收益
    private Dictionary<ResourceType, double> CalculateFinalResources(Dictionary<ResourceType, double> resources, Dictionary<ResourceType, double> manufactureExpend, Dictionary<ResourceType, double> actualManufacturedResources)
    {
        var finalResources = new Dictionary<ResourceType, double>(resources);
        var actualExpend = new Dictionary<ResourceType, double>();
        // 重新计算实际消耗
        foreach (var manufacture in actualManufacturedResources)
        {
            if (resourceManager.formula.ContainsKey(manufacture.Key))
            {
                var expendMap = resourceManager.formula[manufacture.Key];
                foreach (var expend in expendMap)
                {
                    AddOrUpdateResource(actualExpend, expend.Key, expend.Value * manufacture.Value);
                }
            }
        }
        // 扣除实际消耗
        foreach (var expend in actualExpend)
        {
            if (finalResources.ContainsKey(expend.Key))
            {
                finalResources[expend.Key] -= expend.Value;
                if (finalResources[expend.Key] < 0)
                {
                    finalResources[expend.Key] = 0;
                }
            }
        }
        return finalResources;
    }

    // 计算实际可消耗的制造离线收益
    private Dictionary<ResourceType, double> CalculateActualManufacturedResources(Dictionary<ResourceType, double> manufacturedResources, Dictionary<ResourceType, double> manufactureExpend, Dictionary<ResourceType, double> resources)
    {
        var actualManufacturedResources = new Dictionary<ResourceType, double>(manufacturedResources);
        foreach (var expend in manufactureExpend)
        {
            if (resources.ContainsKey(expend.Key))
            {
                double available = resources[expend.Key];
                if (expend.Value > available)
                {
                    // 消耗超过可用资源，调整制造资源
                    foreach (var manufacture in manufacturedResources)
                    {
                        if (resourceManager.formula.ContainsKey(manufacture.Key) && resourceManager.formula[manufacture.Key].ContainsKey(expend.Key))
                        {
                            double factor = resourceManager.formula[manufacture.Key][expend.Key];
                            double newAmount = manufacture.Value * (available / expend.Value);
                            actualManufacturedResources[manufacture.Key] = newAmount;
                        }
                    }
                }
            }
        }
        return actualManufacturedResources;
    }

    /// <summary>
    /// 合并内容
    /// </summary>
    /// <param name="dict1"></param>
    /// <param name="dict2"></param>
    /// <returns></returns>
    Dictionary<ResourceType, double> MergeDictionariesWithSum(Dictionary<ResourceType, double> dict1, Dictionary<ResourceType, double> dict2)
    {
        // 创建一个新的 Dictionary 用于存储合并结果
        Dictionary<ResourceType, double> result = new Dictionary<ResourceType, double>(dict1);

        // 遍历第二个 Dictionary
        foreach (KeyValuePair<ResourceType, double> pair in dict2)
        {
            if (result.ContainsKey(pair.Key))
            {
                // 如果键已存在，将对应的值相加
                result[pair.Key] += pair.Value;
            }
            else
            {
                // 如果键不存在，直接添加到结果 Dictionary 中
                result.Add(pair.Key, pair.Value);
            }
        }

        return result;
    }
    // 添加或更新资源字典中的值
    private void AddOrUpdateResource(Dictionary<ResourceType, double> resourceDict, ResourceType resourceType, double value)
    {
        if (resourceDict.ContainsKey(resourceType))
        {
            resourceDict[resourceType] += value;
        }
        else
        {
            resourceDict[resourceType] = value;
        }
    }

    // 计算实际产量
    private void CalculateActualProduction(Dictionary<ResourceType, double> resources, long time)
    {
        // 获取资源类型的键数组
        ResourceType[] keys = new ResourceType[resources.Count];
        resources.Keys.CopyTo(keys, 0);

        // 遍历键数组，更新资源的值
        for (int i = 0; i < keys.Length; i++)
        {
            ResourceType key = keys[i];
            resources[key] = resources[key] * time * offLineMultiple;
        }
    }

    // 备份存档文件
    private void BackupSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Copy(saveFilePath, backupFilePath, true);
        }
    }

    // 删除备份文件
    private void DeleteBackupFile()
    {
        if (File.Exists(backupFilePath))
        {
            File.Delete(backupFilePath);
        }
    }

    // 恢复备份文件
    private bool RestoreBackupFile()
    {
        if (File.Exists(backupFilePath))
        {
            File.Copy(backupFilePath, saveFilePath, true);
            return true;
        }
        return false;
    }
}