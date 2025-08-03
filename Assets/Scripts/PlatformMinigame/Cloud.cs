using System.Collections;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class Cloud : MonoBehaviour
{
    private Animator animator;
    private bool isTriggered = false;
    private float cooldown = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Collider2D collider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Entrou" + other.tag);

        if (other.tag == "Player" && !isTriggered)
        {
            StartCoroutine(RespawnCloud());

            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.canJump = true; // Assuming PlayerController has a canClimb variable
            }
        }
    }

    public IEnumerator RespawnCloud()
    {
        isTriggered = true;
        animator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(cooldown);

        animator.SetTrigger("FadeIn");
        isTriggered = false;
    }
}
