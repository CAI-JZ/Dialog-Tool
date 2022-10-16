using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestText : MonoBehaviour
{
    [SerializeField]
    private AdvancedText _text;
    [SerializeField] private string content;

    void Start()
    {
        _text.TypingText(content);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
