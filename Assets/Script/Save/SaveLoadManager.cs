using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TapSDK.Core;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveLoadManager : MonoBehaviour
{
    public static bool Install = true;
    private const string saveFileName = "saveData.dat";
    private const string backupFileName = "saveData_backup.dat";
    private string saveFilePath;
    private string backupFilePath;

    public ResourceManager resourceManager;
    public TechManager techManager;
    public LogManager logManager;
    public GameObject FacilityPanelContent;
    public VoiceOverManager voiceOverManager;
    public TimeManager timeManager;
    public VIPManager vipManager;
    public DailyBonusManager dailyBonusManager;
    public ProductionAccelerationManager productionAccelerationManager;
    public AccelerateImmediatelyManager accelerateImmediatelyManager;
    public RegeneratedCrystalManager regeneratedCrystalManager;
    public TipsManager tipsManager;
    public GameObject voiceOver;
    public BattlePanelManager battlePanelManager;
    public MarsPanelManager marsPanelManager;

    public DeepSpacePanelManager deepSpacePanelManager;

    public static SaveLoadManager Instance { get; private set; }
    private FacilityPanelManager[] facilities;

    private void Awake()
    {
        Instance = this;
        facilities = FacilityPanelContent.GetComponentsInChildren<FacilityPanelManager>(true);
        saveFilePath = GetSaveFilePath(saveFileName);
        backupFilePath = GetSaveFilePath(backupFileName);
        EnsureDirectoryExists(saveFilePath);
        EnsureDirectoryExists(backupFilePath);
        StartCoroutine(AutoSave());
    }

    void Start()
    {

        ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();
    }

    public void SecondLife()
    {
        AchievementUtils.SaveAchievement();
        regeneratedCrystalManager.SetSecondLifeCount(regeneratedCrystalManager.GetSecondLifeCount() + 1);
        ResetGameState();
        SaveGame(true);
    }

    public void Save() => SaveGame(false);

    private void ResetGameState()
    {
        ClearFacilities();
        ResetTech();
        ResetAdditions();
        ResetResources();
        ClearLogs();
        marsPanelManager.LoadSave(0);//清空殖民点数
        deepSpacePanelManager.LoadSave(0);//清空深空点数
        battlePanelManager.ClearSoldier();//清空士兵数量
    }

    private void ClearFacilities()
    {
        foreach (var facilityPanel in facilities)
            facilityPanel.Clear();
    }

    private void ResetTech()
    {

        // 复制字典的键到一个新的列表
        var keys = new List<TechType>(techManager.techTypeStudyFlag.Keys);

        foreach (var key in keys)
        {
            if (!techManager.specialTech.ContainsKey(key))
            {
                techManager.techTypeStudyFlag[key] = false;
            }
        }
    }

    private void ResetAdditions() => ResourceAdditionManager.Instance.Initialize();
    private void ResetResources() => ResourceManager.Instance.Initialize();
    private void ClearLogs() => LogManager.Instance.Initialize();

    public void SaveGame(bool secondLife)
    {
        AchievementUtils.SaveAchievement();
        try
        {
            PerformBackup();
            var gameData = CreateGameData(secondLife);
            SaveGameDataToFile(gameData);
            DeleteBackupFile();
            Debug.Log("保存成功");
            logManager.AddLog("保存成功");
        }
        catch (Exception ex)
        {
            logManager.AddLog($"保存游戏数据时出错: {ex.Message}");
            Debug.LogError($"保存游戏数据时出错: {ex.Message}");
            RestoreBackupFile();
        }
    }





    private void SaveGameDataToFile(GameData gameData)
    {

        EnsureDirectoryExists(saveFilePath);
        using (var stream = new FileStream(saveFilePath, FileMode.Create))
        {
            var formatter = new BinaryFormatter();



            // foreach (var resources in gameData.resources)
            // {
            //     if (resources.Key == ResourceType.Currency)
            //         logManager.AddLog("保存资源:" + resources.Key.GetName() + "--" + AssetsUtil.FormatNumber(resources.Value));
            // }




            formatter.Serialize(stream, gameData);
        }
    }

    private GameData CreateGameData(bool secondLife)
    {
        return new GameData
        {
            secondLife = secondLife,
            resources = resourceManager.resources,
            resourcesMax = resourceManager.resourcesMax,
            resourceUnlocks = resourceManager.resourceUnlocks,
            resourcesHistory = resourceManager.resourcesHistory,
            StudyFlag = GetStudyFlagDictionary(),
            logs = logManager.GetAllLogs(),
            facility = GetFacilityPanelCountDictionary(),
            speedTime = timeManager.RemainingTime,
            dailyBonus = LoadUtil.GetTimestampInMilliseconds(dailyBonusManager.lastRefreshDate),
            dailyBonusCount = dailyBonusManager.remainingClaims,

            productionAcceleration = LoadUtil.GetTimestampInMilliseconds(productionAccelerationManager.lastRefreshDate),
            productionAccelerationCount = productionAccelerationManager.remainingClaims,

            AccelerateImmediately = LoadUtil.GetTimestampInMilliseconds(accelerateImmediatelyManager.lastRefreshDate),
            AccelerateImmediatelyCount = accelerateImmediatelyManager.remainingClaims,

            saveTime = LoadUtil.GetTimestampInMilliseconds(DateTime.Now),
            militaryStrength = battlePanelManager.GetSoldierCount(),
            attackStrength = battlePanelManager.GetattackCount(),
            dangerValue = battlePanelManager.GetdangerCount(),
            autofill = battlePanelManager.GetAutofillCount(),
            secondLifeCount = regeneratedCrystalManager.GetSecondLifeCount(),
            marsPoints = marsPanelManager.GetThisCount(),
            deepSpacePoints = deepSpacePanelManager.GetThisCount(),
            vipTime = vipManager.ExpiredTime
        };
    }

    private Dictionary<string, bool> GetStudyFlagDictionary()
    {
        var studyFlag = new Dictionary<string, bool>();
        foreach (var study in techManager.techTypeStudyFlag)
            studyFlag[TechTypeHelper.TechTypeToString(study.Key)] = study.Value;
        return studyFlag;
    }

    public Dictionary<FacilityType, FacilityPanelCount> GetFacilityPanelCountDictionary()
    {
        var facility = new Dictionary<FacilityType, FacilityPanelCount>();
        foreach (var facilityPanel in facilities)
        {
            facility[facilityPanel.FacilityType] = new FacilityPanelCount
            {
                operationQuantity = facilityPanel.GetCount(),
                resourceQuantity = facilityPanel.GetMaxCount(),
                progressBarCount = facilityPanel.GetScheduleCount(),
                output = facilityPanel.GetThisOutPut()
            };
        }
        return facility;
    }
    /// <summary>
    /// 开始加载存档
    /// </summary>
    public void TryLoadGame()
    {
        if (File.Exists(saveFilePath))
        {

            Install = false;
            var gameData = LoadGameDataFromFile(saveFilePath);
            if (gameData != null)
            {
                ApplyGameData(gameData);
                if (!gameData.secondLife)
                    CalculateOfflineProduction(gameData);
            }
            Debug.Log("加载存档");
        }
        else
        {

            techManager.techTypeStudyFlag = new Dictionary<TechType, bool>();
            voiceOverManager.GameStartVoiceOver();
            Debug.Log("No save data found.");
        }
        ExecuteStudiedTechs();
    }

    public void Reload()
    {
        // 获取当前场景的名称
        string sceneName = SceneManager.GetActiveScene().name;
        // 重新加载当前场景
        SceneManager.LoadScene(sceneName);
    }

    private GameData LoadGameDataFromFile(string path)
    {
        try
        {
            using (var stream = new FileStream(path, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                return (GameData)formatter.Deserialize(stream);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"加载游戏数据时出错: {ex.Message}");
            return null;
        }
    }

    private void ApplyGameData(GameData gameData)
    {
        resourceManager.resources = gameData.resources;





        resourceManager.resourcesMax = gameData.resourcesMax;
        resourceManager.resourceUnlocks = gameData.resourceUnlocks;
        if (gameData.resourcesHistory != null)
        {
            resourceManager.resourcesHistory = gameData.resourcesHistory;
        }



        techManager.techTypeStudyFlag = GetTechTypeStudyFlagDictionary(gameData.StudyFlag);
        logManager.LoadAllLogs(gameData.logs);
        dailyBonusManager.lastRefreshDate = LoadUtil.FromMillisecondsTimestamp(gameData.dailyBonus);
        dailyBonusManager.remainingClaims = gameData.dailyBonusCount;


        productionAccelerationManager.lastRefreshDate = LoadUtil.FromMillisecondsTimestamp(gameData.productionAcceleration);
        productionAccelerationManager.remainingClaims = gameData.productionAccelerationCount;

        accelerateImmediatelyManager.lastRefreshDate = LoadUtil.FromMillisecondsTimestamp(gameData.AccelerateImmediately);
        accelerateImmediatelyManager.remainingClaims = gameData.AccelerateImmediatelyCount;

        LoadFacilityPanelCounts(gameData.facility);
        timeManager.AddTime((int)gameData.speedTime);
        battlePanelManager.Install(gameData.militaryStrength, gameData.attackStrength, gameData.dangerValue, gameData.autofill);
        regeneratedCrystalManager.SetSecondLifeCount(gameData.secondLifeCount);
        marsPanelManager.LoadSave(gameData.marsPoints);
        deepSpacePanelManager.LoadSave(gameData.deepSpacePoints);
        vipManager.ExpiredTime = gameData.vipTime;


        dailyBonusManager.start = true;
        productionAccelerationManager.start = true;
        accelerateImmediatelyManager.start = true;
        Debug.Log("存档加载完毕");


        //   foreach (var resources in resourceManager.resources)
        // {
        //     if (resources.Key == ResourceType.Currency)
        //     {
        //         logManager.AddLog("加载资源:" + resources.Key.GetName() + "--" + AssetsUtil.FormatNumber(resources.Value));
        //     }

        // }

    }

    private Dictionary<TechType, bool> GetTechTypeStudyFlagDictionary(Dictionary<string, bool> studyFlag)
    {
        var techTypeStudyFlag = new Dictionary<TechType, bool>();
        foreach (var study in studyFlag)
            techTypeStudyFlag[TechTypeHelper.StringToTechType(study.Key)] = study.Value;
        return techTypeStudyFlag;
    }

    private void LoadFacilityPanelCounts(Dictionary<FacilityType, FacilityPanelCount> facility)
    {
        foreach (var facilityPanel in facilities)
            if (facility.ContainsKey(facilityPanel.FacilityType))
                facilityPanel.LoadSaveCount(facility[facilityPanel.FacilityType]);
    }

    private void CalculateOfflineProduction(GameData gameData)
    {
        var elapsedTime = (LoadUtil.GetTimestampInMilliseconds(DateTime.Now) - gameData.saveTime) / 1000;
        int time;
        if (elapsedTime > 86400)
        {
            time = 86400;
        }
        else
        {
            time = (int)elapsedTime;
        }



        tipsManager.ShowRevenue(CalculatedProduction(gameData.facility, time, 0.4f), time);
    }

    private void ExecuteStudiedTechs()
    {
        ExecuteStudiedTechsInContent(techManager.content);
        ExecuteStudiedTechsInContent(techManager.SystemContent);
    }

    private void ExecuteStudiedTechsInContent(GameObject content)
    {
        var studyItems = content.GetComponentsInChildren<StudyItemManager>(true);
        foreach (var itemManager in studyItems)
            if (techManager.techTypeStudyFlag.ContainsKey(itemManager.techType) && techManager.techTypeStudyFlag[itemManager.techType])
                itemManager.Study.Invoke();
    }

    private IEnumerator AutoSave()
    {
        while (true)
        {
            yield return new WaitForSeconds(60 * 3);
            if (!voiceOver.activeSelf)
                SaveGame(false);
        }
    }

    private string GetSaveFilePath(string saveFileName)
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                return Path.Combine(Application.persistentDataPath, saveFileName);
            case RuntimePlatform.IPhonePlayer:
                return Path.Combine(Application.dataPath, saveFileName);
            case RuntimePlatform.WindowsPlayer:
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments), saveFileName);
            default:
                return Path.Combine(Application.persistentDataPath, saveFileName);
        }
    }

    public Dictionary<ResourceType, double> CalculatedProduction(Dictionary<FacilityType, FacilityPanelCount> facility, long time, float offLineMultiple)
    {
        // 合并使用单个资源池，同时记录产出和消耗
        var resources = new Dictionary<ResourceType, double>();

        // 1. 计算直接产出资源
        foreach (var panel in facility)
        {
            if (panel.Value.output == null) continue;

            foreach (var output in panel.Value.output)
            {
                if (output.Value.directBirth)
                {
                    double amount = output.Value.val * time * offLineMultiple;
                    AddOrUpdateResource(resources, output.Key, amount);
                }
            }
        }

        // 2. 计算加工产出（在同一个资源池上操作）
        foreach (var panel in facility)
        {
            if (panel.Key == FacilityType.MemoryFurnace) continue;
            if (panel.Value.output == null) continue;

            foreach (var output in panel.Value.output)
            {
                if (!output.Value.directBirth && resourceManager.formula.ContainsKey(output.Key))
                {
                    ResourceType product = output.Key;
                    if (product == ResourceType.memoryAlloy)
                    {
                        continue;
                    }
                    double baseOutput = output.Value.val * time * offLineMultiple;
                    var formula = resourceManager.formula[product];
                    // 计算基于当前资源的最大可能产出
                    double maxPossible = CalculateMaxProduction(resources, formula);
                    // 实际产出不能超过理论最大值
                    double actualOutput = Math.Min(maxPossible, baseOutput);
                    if (actualOutput > 0)
                    {
                        // 添加加工产出
                        AddOrUpdateResource(resources, product, actualOutput);

                        // 扣除消耗的原材料（直接在resources上操作）
                        foreach (var expend in formula)
                        {
                            double consumed = expend.Value * actualOutput;
                            resources[expend.Key] -= consumed;

                            // 确保资源不会为负（处理浮点精度问题）
                            if (resources[expend.Key] < 0)
                                resources[expend.Key] = 0;
                        }
                    }
                }
            }
        }

        if (facility.ContainsKey(FacilityType.MemoryFurnace))
        {
            //单独计算记忆合金
            foreach (var output in facility[FacilityType.MemoryFurnace].output)
            {
                if (!output.Value.directBirth && resourceManager.formula.ContainsKey(output.Key))
                {
                    ResourceType product = output.Key;
                    double baseOutput = output.Value.val * time * offLineMultiple;
                    var formula = resourceManager.formula[product];
                    // 计算基于当前资源的最大可能产出
                    double maxPossible = CalculateMaxProduction(resources, formula);
                    // 实际产出不能超过理论最大值
                    double actualOutput = Math.Min(maxPossible, baseOutput);
                    if (actualOutput > 0)
                    {
                        // 添加加工产出
                        AddOrUpdateResource(resources, product, actualOutput);

                        // 扣除消耗的原材料（直接在resources上操作）
                        foreach (var expend in formula)
                        {
                            double consumed = expend.Value * actualOutput;
                            resources[expend.Key] -= consumed;

                            // 确保资源不会为负（处理浮点精度问题）
                            if (resources[expend.Key] < 0)
                                resources[expend.Key] = 0;
                        }
                    }
                }
            }
        }


        return resources; // 返回包含所有产出和消耗的最终资源池
    }

    // 辅助方法：基于当前资源池计算最大可能产出
    private double CalculateMaxProduction(Dictionary<ResourceType, double> resources, Dictionary<ResourceType, double> formula)
    {
        double maxOutput = double.MaxValue;

        foreach (var expend in formula)
        {
            ResourceType resourceType = expend.Key;
            double requiredPerUnit = expend.Value;
            Debug.Log("需求资源:" + resourceType.GetName());
            Debug.Log("需求数量:" + requiredPerUnit);
            // 如果资源不足，无法生产
            Debug.Log("拥有的资源数量:" + resources.ContainsKey(resourceType));
            Debug.Log("拥有的资源数量" + resources[resourceType]);
            if (!resources.ContainsKey(resourceType) || resources[resourceType] <= 0)
                return 0;

            // 计算受当前资源限制的最大产出
            double currentMax = resources[resourceType] / requiredPerUnit;
            if (currentMax < maxOutput)
                maxOutput = currentMax;
        }

        return maxOutput;
    }



    private void AddOrUpdateResource(Dictionary<ResourceType, double> resourceDict, ResourceType resourceType, double value)
    {
        if (resourceDict.ContainsKey(resourceType))
            resourceDict[resourceType] += value;
        else
            resourceDict[resourceType] = value;
    }

    private void PerformBackup()
    {
        EnsureDirectoryExists(backupFilePath);
        if (File.Exists(saveFilePath))
            File.Copy(saveFilePath, backupFilePath, true);
    }

    private void DeleteBackupFile()
    {
        if (File.Exists(backupFilePath))
            File.Delete(backupFilePath);
    }

    private bool RestoreBackupFile()
    {
        EnsureDirectoryExists(saveFilePath);
        if (File.Exists(backupFilePath))
        {
            File.Copy(backupFilePath, saveFilePath, true);
            return true;
        }
        return false;
    }

    public string ExportSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            var fileBytes = File.ReadAllBytes(saveFilePath);
            var compressedBytes = CompressBytes(fileBytes);
            var encryptedBytes = EncryptBytes(compressedBytes);
            return Convert.ToBase64String(encryptedBytes);
        }
        return null;
    }

    public bool ImportSaveData(string encryptedCode)
    {
        try
        {
            var encryptedBytes = Convert.FromBase64String(encryptedCode);
            var decryptedBytes = DecryptBytes(encryptedBytes);
            var decompressedBytes = DecompressBytes(decryptedBytes);
            EnsureDirectoryExists(saveFilePath);
            File.WriteAllBytes(saveFilePath, decompressedBytes);
            Debug.Log("导入存档成功");
            logManager.AddLog("导入存档成功");
            Reload();
            return true;
        }
        catch (Exception ex)
        {
            logManager.AddLog($"导入存档时出错:无效的存档代码");
            return false;
        }
    }

    private byte[] EncryptBytes(byte[] data)
    {
        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes("AValid256BitEncryptionKey2345678");
            aesAlg.IV = new byte[16];
            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    csEncrypt.Write(data, 0, data.Length);
                    csEncrypt.FlushFinalBlock();
                    return msEncrypt.ToArray();
                }
            }
        }
    }

    private byte[] DecryptBytes(byte[] cipherText)
    {
        using (var aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes("AValid256BitEncryptionKey2345678");
            aesAlg.IV = new byte[16];
            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using (var msDecrypt = new MemoryStream())
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                {
                    csDecrypt.Write(cipherText, 0, cipherText.Length);
                    csDecrypt.FlushFinalBlock();
                    return msDecrypt.ToArray();
                }
            }
        }
    }

    private byte[] CompressBytes(byte[] data)
    {
        using (var outputStream = new MemoryStream())
        {
            using (var gZipStream = new GZipStream(outputStream, CompressionMode.Compress))
            {
                gZipStream.Write(data, 0, data.Length);
            }
            return outputStream.ToArray();
        }
    }

    private byte[] DecompressBytes(byte[] data)
    {
        using (var inputStream = new MemoryStream(data))
        {
            using (var outputStream = new MemoryStream())
            {
                using (var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress))
                {
                    gZipStream.CopyTo(outputStream);
                }
                return outputStream.ToArray();
            }
        }
    }

    private void EnsureDirectoryExists(string path)
    {
        var directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }











}