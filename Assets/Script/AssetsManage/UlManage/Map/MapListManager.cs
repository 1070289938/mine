using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapListManager : MonoBehaviour
{

    public TechType techType;//显示科技前提

    public string techString;//科技前提字符串

    public MapType mapType;

    public GameObject content;//内容

    void Awake()
    {
        techType = TechTypeHelper.StringToTechType(techString);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
