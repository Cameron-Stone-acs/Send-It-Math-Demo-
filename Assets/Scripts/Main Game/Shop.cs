using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class Shop : MonoBehaviour
{
    public TMP_InputField answerText;
    public TMP_Text damageText;
    public GameObject player;

    private Player playerScript;
    private Math mathScript;

    public AudioSource sound;

    private int score = 0;

    void Start()
    {
        mathScript = GetComponent<Math>();
        playerScript = player.GetComponent<Player>();
    }

    void Update()
    {
        score = mathScript.score;
    }

    public void addDamage() 
    {
        score = mathScript.score;
        if (score >= 5)
        {
            score -= 5;
            mathScript.damage++;
            mathScript.score = score;
            damageText.text = "damage: " + mathScript.damage;
            sound.pitch = UnityEngine.Random.Range(0.74f, 1.26f);
            sound.Play();
        }
        mathScript.scoreText.text = "Score: " + score;
    }

    public void replenishHealth() 
    {
        score = mathScript.score;
        if (score >= 10)
        {
            score -= 10;
            playerScript.health = 10;
            mathScript.score = score;
            sound.pitch = UnityEngine.Random.Range(0.74f, 1.26f);
            sound.Play();
        }
        mathScript.scoreText.text = "Score: " + score;
    }

    public void revealAnswer() 
    {
        score = mathScript.score;
        if (score >= 25)
        {
            score -= 25;
            float Answer = mathScript.qAnswer;
            answerText.text = Answer.ToString();
            mathScript.score = score;
            sound.pitch = UnityEngine.Random.Range(0.74f, 1.26f);
            sound.Play();
        }
        mathScript.scoreText.text = "Score: " + score;
    }
}
