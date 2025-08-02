using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public float speed;
    public bool movingRight = true;


    public GameObject startPoint;
    public GameObject endPoint;
    private Vector2 startPosition;
    private Vector2 endPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = startPoint.transform.position;
        endPosition = endPoint.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
            transform.localScale = new Vector3(1, 1, 1); 

            if (Vector2.Distance(transform.position, endPosition) < 0.1f)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.position = Vector2.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, startPosition) < 0.1f)
            {
                movingRight = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().PlayerDeath();
        }
    }
}
