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



    void onClick(){

        ResourceManager.Instance.AddResource(ResourceType.RegeneratedCrystal,1);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
