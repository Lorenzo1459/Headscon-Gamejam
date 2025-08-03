using UnityEngine;

public class Bee : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Collider2D itemCollider;
    Animator animator;
    EnemyMove enemyMove;
    void Start()
    {
        itemCollider = GetComponent<Collider2D>();
        enemyMove = GetComponent<EnemyMove>();
        animator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("win");
            enemyMove.StopMovementt(); // Stop the enemy movement
        }
    }
}
