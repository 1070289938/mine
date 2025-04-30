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
    public DailyBonusManager dailyBonusManager;
    public ProductionAccelerationManager productionAccelerationManager;
    public AccelerateImmediatelyManager accelerateImmediatelyManager;
    public RegeneratedCrystalManager regeneratedCrystalManager;
    public TipsManager tipsManager;
    public GameObject voiceOver;
    public BattlePanelManager battlePanelManager;
    public MarsPanelManager marsPanelManager;

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
        TryLoadGame();
        StartCoroutine(AutoSave());
    }

    void Start()
    {
        ExecuteStudiedTechs();
        ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();
    }

    public void SecondLife()
    {
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
            StudyFlag = GetStudyFlagDictionary(),
            logs = logManager.GetAllLogs(),
            facility = GetFacilityPanelCountDictionary(),
            speedTime = timeManager.RemainingTime,
            dailyBonus = LoadUtil.GetTimestampInMilliseconds(dailyBonusManager.lastClickDate),
            productionAcceleration = LoadUtil.GetTimestampInMilliseconds(productionAccelerationManager.lastClickTime),
            AccelerateImmediately = LoadUtil.GetTimestampInMilliseconds(accelerateImmediatelyManager.lastClickTime),
            saveTime = LoadUtil.GetTimestampInMilliseconds(DateTime.Now),
            militaryStrength = battlePanelManager.GetSoldierCount(),
            attackStrength = battlePanelManager.GetattackCount(),
            dangerValue = battlePanelManager.GetdangerCount(),
            autofill = battlePanelManager.GetAutofillCount(),
            secondLifeCount = regeneratedCrystalManager.GetSecondLifeCount(),
            marsPoints = marsPanelManager.GetThisCount()
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

    private void TryLoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            voiceOverManager.GameLoadVoiceOver();
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
            voiceOverManager.GameStartVoiceOver();
            techManager.techTypeStudyFlag = new Dictionary<TechType, bool>();
            Debug.Log("No save data found.");
        }
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
        techManager.techTypeStudyFlag = GetTechTypeStudyFlagDictionary(gameData.StudyFlag);
        logManager.LoadAllLogs(gameData.logs);
        dailyBonusManager.lastClickDate = LoadUtil.FromMillisecondsTimestamp(gameData.dailyBonus);
        productionAccelerationManager.lastClickTime = LoadUtil.FromMillisecondsTimestamp(gameData.productionAcceleration);
        accelerateImmediatelyManager.lastClickTime = LoadUtil.FromMillisecondsTimestamp(gameData.AccelerateImmediately);

        LoadFacilityPanelCounts(gameData.facility);
        timeManager.AddTime((int)gameData.speedTime);
        battlePanelManager.Install(gameData.militaryStrength, gameData.attackStrength, gameData.dangerValue, gameData.autofill);
        regeneratedCrystalManager.SetSecondLifeCount(gameData.secondLifeCount);
        marsPanelManager.LoadSave(gameData.marsPoints);
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
        tipsManager.ShowRevenue(CalculatedProduction(gameData.facility, elapsedTime, 0.2f));
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
            yield return new WaitForSeconds(60 * 10);
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
        var directResources = new Dictionary<ResourceType, double>();
        var manufacturedResources = new Dictionary<ResourceType, double>();
        foreach (var panel in facility)
            if (panel.Value.output != null)
                foreach (var output in panel.Value.output)
                    AddResource(directResources, manufacturedResources, output.Key, output.Value.val, output.Value.directBirth);

        CalculateActualProduction(directResources, time, offLineMultiple);
        CalculateActualProduction(manufacturedResources, time, offLineMultiple);

        var manufactureExpend = CalculateManufactureExpend(manufacturedResources);
        var resources = MergeDictionariesWithSum(directResources, manufacturedResources);
        var actualManufacturedResources = CalculateActualManufacturedResources(manufacturedResources, manufactureExpend, resources);
        var finalResources = CalculateFinalResources(resources, manufactureExpend, actualManufacturedResources);

        return finalResources;
    }

    private void AddResource(Dictionary<ResourceType, double> directResources, Dictionary<ResourceType, double> manufacturedResources, ResourceType resourceType, double value, bool directBirth)
    {
        var targetDict = directBirth ? directResources : manufacturedResources;
        AddOrUpdateResource(targetDict, resourceType, value);
    }

    private Dictionary<ResourceType, double> CalculateManufactureExpend(Dictionary<ResourceType, double> manufacturedResources)
    {
        var manufactureExpend = new Dictionary<ResourceType, double>();
        foreach (var manufacture in manufacturedResources)
            if (resourceManager.formula.ContainsKey(manufacture.Key))
                foreach (var expend in resourceManager.formula[manufacture.Key])
                    AddOrUpdateResource(manufactureExpend, expend.Key, expend.Value * manufacture.Value);
        return manufactureExpend;
    }

    private Dictionary<ResourceType, double> CalculateFinalResources(Dictionary<ResourceType, double> resources, Dictionary<ResourceType, double> manufactureExpend, Dictionary<ResourceType, double> actualManufacturedResources)
    {
        var finalResources = new Dictionary<ResourceType, double>(resources);
        var actualExpend = CalculateActualExpend(actualManufacturedResources);
        foreach (var expend in actualExpend)
            if (finalResources.ContainsKey(expend.Key))
            {
                finalResources[expend.Key] -= expend.Value;
                if (finalResources[expend.Key] < 0)
                    finalResources[expend.Key] = 0;
            }
        return finalResources;
    }

    private Dictionary<ResourceType, double> CalculateActualExpend(Dictionary<ResourceType, double> actualManufacturedResources)
    {
        var actualExpend = new Dictionary<ResourceType, double>();
        foreach (var manufacture in actualManufacturedResources)
            if (resourceManager.formula.ContainsKey(manufacture.Key))
                foreach (var expend in resourceManager.formula[manufacture.Key])
                    AddOrUpdateResource(actualExpend, expend.Key, expend.Value * manufacture.Value);
        return actualExpend;
    }

    private Dictionary<ResourceType, double> CalculateActualManufacturedResources(Dictionary<ResourceType, double> manufacturedResources, Dictionary<ResourceType, double> manufactureExpend, Dictionary<ResourceType, double> resources)
    {
        var actualManufacturedResources = new Dictionary<ResourceType, double>(manufacturedResources);
        foreach (var expend in manufactureExpend)
            if (resources.ContainsKey(expend.Key))
            {
                var available = resources[expend.Key];
                if (expend.Value > available)
                    foreach (var manufacture in manufacturedResources)
                        if (resourceManager.formula.ContainsKey(manufacture.Key) && resourceManager.formula[manufacture.Key].ContainsKey(expend.Key))
                        {
                            var factor = resourceManager.formula[manufacture.Key][expend.Key];
                            actualManufacturedResources[manufacture.Key] = manufacture.Value * (available / expend.Value);
                        }
            }
        return actualManufacturedResources;
    }

    private Dictionary<ResourceType, double> MergeDictionariesWithSum(Dictionary<ResourceType, double> dict1, Dictionary<ResourceType, double> dict2)
    {
        var result = new Dictionary<ResourceType, double>(dict1);
        foreach (var pair in dict2)
            if (result.ContainsKey(pair.Key))
                result[pair.Key] += pair.Value;
            else
                result.Add(pair.Key, pair.Value);
        return result;
    }

    private void AddOrUpdateResource(Dictionary<ResourceType, double> resourceDict, ResourceType resourceType, double value)
    {
        if (resourceDict.ContainsKey(resourceType))
            resourceDict[resourceType] += value;
        else
            resourceDict[resourceType] = value;
    }

    private void CalculateActualProduction(Dictionary<ResourceType, double> resources, long time, float offLineMultiple)
    {
        // 创建一个新的字典来存储计算后的结果
        Dictionary<ResourceType, double> newResources = new Dictionary<ResourceType, double>();
        foreach (var pair in resources)
        {
            newResources[pair.Key] = pair.Value * time * offLineMultiple;
        }
        // 清空原字典
        resources.Clear();
        // 将新字典的内容复制回原字典
        foreach (var pair in newResources)
        {
            resources[pair.Key] = pair.Value;
        }
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