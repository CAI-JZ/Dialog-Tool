using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class CustomTextPreprocesseor : ITextPreprocessor
{
    public Dictionary<int, float> IntervalDictionary;

    public CustomTextPreprocesseor()
    {
        IntervalDictionary = new Dictionary<int, float>();
    }

    public string PreprocessText(string text)
    {
        IntervalDictionary.Clear();

        string processingText = text;
        string pattern = "<.*?>";
        Match match = Regex.Match(processingText, pattern);

        while (match.Success)
        {
            string label = match.Value.Substring(1, match.Length - 2);

            //尝试转换成浮点数
            if (float.TryParse(label, out float result))
            {
                IntervalDictionary[match.Index - 1] = result;
            }

            processingText = processingText.Remove(match.Index, match.Length);

            match = Regex.Match(processingText, pattern);
        }
        processingText = text;
        pattern = @"<(\d+)(\.\d+)?>"; //\d代表十进制数字。
        processingText = Regex.Replace(processingText, pattern, "");
        return processingText;
    }
}

public class AdvancedText : TextMeshProUGUI
{
    public AdvancedText()
    {
        textPreprocessor = new CustomTextPreprocesseor();
    }

    private CustomTextPreprocesseor SelfPreprocessor => (CustomTextPreprocesseor)textPreprocessor;

    public void TypingText(string content)
    {
        SetText(content);
        StartCoroutine(Typing());
    }

    private int _typingIndex;
    private float _defultInterval = 0.2f;
    IEnumerator Typing()
    {
        ForceMeshUpdate();
        for (int i = 0; i < m_characterCount; i++)
        {
            SetSingleCharacterAlpah(i, 0);
        }

        _typingIndex = 0;
        while (_typingIndex < m_characterCount)
        {
            if (textInfo.characterInfo[_typingIndex].isVisible)
            {
                StartCoroutine(FadeInCharacter(_typingIndex));
            }
            //SetSingleCharacterAlpah(_typingIndex, 255);
            
            if (SelfPreprocessor.IntervalDictionary.TryGetValue(_typingIndex, out float result))
            {
                yield return new WaitForSecondsRealtime(result);
            }
            else
            {
                yield return new WaitForSecondsRealtime(_defultInterval);
            }
            _typingIndex++;   
        }

        yield return null;
    }

    //newAlpha的范围是0-255
    private void SetSingleCharacterAlpah (int index, byte newAlpah)
    {
        TMP_CharacterInfo charInfo = textInfo.characterInfo[index];
        int matIndex = charInfo.materialReferenceIndex;
        int verIndex = charInfo.vertexIndex;
        for (int i = 0; i < 4; i++)
        {
            textInfo.meshInfo[matIndex].colors32[verIndex + i].a = newAlpah;
        }

        UpdateVertexData();
    }

    //制作渐入的效果
    IEnumerator FadeInCharacter(int index, float duration = 0.5f)
    {
        if (duration <= 0)
        {
            SetSingleCharacterAlpah(index, 255);
        }
        else
        {
            float timer = 0;
            while (timer < duration)
            {
                timer = Mathf.Min(duration, timer+Time.unscaledDeltaTime);
                SetSingleCharacterAlpah(index, (byte)(255 * timer / duration));
                yield return null;
            }
        }
    }
}
