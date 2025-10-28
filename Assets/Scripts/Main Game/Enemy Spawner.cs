using Unity.Multiplayer.Center.Common;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public float temphealth = 10;
    public float health = 10f;
    public GameObject[] Enemies;
    public Slider healthbar;

    private float addedY;
    private GameObject summoned;

    void Start()
    {
        spawnEnemy();
    }

    void Update()
    {
        healthbar.value = health / temphealth;
        if (health <= 0) {
            Enemy myScript = summoned.GetComponent<Enemy>();
            myScript.Death();
            health = temphealth;
        }
    }
    
    public void answerWrong() 
    {
        Enemy myScript = summoned.GetComponent<Enemy>();
        myScript.Attack();
    }

    public void answerWright() 
    {
        Enemy myScript = summoned.GetComponent<Enemy>();
        myScript.Damage();
    }

    public void spawnEnemy()
    {
        temphealth = health;
        GameObject selected = Enemies[UnityEngine.Random.Range(0, Enemies.Length)];
        if (selected.name == "Evil Wizard") addedY = 0f;
        if (selected.name == "Evil Wizard 2") addedY = 0.37f;
        if (selected.name == "Evil Wizard 3") addedY = -0.05f;
        if (selected.name == "Fantasy Warrior") addedY = -0.5f;
        if (selected.name == "Hero Knight") addedY = -0.25f;
        if (selected.name == "Hero Knight 2") addedY = -0.93f;
        if (selected.name == "Huntress") addedY = -0.38f;
        if (selected.name == "Huntress 2") addedY = -0.68f;
        if (selected.name == "Martial Hero") addedY = -0.39f;
        if (selected.name == "Martial Hero 2") addedY = 0f;
        if (selected.name == "Martial Hero 3") addedY = -0.56f;
        if (selected.name == "Medieval King Pack 2") addedY = 1.35f;

        float yPosition = gameObject.transform.position.y + addedY;
        summoned = Instantiate(selected, new Vector3(gameObject.transform.position.x, yPosition, 0), Quaternion.identity, transform);
    }
}
