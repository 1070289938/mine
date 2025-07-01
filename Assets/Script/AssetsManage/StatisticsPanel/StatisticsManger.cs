using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatisticsManger : MonoBehaviour
{

    // 重生次数
    public TextMeshProUGUI secondLife;

    // 工人总数
    public TextMeshProUGUI totalWorkforce;

    // 重生晶体计算上限
    public TextMeshProUGUI secondLifeUpperLimit;
    // 重生晶体提升的储量
    public TextMeshProUGUI secondLifeReserves;

    // 飞升精华降低的资源蜕变
    public TextMeshProUGUI ascensionEssence;
    // Start is called before the first frame update
    void Start()
    {

    }


    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer -= 1;
            Compute();
        }
    }

    void Compute()
    {
        //重生次数
        secondLife.text = RegeneratedCrystalManager.Instance.GetSecondLifeCount().ToString();
        //工人总数
        totalWorkforce.text = ResourceCountManager.Instance.GetMinerCount().ToString();
        //重生晶体计算上限
        int max = 250;
        max += (int)ResourceManager.Instance.GetResource(ResourceType.DimensionalStone);
        secondLifeUpperLimit.text = max.ToString();

        // 重生晶体提升的储量
        double count = ResourceAdditionManager.Instance.GetSecondLifeReservesUp();
        secondLifeReserves.text = (count * 100) + "%";

        double metamorphosis = ResourceAdditionManager.Instance.GetMetamorphosis();

        ascensionEssence.text = (metamorphosis * 100) + "%(上限50%)";

    }



}
