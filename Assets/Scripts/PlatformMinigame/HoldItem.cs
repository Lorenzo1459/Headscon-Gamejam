using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HoldItem : MonoBehaviour
{
    Collider2D itemCollider;
    public bool itemCatched = false; // Flag to check if the item is caught
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
            if (!player.isHoldingItem && !itemCatched)
            {
                player.HoldItem(); // Call the method to hold the item
                itemCatched = true; // Set the flag to true when the item is caught
            }// Check if the player is not already holding an item and the item hasn't been caught
        }
    }

}
