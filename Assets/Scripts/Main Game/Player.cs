using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isDead = false;
    private bool isAnimating = false;
    private float temphealth;

    public Sprite death;
    public float health = 10f;
    public Slider healthbar;

    public AudioClip attack1;
    public AudioClip attack2;
    public AudioClip attack3;
    public AudioClip damage;
    public AudioClip lose;
    public AudioSource sound;
    public AudioSource music;

    void Start()
    {
        temphealth = health;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        animator.SetBool("Idle", true);
    }

    void Update()
    {
        healthbar.value = health / temphealth;
        if (!isDead && health <= 0)
        {
            Death();
        }
    }

    //Anything below is just animation stuff
    public void Attack()
    {
        if (isDead || isAnimating) return;
        StartCoroutine(PlayAction("Attack"));
        int a = UnityEngine.Random.Range(0, 3);
        if (a == 0) sound.clip = attack1;
        else if (a == 1) sound.clip = attack2;
        else if (a == 2) sound.clip = attack3;
        sound.pitch = UnityEngine.Random.Range(0.74f, 1.26f);
        sound.Play();
    }

    public void Damage()
    {
        if (isDead || isAnimating) return;
        StartCoroutine(PlayAction("Damage"));
        sound.clip = damage;
        sound.pitch = UnityEngine.Random.Range(0.74f, 1.26f);
        sound.Play();
    }

    public void Death()
    {
        if (isDead) return;
        sound.clip = lose;
        sound.pitch = UnityEngine.Random.Range(0.5f, 1f);
        sound.Play();
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
        music.volume = 0f;
        animator.SetBool("Idle", false);
        animator.SetTrigger("Death");

        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(3);

        animator.enabled = false;
        spriteRenderer.sprite = death;
        SceneManager.LoadScene("Main Menu");
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