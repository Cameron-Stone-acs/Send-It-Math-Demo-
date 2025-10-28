using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class Enemy : MonoBehaviour
{
    public float AttackSpeed = 3f;
    public float damage = 1f;

    private GameObject player;
    private GameObject enemySpawner;
    private Animator animator;
    private bool isDead = false;
    private bool isAnimating = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        enemySpawner = GameObject.Find("Enemy Spawner");
    }

    public void Attack()
    {
        if (isDead || isAnimating) return;
        Player playerScript = player.GetComponent<Player>();
        playerScript.Damage();
        playerScript.health = playerScript.health - damage;
        StartCoroutine(PlayAction("Attack"));
    }

    public void Damage()
    {
        if (isDead || isAnimating) return;
        StartCoroutine(PlayAction("Damage"));
    }

    public void Death()
    {
        if (isDead) return;

        isDead = true;
        StopAllCoroutines();
        StartCoroutine(PlayDeath());
    }

    IEnumerator PlayAction(string triggerName)
    {
        isAnimating = true;

        animator.SetBool("Idle", false);
        animator.SetTrigger(triggerName);

        yield return WaitForAnimation(triggerName);

        if (!isDead)
        {
            animator.SetBool("Idle", true);
        }

        isAnimating = false;
    }

    IEnumerator PlayDeath()
    {
        animator.SetBool("Idle", false);
        animator.SetTrigger("Death");

        yield return WaitForAnimation("Death");
        animator.enabled = false;
        Destroy(this.gameObject);
        EnemySpawner spawnerScript = enemySpawner.GetComponent<EnemySpawner>();
        spawnerScript.spawnEnemy();
    }

    IEnumerator WaitForAnimation(string clipName)
    {
        while (!IsInAnimation(clipName)) yield return null;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(stateInfo.length);
    }

    private bool IsInAnimation(string clipName)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(clipName);
    }
}
