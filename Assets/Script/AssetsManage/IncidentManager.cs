using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncidentManager : MonoBehaviour
{


    List<string> hole = new List<string>();//矿洞环境


    void Awake()
    {



        //矿洞
        hole.Add("你发现有一只蟑螂从你的脚边爬了过去");
        hole.Add("你感觉背后凉凉的,原来是有一滴水滴到了你的后脑勺");
        hole.Add("你被一颗石子绊倒了");
        hole.Add("你看到地上有一条蚂蚁排成的队伍");
        hole.Add("你偷偷在无人的角落上了个厕所....");
        hole.Add("你不小心踩死了一只蟑螂..蟑螂的酱汁在鞋底难以擦除");
        hole.Add("你有点困了..");






    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExecuteMethodWithProbability());//打印输出的协程
        //是否有矿工
        StartCoroutine(StoneMiner());
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void Carry(List<string> list)
    {
        Debug.Log("执行随机事件");
        // 生成一个介于 0 到 List 元素数量之间的随机索引
        int randomIndex = Random.Range(0, list.Count);
        string str = list[randomIndex];
        LogManager.Instance.AddLog(str);
    }
    public float probability = 0.02f;
    IEnumerator ExecuteMethodWithProbability()
    {
        while (true)
        {
            Debug.Log("执行一次协程");
            float randomValue = Random.value;
            if (randomValue < probability)
            {

                Carry(hole);
            }
            yield return new WaitForSeconds(1f);
        }

    }




    // 有矿工后有概率触发的事件
    IEnumerator StoneMiner()
    {
        while (true)
        {
            // 等待1秒
            yield return new WaitForSeconds(1f);
            FacilityPanelManager manager = FacilityManager.Instance.GetFacilityPanel(FacilityType.StoneMiner);
            // 如果矿工数量>1就ok
            if (manager.GetCount() > 0)
            {
                hole.Add("疼!你被不知道哪里弹来的石子砸到了脑袋");
                hole.Add("有两个矿工打起来了!");
                yield break; // 退出协程
            }
        }
    }

    // 到矿场后有概率触发的事件
    IEnumerator CallMethodEverySecond()
    {
        while (true)
        {
            // 等待1秒
            yield return new WaitForSeconds(1f);
            FacilityPanelManager manager = FacilityManager.Instance.GetFacilityPanel(FacilityType.StoneMiner);
            // 如果矿工数量>1就ok
            if (manager.GetCount() > 0)
            {
                hole.Add("疼!你被不知道哪里弹来的石子砸到了脑袋");
                hole.Add("有两个矿工打起来了!");
                yield break; // 退出协程
            }
        }
    }

}
