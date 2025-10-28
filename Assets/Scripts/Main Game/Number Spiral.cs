using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberSpiral : MonoBehaviour
{
    // Tex Box stuff
    public RectTransform textBoxPrefab;
    private List<RectTransform> textBoxes = new List<RectTransform>();

    // Other script
    public Math math;

    // Settings
    public float radiusIncrement = 20f;
    public float angleIncrement = 30f;
    public float spinSpeed = 30f;
    public float maxRadius = 200f;
    public int spiralCount = 3;
    private float currentAngle;

    void Start()
    {
        ArrangeInSpiral();
    }

    void Update()
    {
        currentAngle += spinSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    // Basically an update for the spiral
    void ArrangeInSpiral()
    {
        // One list of boxes, divided across spiral arms
        int total = textBoxes.Count;

        for (int i = 0; i < total; i++)
        {
            int spiralIndex = i % spiralCount;
            int indexInSpiral = i / spiralCount;

            // Base angle offset for each spiral arm
            float spiralAngleOffset = (360f / spiralCount) * spiralIndex;

            float angle = indexInSpiral * angleIncrement + spiralAngleOffset;
            float radius = Mathf.Min(indexInSpiral * radiusIncrement, maxRadius);

            float rad = angle * Mathf.Deg2Rad;
            Vector2 pos = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;

            textBoxes[i].anchoredPosition = pos;
        }
    }

    // Create new text box
    public void SpawnTextBox()
    {
        // Get answer from maths cript
        string qAnswer = math.qAnswer.ToString();

        // Make box and reset position
        RectTransform newTextBox = Instantiate(textBoxPrefab, transform);
        newTextBox.anchoredPosition = Vector2.zero;

        // make it cild of number spiral
        TextMeshProUGUI tmpText = newTextBox.GetComponentInChildren<TextMeshProUGUI>();
        if (tmpText != null)
        {
            // Set its text and randomiz text color
            tmpText.text = qAnswer;
            tmpText.color = Random.ColorHSV(0f, 1f, 0.6f, 1f, 0.8f, 1f);
        }

        // Add text box to list and update list
        textBoxes.Add(newTextBox);
        ArrangeInSpiral();
    }
}
