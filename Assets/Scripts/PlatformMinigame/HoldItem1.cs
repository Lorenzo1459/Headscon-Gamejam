using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HoldItem1 : MonoBehaviour
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
            if (!player.isHoldingItem)
            {
                player.HoldItem(); // Call the method to hold the item
            }// Check if the player is not already holding an item and the item hasn't been caught
        }
    }

}
