using UnityEngine;

public class LRMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        body.linearVelocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
        spriteRenderer.flipX = body.linearVelocity.x < 0f;
    }
}