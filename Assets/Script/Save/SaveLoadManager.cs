using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private const string saveFileName = "saveData.json";
    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, saveFileName);
        LoadGame();
    }

    // 保存游戏数据
    public void SaveGame()
    {
        // 获取需要保存的数据
        Vector3 playerPosition = GameObject.FindWithTag("Player").transform.position;
        int score = 0; // 假设这里有一个获取分数的逻辑

        GameData gameData = new GameData();

        // 将数据序列化为JSON字符串
        string jsonData = JsonUtility.ToJson(gameData);

        // 将JSON字符串写入文件
        File.WriteAllText(saveFilePath, jsonData);

        Debug.Log("Game saved!");
    }

    // 加载游戏数据
    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            // 从文件中读取JSON字符串
            string jsonData = File.ReadAllText(saveFilePath);

            // 将JSON字符串反序列化为GameData对象
            GameData gameData = JsonUtility.FromJson<GameData>(jsonData);

            // 应用加载的数据
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                player.transform.position = gameData.playerPosition;
            }
            // 这里可以处理分数的加载逻辑

            Debug.Log("Game loaded!");
        }
        else
        {
            Debug.Log("No save data found.");
        }
    }
}