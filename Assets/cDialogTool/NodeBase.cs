using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NodeBase 
{
    protected Rect m_Rect;
    public Rect Rect => m_Rect;
    public Rect m_HeaderRect { get { return new Rect(m_Rect.x, m_Rect.y, m_Rect.width, 20); } }

}

public class StartNode : NodeBase
{
    public StartNode(Rect rect)
    {
        m_Rect = rect;
    }
}

public class DialogNode : NodeBase
{
    public DialogNode(Rect rect)
    {
        m_Rect = rect;
    }
}

public class ChoiceNode : NodeBase
{
    public ChoiceNode(Rect rect)
    {
        m_Rect = rect;
    }
}