using UnityEngine;

public class Honey : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int collectedHoney = 0; // Variable to track collected honey
    public int requiredHoney = 4; // Number of honey pieces required to open the door
    public GameObject door; // Reference to the honey prefab

    // Update is called once per fram

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            if (player.isHoldingItem)
            {
                player.StopHoldingItem(); // Stop holding item if any
                collectedHoney++; // Increment the honey count when the player collects it
            }

        }

        OpenDoor(); // Check if the door should be opened
    }

    void OpenDoor()
    {
        if (collectedHoney >= requiredHoney)
        {
            door.SetActive(true); // Open the door when enough honey is collected
        }
    }

}
