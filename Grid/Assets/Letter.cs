using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Letter : MonoBehaviour
{
    [SerializeField] float moveTime = 2f;

    private float fontSize;

    private TextMeshProUGUI textMeshPro;
    private RectTransform rectTransform;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
        fontSize = textMeshPro.fontSize;

        InitializeLetterText();
    }

    private void InitializeLetterText()
    {
        string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
            "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        int letterIndex = Random.Range(0, letters.Length);
        textMeshPro.text = letters[letterIndex];
    }

    public void ChangePosition(Vector3 newPos)
    {
        StartCoroutine(ChangePositionRoutine(newPos));
    }

    IEnumerator ChangePositionRoutine(Vector3 newPos)
    {
        Vector2 startPos = transform.position;
        for (float i = 0; i < moveTime; i += Time.deltaTime)
        {
            transform.position = Vector2.Lerp(startPos, newPos, i/moveTime);
            yield return null;
        }
    }

    public void SetUpLetter(float fontMultiplier, float width)
    {   
        rectTransform.sizeDelta = new Vector2(width, width);
        textMeshPro.fontSize = fontSize*fontMultiplier;
    }
}
