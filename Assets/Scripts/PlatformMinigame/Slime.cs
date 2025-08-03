using UnityEngine;

public class Slime : MonoBehaviour
{
    Animator animator;
    EnemyMove enemyMove;
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyMove = GetComponent<EnemyMove>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player.isHoldingItem)
            {
                enemyMove.StopMovementt(); // Stop the enemy movement
                player.StopHoldingItem();
                FindFirstObjectByType<slimeboss>().callAnimator(); // Call the slime boss animator to trigger the win animation
                animator.SetTrigger("win"); // Trigger the win animation
                this.gameObject.SetActive(false); // Deactivate the slime after the player wins
            }
            else
            {
                player.PlayerDeath(); // If the player is not holding an item, they die
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().PlayerDeath();
        }
    }
}
