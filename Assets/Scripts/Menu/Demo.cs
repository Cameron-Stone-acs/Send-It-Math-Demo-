using UnityEngine;
using TMPro;
public class Demo : MonoBehaviour
{
    float scale = 1;
    public float expandMaxSize = 1;
    public float beatSpeed = 1;
    void FixedUpdate()
    {
        scale = Mathf.PingPong(Time.time * beatSpeed, expandMaxSize) + 1;
        this.transform.localScale = new Vector3(scale, scale, 1f);
    }
}
