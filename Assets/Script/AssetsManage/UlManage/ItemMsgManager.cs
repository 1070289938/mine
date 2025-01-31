using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMsgManager : MonoBehaviour
{

    public TextMeshProUGUI nameText;

    public TextMeshProUGUI detailsText;

    public GameObject priceList;

    public GameObject prefab; 


    public Button off;

    public Button study;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Install(StudyItemManager studyItem){
        nameText.text = studyItem.studyName;
        detailsText.text = studyItem.details;
        //销毁所有的子节点
        foreach (Transform child in priceList.transform)
        {
            Destroy(child.gameObject); // 销毁子物体
        }
            // 方法 1: 使用 foreach 遍历字典的键值对
        foreach (var res in studyItem.resources)
        {
             GameObject expend = Instantiate(prefab, priceList.transform);
             expend.GetComponent<ExpendManager>().Install(res.Key,res.Value);
        }
       
        
        study.onClick.RemoveAllListeners();
        study.onClick.AddListener(() =>{
            
            JudgmentResult result = studyItem.StudyFrame();
            if(!result.flag){
                //对应类型的资源出现红色抖动
                
                ExpendManager[] expends = GetComponentsInChildren<ExpendManager>();
                // 输出子节点的名称
                foreach (ExpendManager child in expends)
                {
                    if(child.type == result.type){
                        //让这个抖动
                        Animation animation = child.GetComponent<Animation>();

                        if (!animation.isPlaying) // 如果没有动画在播放
                        {
                                animation.Play("quiver");
                        }
                        return;
                    }
                }

            }


            off.onClick.Invoke();

        });

    

    }
    




}
