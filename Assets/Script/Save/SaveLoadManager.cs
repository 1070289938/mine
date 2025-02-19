using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class SaveLoadManager : MonoBehaviour
{

    public static bool Install = true;

    private const string saveFileName = "saveData.dat";
    private string saveFilePath;


    public ResourceManager resourceManager;//资源类管理

    public TechChecker techChecker;//科技监听管理

    public TechManager techManager;//科技研究管理

    public LogManager logManager;//日志管理

    public GameObject FacilityPanelContent;//面板的所有内容

    public VoiceOverManager voiceOverManager;//开场白

    public TimeManager timeManager;//时间管理

    public DailyBonusManager dailyBonusManager;//每日奖励

    public ProductionAccelerationManager productionAccelerationManager;//双倍加速奖励


    public static SaveLoadManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        saveFilePath = Path.Combine(Application.persistentDataPath, saveFileName);
        LoadGame();
        StartCoroutine(AutoSave());
    }



    /// <summary>
    /// 重生,清除掉除了系统资源之外的所有的资源
    /// </summary>
    public void SecondLife()
    {

        //面板数量全部归零
        FacilityPanelManager[] facilities = FacilityPanelContent.GetComponentsInChildren<FacilityPanelManager>(true);
        foreach (FacilityPanelManager facilityPanel in facilities)
        {

            facilityPanel.Clear();//清空面板内容
        }

        //科技全部归零
        TechManager.Instance.techTypeStudyFlag = new();
        //加成全部归零
        ResourceAdditionManager.Instance.Initialize();
        //资源全部归零
        ResourceManager.Instance.Initialize();
        //日志全部清空
        LogManager.Instance.Initialize();


        SaveGame();
    }


    // 保存游戏数据
    public void SaveGame()
    {

        //科技是否研究
        Dictionary<string, bool> StudyFlag = new Dictionary<string, bool>();

        foreach (var Study in techManager.techTypeStudyFlag)
        {
            string type = TechTypeHelper.TechTypeToString(Study.Key);
            StudyFlag[type] = Study.Value;
        }


        Dictionary<FacilityType, FacilityPanelCount> facility = new Dictionary<FacilityType, FacilityPanelCount>();

        FacilityPanelManager[] facilities = FacilityPanelContent.GetComponentsInChildren<FacilityPanelManager>(true);
        foreach (FacilityPanelManager facilityPanel in facilities)
        {
            FacilityPanelCount panelCount = new FacilityPanelCount();
            panelCount.operationQuantity = facilityPanel.GetCount();//获取运行数量
            panelCount.resourceQuantity = facilityPanel.GetMaxCount();//获取最大数量
            facility[facilityPanel.FacilityType] = panelCount;//保存数量
        }

        GameData gameData = new GameData
        {
            resources = resourceManager.resources,//现有资源
            resourcesMax = resourceManager.resourcesMax,//最大资源
            resourceUnlocks = resourceManager.resourceUnlocks,//资源是否显示

            StudyFlag = StudyFlag,//科技是否研究

            logs = logManager.GetAllLogs(),//日志
            facility = facility,//面板数量

            speedTime = timeManager.RemainingTime,

            dailyBonus = LoadUtil.GetTimestampInMilliseconds(dailyBonusManager.lastClickDate),
            productionAcceleration = LoadUtil.GetTimestampInMilliseconds(productionAccelerationManager.lastClickTime),

        };

        // 创建二进制格式化器
        BinaryFormatter formatter = new BinaryFormatter();
        // 创建文件流
        using (FileStream stream = new FileStream(saveFilePath, FileMode.Create))
        {
            // 将存档数据序列化并写入文件
            formatter.Serialize(stream, gameData);
        }

        Debug.Log("保存成功");
    }

    // 加载游戏数据
    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {



            //显示加载中开场白
            voiceOverManager.GameLoadVoiceOver();

            Install = false;//拥有存档文件，不需要初始化了

            // 创建二进制格式化器
            BinaryFormatter formatter = new BinaryFormatter();
            // 创建文件流
            using (FileStream stream = new FileStream(saveFilePath, FileMode.Open))
            {
                // 从文件中反序列化存档数据
                GameData gameData = (GameData)formatter.Deserialize(stream);
                if (gameData != null)
                {
                    resourceManager.resources = gameData.resources;//现有资源
                    resourceManager.resourcesMax = gameData.resourcesMax;//最大资源
                    resourceManager.resourceUnlocks = gameData.resourceUnlocks;//资源是否显示


                    Dictionary<TechType, bool> techTypeStudyFlag = new Dictionary<TechType, bool>();

                    foreach (var Study in gameData.StudyFlag)
                    {

                        TechType type = TechTypeHelper.StringToTechType(Study.Key);
                        techTypeStudyFlag[type] = Study.Value;
                    }


                    techManager.techTypeStudyFlag = techTypeStudyFlag;//科技是否研究
                    //加载日志
                    logManager.LoadAllLogs(gameData.logs);
                   

                    dailyBonusManager.lastClickDate = LoadUtil.FromMillisecondsTimestamp(gameData.dailyBonus);
                    productionAccelerationManager.lastClickTime = LoadUtil.FromMillisecondsTimestamp(gameData.productionAcceleration);


                    //初始化所有面板的数量
                    if (gameData.facility != null)
                    {
                        FacilityPanelManager[] facilities = FacilityPanelContent.GetComponentsInChildren<FacilityPanelManager>(true);
                        foreach (FacilityPanelManager facilityPanel in facilities)
                        {
                            if (gameData.facility.ContainsKey(facilityPanel.FacilityType))
                            {//如果有这个key就进行赋值
                                facilityPanel.LoadSaveCount(gameData.facility[facilityPanel.FacilityType]);
                            }

                        }

                    }

                    //加载剩余的加速时间
                    timeManager.AddTime((int)gameData.speedTime);



                }

            }

            Debug.Log("加载存档");
        }
        else
        {
            //显示开场旁白
            voiceOverManager.GameStartVoiceOver();
            //没有存档的情况下
            techManager.techTypeStudyFlag = new Dictionary<TechType, bool>();//初始化科技是否研究




            Debug.Log("No save data found.");
        }
    }


    void Start()
    {
        //立即执行一下所有已研究完成的科技的方法
        StudyItemManager[] studyItems = techManager.content.GetComponentsInChildren<StudyItemManager>(true);

        foreach (StudyItemManager itemManager in studyItems)
        {
            if (techManager.techTypeStudyFlag.ContainsKey(itemManager.techType))
            {

                if (techManager.techTypeStudyFlag[itemManager.techType])
                {
                    //如果这个科技在存档里面已经研究，那就执行一下
                    itemManager.Study.Invoke();
                }
            }

        }
        ResourceUpperLimitManager.Instance.RefreshUpperLimitAllResources();//刷新所有储存上限
    }

    // 自动保存协程
    private IEnumerator AutoSave()
    {
        while (true)
        {
            // 等待一分钟
            yield return new WaitForSeconds(5);
            // 保存游戏
            SaveGame();
        }
    }

}