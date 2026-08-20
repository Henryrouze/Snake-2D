using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    private Vector2 startPosition = new Vector2(-0.2f,0);
    private Vector2 direction = Vector2.right;
    private float timer;
    public float tickRate = 0.5f;
    private float step = 0.4f;
    private SpriteRenderer spriteHeadSnake;
    [SerializeField] Sprite headSnakeRight;
    [SerializeField] Sprite headSnakeLeft;
    [SerializeField] Sprite headSnakeUp;
    [SerializeField] Sprite headSnakeDown;

    void Start()
    {
        transform.position = startPosition;
        spriteHeadSnake = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Change the direction of the snake with WASD keys.
        if (Input.GetKeyDown(KeyCode.D) && direction != Vector2.left)
        {
            direction = Vector2.right;
            spriteHeadSnake.sprite = headSnakeRight;
        }
        else if (Input.GetKeyDown(KeyCode.A) && direction != Vector2.right)
        {
            direction = Vector2.left;
            spriteHeadSnake.sprite = headSnakeLeft;
        }
        else if (Input.GetKeyDown(KeyCode.W) && direction != Vector2.down)
        {
            direction = Vector2.up;
            spriteHeadSnake.sprite = headSnakeUp;
        }
        else if (Input.GetKeyDown(KeyCode.S) && direction != Vector2.up)
        {
            direction = Vector2.down;
            spriteHeadSnake.sprite = headSnakeDown;
                // Change direction of the snake with WASD keys.
            }

        // Change direction of the snake with arrow keys.
        if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)
        {
            direction = Vector2.right;
            spriteHeadSnake.sprite = headSnakeRight;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right)
        {
            direction = Vector2.left;
            spriteHeadSnake.sprite = headSnakeLeft;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down)
        {
            direction = Vector2.up;
            spriteHeadSnake.sprite = headSnakeUp;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up)
        {
            direction = Vector2.down;
            spriteHeadSnake.sprite = headSnakeDown;
        }

        // Move the snake.
        timer += Time.deltaTime;
        if (timer >= tickRate)
        {
            transform.position = new Vector2(transform.position.x + direction.x * step, transform.position.y + direction.y * step);
            timer = 0;
        }
    }
}
