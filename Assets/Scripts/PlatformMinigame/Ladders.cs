using UnityEngine;

public class Ladders : MonoBehaviour
{
    private bool isTriggered = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Entrou" + other.tag);

        if(other.tag == "Player" && !isTriggered){
            isTriggered = true;
            PlayerController player = other.GetComponent<PlayerController>();
            if(player != null){
                player.canJump = true; // Assuming PlayerController has a canClimb variable
                Debug.Log("Player can climb the ladder");
            }
        }
    }void 
    OnTriggerExit2D(Collider2D other){
        Debug.Log("Saiu" + other.tag);
        if(other.tag == "Player") isTriggered = false;
    }
}
