using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Letter : MonoBehaviour
{
    [SerializeField] float moveTime = 2f;
    private void Start()
    {
        string[] letters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
            "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        int letterIndex = Random.Range(0, letters.Length);
        GetComponent<TextMeshProUGUI>().text = letters[letterIndex];
    }

    public void ChangePosition(Vector3 newPos)
    {
        StartCoroutine(ChangePositionRoutine(newPos));
    }

    IEnumerator ChangePositionRoutine(Vector3 newPos)
    {
        float elapsedTime = 0f;
        Vector2 startPos = transform.position;
        for (float i = 0; i < moveTime; i += Time.deltaTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector2.Lerp(startPos, newPos, elapsedTime);
            yield return null;
        }
    }
}
