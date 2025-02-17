using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//职业培训的研究
public class VocationalTrainingStudy : MonoBehaviour
{


    string studyName = "职业培训";

    string details = "培训职业员工,提高产出的效率\n\n提升水泥搅拌工、钢铁铸造工以及挖掘机50%的效率";

    string Successful = "职业培训研究成功!";

    TechType techType = TechType.VocationalTraining;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币5000 ，铜矿1000
        [ResourceType.Currency] = 5000,
        [ResourceType.Copper] = 1000,
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
        //提升水泥工人50%效率
        ResourceAdditionManager.Instance.AddCement(0.5);
        //提升钢铁铸造工50%效率
        ResourceAdditionManager.Instance.AddSteel(0.5);

        //提升挖掘机50%效率
        ResourceAdditionManager.Instance.AddExcavator(0.5);


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
