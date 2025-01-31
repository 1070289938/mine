using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExpendManager : MonoBehaviour
{
  public TextMeshProUGUI nameText;

  public TextMeshProUGUI countText;

  string count;

  double doubleCount;

  public ResourceType type;

  bool addTime = false;//增加每秒提示

  // Start is called before the first frame update
  void Start()
  {
    nameText.text = type.GetName();
    countText.text = count;
  }

  // Update is called once per frame
  void Update()
  {

  }
  /// <summary>
  /// 初始化
  /// </summary>
  /// <param name="type"></param>
  /// <param name="count"></param>
  public void Install(ResourceType type, double count)
  {
    this.type = type;
    this.doubleCount = count;
    this.count = AssetsUtil.FormatNumber(count);

  }

  /// <summary>
  /// 初始化
  /// </summary>
  /// <param name="type"></param>
  /// <param name="count"></param>
  public void Install(ResourceType type, double count, bool addTime)
  {
    this.type = type;
    this.doubleCount = count;
    this.count = AssetsUtil.FormatNumber(count);
    this.addTime = addTime;
  }

  /// <summary>
  /// 更新内容
  /// </summary>
  /// <param name="count"></param>
  public void UpdateCount(double count)
  {
    if (doubleCount != count)
    {
      doubleCount = count;
      this.count = AssetsUtil.FormatNumber(count);
      string time = "";
      if (addTime)
      {
        time = " /s";
      }
      countText.text = this.count + time;
    }
  }


}
