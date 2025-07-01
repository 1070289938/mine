using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceContentManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 遍历节点并更新内容
        foreach (Transform child in transform)
        {
            ResourceType type = child.GetComponent<ResourceShowManager>().getResourceType();

            if (!child.gameObject.activeSelf)
            {

                if (ResourceManager.Instance.IsResourceUnlocked(type))
                {
                    child.gameObject.SetActive(true);
                }
            }
            else
            {
                if (!ResourceManager.Instance.IsResourceUnlocked(type))
                {
                    child.gameObject.SetActive(false);
                }
            }



        }
    }
}
