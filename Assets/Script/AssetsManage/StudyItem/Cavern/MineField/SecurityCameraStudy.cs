using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//安全摄像头的研究
public class SecurityCameraStudy : MonoBehaviour
{


    string studyName = "安全摄像头";

    string details = "在仓库附近安装安全摄像头,以提高安全程度来提高储量\n\n提升仓库35%最大储量";

    string Successful = "安全摄像头研究成功!";

    TechType techType = TechType.SecurityCamera;

    //研究需要的资源
    Dictionary<ResourceType, double> resources = new Dictionary<ResourceType, double>()
    {
        //设置价格 软妹币380k ，钢900 硅500
        [ResourceType.Currency] = AssetsUtil.ParseNumber("380k"),
        [ResourceType.Steel] = 900,
        [ResourceType.Silicon] = 500,
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
        //提升仓库35%储量上限
        FacilityPanelManager facilityPanel = FacilityManager.Instance.GetFacilityPanel(FacilityType.Stash);

        StashManager stashManager = facilityPanel.GetComponent<StashManager>();
        stashManager.AddUp(0.35);
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
