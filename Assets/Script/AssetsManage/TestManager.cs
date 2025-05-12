using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestManager : MonoBehaviour
{

    Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(onClick);
    }



    void onClick()
    {

        ResourceManager.Instance.AddResource(ResourceType.RegeneratedCrystal, 1000, false);
        ResourceManager.Instance.AddResource(ResourceType.DimensionalStone, 100, false);
        //    foreach(TechnologyBean bean in  DataProcessing.technologies){

        //     if(!TechManager.Instance.GetTechFlag(bean.techType)){
        //         Debug.Log(bean.techType+"未研究");
        //     }

        //    }



    }

    // Update is called once per frame
    void Update()
    {

    }
}
