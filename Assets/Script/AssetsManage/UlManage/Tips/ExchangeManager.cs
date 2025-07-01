using System.Collections;
using System.Collections.Generic;
using TapTap.TapAd;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using System.Security.Cryptography;
/// <summary>
/// 兑换码
/// </summary>
public class ExchangeManager : MonoBehaviour
{
    /// <summary>
    /// 内容
    /// </summary>
    public TMP_InputField content;



    //取消按钮
    public Button drawDown;

    //导入按钮
    public Button doubleClaim;

    //粘贴按钮
    public Button affix;


    // Start is called before the first frame update
    void Start()
    {
        drawDown.onClick.AddListener(ReceiveIncome);
        doubleClaim.onClick.AddListener(WatchAdvertisement);
        affix.onClick.AddListener(Affix);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 取消
    /// </summary>
    public void ReceiveIncome()
    {
        TipsManager.Instance.OnClickAll();
    }



    // 兑换
    void WatchAdvertisement()
    {

        Redeem();
        TipsManager.Instance.OnClickAll();
    }

    void Affix()
    {
        string clipboardText = GUIUtility.systemCopyBuffer;
        if (string.IsNullOrEmpty(clipboardText))
        {
            LogManager.Instance.AddLog("粘贴板没有内容");
            // 这里可以添加处理剪贴板内容的逻辑，例如导入存档等
        }
        content.text = clipboardText;
    }
    /// <summary>
    /// 兑换
    /// </summary>
    public void Redeem()
    {
        string apiUrl = "https://poster-api.xd.cn/api/v1.0/cdk/game/submit-simple";//地址
        string clientId = "wcstctdngmqzpuadpz";//开发者后台 ClientId 信息
        string giftCode = content.text;//兑换码
        string characterId = Utils.GetUserId();//用户id
        string nonceStr = GenerateRandomLetters();//随机字符串
        Debug.Log(characterId);
        // 获取当前时间戳（秒）
        int timestamp = (int)System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1)).TotalSeconds;

        // 拼接并加密 sign 参数
        string sign = GetSign(timestamp, nonceStr, clientId);

        // 构建请求参数
        string requestBody = "{\"client_id\":\"" + clientId + "\",\"gift_code\":\"" + giftCode + "\",\"character_id\":\"" + characterId + "\",\"nonce_str\":\"" + nonceStr + "\",\"timestamp\":" + timestamp + ",\"sign\":\"" + sign + "\"}";


        PostManager.Instance.ToPost(apiUrl, requestBody, Callback);
    }

    void Callback(int type, string data)
    {
        if (type != 0)
        {
            return;
        }
        Debug.Log("兑换回调:" + data);
        JsonRoot root = JsonUtility.FromJson<JsonRoot>(data);

        if (root != null)
        {
            LogManager.Instance.AddLog("兑换成功!");
            foreach (Item item in root.content_obj)
            {
                if (item.name.Equals("Advertisement"))
                {
                    //1天86400秒
                    VIPManager.Instance.AddTime(item.number * 86400);
                    LogManager.Instance.AddLog("获得免广告:" + item.number + "天");
                    continue;
                }


                ResourceType resource = ResourceTypeHelper.StringToResourceType(item.name);
                Debug.Log($"物品名称: {item.name}, 数量: {item.number}");
                ResourceManager.Instance.AddResource(resource, item.number, false);
                LogManager.Instance.AddLog("获得:" + resource.GetName() + AssetsUtil.FormatNumber(item.number) + "个");




            }
            SaveLoadManager.Instance.Save();
        }
        else
        {

            LogManager.Instance.AddLog("兑换失败!");
        }
    }







    private string GetSign(int timestamp, string nonceStr, string clientId)
    {
        // 拼接参数并进行 SHA1 加密
        string signString = timestamp.ToString() + nonceStr + clientId;
        byte[] signBytes = Encoding.UTF8.GetBytes(signString);
        byte[] signHash = new SHA1CryptoServiceProvider().ComputeHash(signBytes);
        string sign = BitConverter.ToString(signHash).Replace("-", "").ToLowerInvariant();

        return sign;
    }

    // 生成仅含字母的随机 5 位字符串
    public string GenerateRandomLetters()
    {
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        System.Random random = new System.Random();
        string randomString = "";
        for (int i = 0; i < 5; i++)
        {
            int index = random.Next(letters.Length);
            randomString += letters[index];
        }
        return randomString;
    }

    // 生成仅含数字的随机 5 位字符串
    public string GenerateRandomNumbers()
    {
        string numbers = "0123456789";
        System.Random random = new System.Random();
        string randomString = "";
        for (int i = 0; i < 5; i++)
        {
            int index = random.Next(numbers.Length);
            randomString += numbers[index];
        }
        return randomString;
    }

    // 生成字母数字混合的随机 5 位字符串
    public string GenerateRandomAlphanumeric()
    {
        string alphanumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        System.Random random = new System.Random();
        string randomString = "";
        for (int i = 0; i < 5; i++)
        {
            int index = random.Next(alphanumeric.Length);
            randomString += alphanumeric[index];
        }
        return randomString;
    }
    // 定义物品类
    [System.Serializable]
    public class Item
    {
        public string name;
        public int number;
    }

    // 定义JSON根对象类
    [System.Serializable]
    public class JsonRoot
    {
        public string activity_id;
        public string c_sign;
        public List<Item> content_obj;
        public Dictionary<string, object> custom;
        public int error;
        public string nonce_str;
        public string sign;
        public bool success;
        public long timestamp;
    }
}
