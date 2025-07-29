using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f; // Ajuste a força do pulo conforme necessário
    bool canJump = true;
    float horizontalInput;
    // O footCollider não é mais usado diretamente para a detecção de chão com OverlapCircle,
    // mas pode ser útil para outras colisões ou visualização.
    // Se você não o usa para mais nada, pode removê-lo.
    // public Collider2D footCollider; 

    [Header("Ground Check Settings")]
    public LayerMask groundLayer; // Continua sendo importante para filtrar o que é "chão"
    public Transform groundCheckPoint; // Ponto de onde o OverlapCircle será lançado
    private bool isGrounded;
    float groundCheckRadius = 0.2f; // Raio do OverlapCircle

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);

        // Realiza a verificação de chão usando Physics2D.OverlapCircle
        // O OverlapCircle retorna um Collider2D se algo for detectado no círculo
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        if (isGrounded && rb.linearVelocity.y <= 0) canJump = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        horizontalInput = context.ReadValue<Vector2>().x;
        if (horizontalInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Face right
        }
        else if (horizontalInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Face left
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump)
        {
            canJump = false;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // Adjust the jump force as needed
        }
        else if( context)
    }
}