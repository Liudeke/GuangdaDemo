using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalItem : ReusingScrollItemBase
{
    public override void SetContent(int index, Dictionary<string, object> data)
    {
      SetText("ID","ID:"+(string)data["ID"]);
    }
}
