using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpSpeed = 0; //player can't jump lol
    
    public bool playerIsGrounded;
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Vector2 groundBoxSize = new Vector2(0.8f, 0.2f);
    
    private InputActions _input;
    private Rigidbody2D _rigidbody2D;
    public PlayerCombat PlayerCombat;

    private void Start()
    {
        _input = GetComponent<InputActions>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    

    private void Update()
    {
        if (Input.GetButtonDown("Attack"))
        {
            Console.WriteLine("Kicking!");
            PlayerCombat.Kick();
        }
        /*  tror ikke jeg trenger noe av dette
         playerIsGrounded = Physics2D.OverlapBox(groundCheck.position, groundBoxSize, 0f, whatIsGround);
        
        if (_input.Jump && playerIsGrounded)
        {
            _rigidbody2D.linearVelocityY = jumpSpeed;
        }
        */
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocityX = _input.Horizontal * moveSpeed;
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(groundCheck.position, groundBoxSize);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Death"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }*/
    
}
