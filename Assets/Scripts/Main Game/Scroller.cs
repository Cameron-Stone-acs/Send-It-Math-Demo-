using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public RawImage image;
    public float x_Direction = 0.1f;
    public float y_Direction = 0.0f;
    public float scrollDuration = 2f;
    
    private bool isScrolling = false;
    private Vector2 scrollOffset = Vector2.zero;

    void Update()
    {
        if (isScrolling)
        {
            scrollOffset += new Vector2(x_Direction, y_Direction) * Time.deltaTime;
            image.uvRect = new Rect(scrollOffset, image.uvRect.size);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerScroll();
        }
    }
    
    public void TriggerScroll()
    {
        StartCoroutine(ScrollForDuration(scrollDuration));
    }

    private IEnumerator ScrollForDuration(float duration)
    {
        isScrolling = true;
        yield return new WaitForSeconds(duration);
        isScrolling = false;
    }
}
