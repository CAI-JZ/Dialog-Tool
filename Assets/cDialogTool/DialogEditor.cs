using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogEditor : EditorWindow
{
    private DialogData m_DialogData;
    
    [MenuItem("Tools/DialogEditor")]
    private static void OpenWindow()
    {
        DialogEditor editor = GetWindow<DialogEditor>(false, "DialogEditor");
    }

    //相当于update
    private void OnGUI()
    {
        DrawNodes();
        ProcessEvent(Event.current);
    }

    private void OnEnable()
    {
        m_DialogData = new DialogData();

    }

    private void DrawNodes()
    {
        DrawStartNode();
        DrawDialogNode();
    }

    private void ProcessEvent(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 1)
                {
                    MouseClickR(e);
                }
                break;
            case EventType.MouseUp:
                break;
            case EventType.MouseDrag:
                break;
            case EventType.KeyDown:
                break;
            case EventType.KeyUp:
                break;
            case EventType.ScrollWheel:
                break;
            default:
                break;
        }
    }

    private void MouseClickR(Event e)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("AddStartNode"),false,()=>AddStartNode(e.mousePosition));
        genericMenu.AddItem(new GUIContent("AddDialogNode"), false, () => AddDialogNode(e.mousePosition));
        genericMenu.AddItem(new GUIContent("AddChoiceNode"), false, () => AddChoiceNode(e.mousePosition));
        genericMenu.AddItem(new GUIContent("AddEventNode"), false, () => AddEventNode(e.mousePosition));
        genericMenu.ShowAsContext();
    }

    private void AddStartNode(Vector2 pos)
    {
        //在鼠标位置创建
        Debug.Log("Add start node");
        StartNode startNode = new StartNode(new Rect(pos,new Vector2(180, 80)));
        m_DialogData.startNodes.Add(startNode);

    }

    private void AddDialogNode(Vector2 pos)
    {
        Debug.Log("Add dialog node");
        DialogNode dialogNode = new DialogNode(new Rect(pos, new Vector2(180, 80)));
        m_DialogData.dialogNodes.Add(dialogNode);
    }

    private void AddChoiceNode(Vector2 pos)
    {

    }

    private void AddEventNode(Vector2 pos)
    {

    }

    private void DrawStartNode()
    {
        foreach (var startNode in m_DialogData.startNodes)
        {
            EditorGUI.DrawRect(startNode.Rect, new Color(1, 0.5f, 0.5f));
            EditorGUI.DrawRect(startNode.m_HeaderRect, Color.white);
            EditorGUI.DrawRect(new Rect(startNode.Rect.x, startNode.Rect.y + 20, startNode.m_HeaderRect.width, 1), Color.black);
        }
        
    }

    private void DrawDialogNode()
    {
        foreach (var dialogNode in m_DialogData.dialogNodes)
        {
            EditorGUI.DrawRect(dialogNode.Rect, new Color(0.9f, 0.8f, 0.6f));
            EditorGUI.DrawRect(dialogNode.m_HeaderRect, Color.white);
            EditorGUI.DrawRect(new Rect(dialogNode.Rect.x, dialogNode.Rect.y + 20, dialogNode.m_HeaderRect.width, 1), Color.black);
        }
    }
}
