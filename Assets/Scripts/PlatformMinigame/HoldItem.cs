using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HoldItem : MonoBehaviour
{
    Collider2D itemCollider;
    void Start()
    {
        itemCollider = GetComponent<Collider2D>();
        itemCollider.isTrigger = true; // Ensure the item collider is a trigger
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            player.HoldItem();
        }
    }

}
