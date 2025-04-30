using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{

    public Button save;//保存存档

    public Button derive;//导出存档

    public Button load;//导入存档

    // Start is called before the first frame update
    void Start()
    {
        save.onClick.AddListener(SaveLoadManager.Instance.Save);
        derive.onClick.AddListener(ExportSaveData);
        load.onClick.AddListener(LoadSaveData);
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void ExportSaveData()
    {
       
        string data = SaveLoadManager.Instance.ExportSaveData();
        Debug.Log(data);
        GUIUtility.systemCopyBuffer = data;
        LogManager.Instance.AddLog("存档代码复制成功!请自行保存");
    }

    public void LoadSaveData()
    {

        TipsManager.Instance.ShowLoad();

    }


}
