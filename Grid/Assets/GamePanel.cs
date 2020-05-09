using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    [SerializeField] GameObject letterPrefab;

    private int columsCount;
    private int rowsCount;
    private int maxColumns;
    private int maxRows;
    private float letterWidth;
    private float startWidth;
    private float multiplyIndex;

    private RectTransform rectTransform;
    private List<Transform> letterTransforms;
    private List<GameObject> letters;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        letterWidth = letterPrefab.GetComponent<RectTransform>().rect.width;
        startWidth = letterWidth;

        InitializeLetters();
    }

    private void InitializeLetters()
    {
        maxColumns = Mathf.FloorToInt(rectTransform.rect.width / letterWidth);
        maxRows = Mathf.FloorToInt(rectTransform.rect.height / letterWidth);

        letters = new List<GameObject>();
        for (int i = 0; i < maxColumns; i++)
        {
            for (int j = 0; j < maxRows; j++)
            {
                GameObject letter = Instantiate(letterPrefab, transform);
                letter.SetActive(false);
                letters.Add(letter);
            }
        }
    }

    public void GenerateLetters()
    {
        if (rowsCount == 0 || columsCount == 0)
            return;

        columsCount = columsCount > maxColumns ? maxColumns : columsCount;
        rowsCount = rowsCount > maxRows ? maxRows : rowsCount;

        FormLetterSize();
        DeactivateLetters();

        letterTransforms = new List<Transform>();
        Vector2 centerPos = new Vector2(-letterWidth * 0.5f, letterWidth * 0.5f);
        Vector2 startPos = new Vector2(centerPos.x - letterWidth * (columsCount * 0.5f - 1),
            centerPos.y + letterWidth * (rowsCount * 0.5f - 1));

        int k = 0;
        for (int i = 0; i < columsCount; i++)
        {
            for (int j = 0; j < rowsCount; j++)
            {
                ActivateLetters(startPos, k, i, j);
                k++;
            }
        }
    }

    private void DeactivateLetters()
    {
        if (letterTransforms != null && letterTransforms.Count > 0)
        {
            foreach (var letter in letterTransforms)
            {
                letter.gameObject.SetActive(false);
            }
        }
    }

    private void ActivateLetters(Vector2 startPos, int k, int i, int j)
    {
        Vector2 letterPos = new Vector2(startPos.x + letterWidth * i, startPos.y - letterWidth * j);
        GameObject letter = letters[k];
        letter.transform.SetParent(transform, false);
        letter.transform.localPosition = letterPos;
        letter.GetComponent<Letter>().SetUpLetter(multiplyIndex, letterWidth);
        letter.SetActive(true);
        letterTransforms.Add(letter.transform);
    }

    public void ShuffleLetters()
    {
        if (letterTransforms==null || letterTransforms.Count < 2)
            return;

        List<Transform> takenTransforms = new List<Transform>();
        Transform newTransform;

        foreach (var letter in letterTransforms)
        {
            do
            {
                int posIndex = UnityEngine.Random.Range(0, letterTransforms.Count);
                newTransform = letterTransforms[posIndex];
            }
            while (takenTransforms.Contains(newTransform));

            takenTransforms.Add(newTransform);
            letter.GetComponent<Letter>().ChangePosition(newTransform.position);
        }

        letterTransforms.Clear();
        letterTransforms.AddRange(takenTransforms);
        takenTransforms.Clear();
    }

    private void FormLetterSize()
    {
        multiplyIndex = maxColumns / columsCount;
        letterWidth = startWidth* multiplyIndex;
    }

    public void SetUpWidth(string width)
    {
        bool isDigit=width.All(char.IsDigit);
        if(isDigit)
        {
            columsCount = int.Parse(width);
        }
        else
        {
            columsCount = 0;
        }
    }

    public void SetUpHeight(string height)
    {
        bool isDigit = height.All(char.IsDigit);
        if (isDigit)
        {
            rowsCount = int.Parse(height);
        }
        else
        {
            rowsCount = 0;
        };
    }
}
