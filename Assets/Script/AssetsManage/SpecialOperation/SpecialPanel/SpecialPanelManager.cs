using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPanelManager : MonoBehaviour
{

    public SpecialOptionType specialOptionType;//当前面板的科技类型




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 初始化内容
    /// </summary>
    public void Install(SpecialOptionType type)
    {
        Verify(type);
        specialOptionType = type;
    }

    void Verify(SpecialOptionType type)
    {
        if (type != specialOptionType)
        {
            Debug.LogError("特殊科技的ResearchRodStudy无法对应");
        }
    }



}
