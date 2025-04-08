using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//狂热信徒
public class FanaticStudy : MonoBehaviour
{


    string studyName = "狂热信徒";

    string details = "信徒们更加的勤奋的祭拜祭坛\n\n每个寺庙都会提升祭坛的效率";

    string Successful = "狂热信徒研究成功";

    TechType techType = TechType.fanatic;
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {   //价格 软妹币 15M 科技点8k
        [ResourceType.Currency] = AssetsUtil.ParseNumber("60M"),
        [ResourceType.Science] = AssetsUtil.ParseNumber("55K"),

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
        //祭坛
        //寺庙
        if (TechManager.Instance.GetTechFlag(TechType.altar) &&
        TechManager.Instance.GetTechFlag(TechType.Temple))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);

        }
    }
    //研究按钮事件
    void Study()
    {

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
