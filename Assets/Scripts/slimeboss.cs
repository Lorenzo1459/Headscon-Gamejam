using UnityEngine;

public class slimeboss : MonoBehaviour
{
    public int enemiesKilled = 0;
    public int required = 8; // Variable to track the number of enemies killed
    public GameObject door; // Reference to the door GameObject
    EnemyMove enemyMove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Animator animator;
    void Start()
    {
        enemyMove = GetComponent<EnemyMove>();
        animator = GetComponent<Animator>();
    }
    public void callAnimator()
    {
        enemiesKilled++; // Increment the number of enemies killed
        animator.SetTrigger("win");

        if (enemiesKilled >= required)
        {
            animator.SetTrigger("end");
            enemyMove.StopMovementt(); // Stop the enemy movement
            this.gameObject.GetComponent<Collider2D>().enabled = false;
            door.SetActive(true); // Activate the door when enough enemies are killed
             // Disable the collider to prevent further interactions
        }
    }

}
