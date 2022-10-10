using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogData : ScriptableObject
{
    public List<StartNode> startNodes;
    public List<DialogNode> dialogNodes;

    public DialogData()
    {
        startNodes = new List<StartNode>();
        dialogNodes = new List<DialogNode>();
    }

}
