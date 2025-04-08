using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//链式机枪
public class ChainMachineGunStudy : MonoBehaviour
{


    string studyName = "链式机枪";

    string details = "为了拥有更大多的火力,科学家们提出了链式机枪的说法,可以装有更多的弹药拥有更大的火力\n\n提升士兵80%战斗力";

    string Successful = "链式机枪研究成功";

    TechType techType = TechType.ChainMachineGun;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("85M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("60k"),
        [ResourceType.Nanomaterials] = AssetsUtil.ParseNumber("1800"),

    }; //研究需要的资源
    // Start is called before the first frame update

    StudyItemManager studyItemManager;



    void Awake()
    {
        studyItemManager = GetComponent<StudyItemManager>();
        studyItemManager.Successful = Successful;


        studyItemManager.btn.onClick.AddListener(onClick);
        studyItemManager.Install(studyName, details, resources, techType);
        //检查方法
        studyItemManager.Inspect = Inspect;
        //研究方法
        studyItemManager.Study = Study;
    }

    void Inspect()
    {
        //穿甲弹
        if (TechManager.Instance.GetTechFlag(TechType.ArmourPiercingProjectile))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {
        ResourceAdditionManager.Instance.AddCombatPower(0.8);
    }


    // Update is called once per frame
    void Update()
    {

    }

    //点击事件
    void onClick()
    {
        studyItemManager.ShowItemMsg();

    }


}
