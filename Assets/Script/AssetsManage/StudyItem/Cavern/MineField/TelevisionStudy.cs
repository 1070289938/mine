using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//电视的研究
public class TelevisionStudy : MonoBehaviour
{


    string studyName = "电视";

    string details = "在屋子里安装电视,用于提高屋子提供的收入以及矿工的效率\n\n提升25%房屋软妹币生产效率\n提升30%矿工效率";

    string Successful = "电视研究成功!";

    TechType techType = TechType.Television;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币50k ，硅100
        [ResourceType.Currency] = AssetsUtil.ParseNumber("50k"),
        [ResourceType.Silicon] = 100,
    };
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
        //互联网
        if (TechManager.Instance.GetTechFlag(TechType.Internet))
        {
            gameObject.SetActive(true);
            TechChecker.Instance.RemoveCheckMethod(Inspect);
        }
    }
    //研究按钮事件
    void Study()
    {
        //触发研究事件
        //提升30%员工加成
        ResourceAdditionManager.Instance.AddWorker(0.3);
        //提升25%软妹币加成
        ResourceAdditionManager.Instance.AddTenementComfort(0.25);

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
