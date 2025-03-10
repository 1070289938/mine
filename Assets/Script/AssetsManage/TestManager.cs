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
        Debug.Log("工人总数:"+ResourceCountManager.Instance.GetMinerCount());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
