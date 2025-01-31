using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpeedManager : MonoBehaviour
{

    public Button accelerate;//加速

    public Button retard;//减速

    public TextMeshProUGUI text;//文本

    int speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        accelerate.onClick.AddListener(OnAccelerate);
        retard.onClick.AddListener(Onretarde);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnAccelerate()
    {   

        speed++;
        text.text = speed.ToString();
        Time.timeScale = speed;
    }

    void Onretarde()
    {
        speed--;
        text.text = speed.ToString();
        Time.timeScale = speed;
    }


}
