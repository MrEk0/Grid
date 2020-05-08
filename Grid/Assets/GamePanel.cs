using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    [SerializeField] GameObject letterPrefab;

    private int columsCount;
    private int rowsCount;
    private float letterWidth;
    private float letterHeight;
    private RectTransform rectTransform;
    private List<Transform> letters;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        letterWidth = letterPrefab.GetComponent<RectTransform>().rect.width;
        letterHeight = letterPrefab.GetComponent<RectTransform>().rect.height;

        int maxColumns = Mathf.FloorToInt(rectTransform.rect.width / letterWidth);
        int maxRows = Mathf.FloorToInt(rectTransform.rect.height / letterHeight);
    }

    public void GenerateLetters()
    {
        if (rowsCount == 0 || columsCount == 0)
            return;

        
        letters = new List<Transform>();
        Vector2 tempPos = new Vector2(-letterWidth * 0.5f, letterHeight * 0.5f);
        Vector2 startPos = new Vector2(tempPos.x - letterWidth * (columsCount * 0.5f - 1), tempPos.y + letterHeight * (rowsCount * 0.5f - 1));

        for (int i = 0; i < columsCount; i++)
        {
            for (int j = 0; j < rowsCount; j++)
            {
                Vector2 letterPos = new Vector2(startPos.x+letterWidth*i, startPos.y-letterHeight*j);
                GameObject letter = Instantiate(letterPrefab, letterPos, Quaternion.identity);
                letter.transform.SetParent(transform, false);
                letters.Add(letter.transform);
            }
        }
    }

    public void ShuffleLetters()
    {
        List<Transform> takenPositions = new List<Transform>();
        Transform newTransform;

        foreach (var vector in letters)
        { 
            //do
            //{
                int posIndex = UnityEngine.Random.Range(0, letters.Count);
                newTransform = letters[posIndex];
            //}
            //while (!takenPositions.Contains(newTransform));
            Debug.Log(takenPositions.Contains(newTransform));
            takenPositions.Add(newTransform);
            vector.GetComponent<Letter>().ChangePosition(newTransform.position);
        }
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
