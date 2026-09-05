using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;

public class SnakeHead : MonoBehaviour
{
    private Vector2 startPosition = new Vector2(-0.2f,0);
    private Vector2 direction = Vector2.right;
    private float timer;
    public float tickRate = 0.5f;
    private float step = 0.4f;
    private SpriteRenderer spriteHeadSnake;
    private Vector2 previousPosition;
    [SerializeField] Sprite headSnakeRight;
    [SerializeField] Sprite headSnakeLeft;
    [SerializeField] Sprite headSnakeUp;
    [SerializeField] Sprite headSnakeDown;
    public List<GameObject> snakeBodyList;
    public GameObject snakeBody;
    private bool isDead;
    private bool isStarted;

    void Start()
    {
        isStarted = false;  
        transform.position = startPosition;
        spriteHeadSnake = GetComponent<SpriteRenderer>();
        if (snakeBodyList == null)
            snakeBodyList = new List<GameObject>();
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > 1f)
        {
            isStarted = true;
        }

        if (isStarted == false)
        {
            return;
        }

        if (isDead == true)
        {
            SceneManager.LoadScene("GameOver");
            return;
        }
        PlayerController();
        SnakeMove();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Apple"))
        {
            snakeBodyList.Insert(0, Instantiate(snakeBody, previousPosition, Quaternion.identity));
        }
    }
    void ContinuousWall()
    {
        if (transform.position.x > 4f)
        {
            transform.position = new Vector2(-3.8f, transform.position.y);
        }
        if (transform.position.x < -4f)
        {
            transform.position = new Vector2(3.8f, transform.position.y);
        }
        if (transform.position.y > 3f)
        {
            transform.position = new Vector2(transform.position.x, -2.8f);
        }
        if (transform.position.y < -3f)
        {
            transform.position = new Vector2(transform.position.x, 2.8f);
        }
    }
    void PlayerController()
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
    }
    void SnakeMove()
    {
        timer += Time.deltaTime;
        if (timer >= tickRate)
        {
            previousPosition = transform.position;
            if (snakeBodyList.Count > 0)
            {
                for (int i = snakeBodyList.Count - 1; i > 0; i--)
                {
                    snakeBodyList[i].transform.position = snakeBodyList[i - 1].transform.position;
                }
                snakeBodyList[0].transform.position = previousPosition;
            }
            transform.position = new Vector2(transform.position.x + direction.x * step, transform.position.y + direction.y * step);
            ContinuousWall();
            UpdateBodySprites();
            gameOver();
            timer = 0;
        }
    }
    void UpdateBodySprites()
    {
        for (int i = 0; i < snakeBodyList.Count; i++)
        {
            SnakeBody body = snakeBodyList[i].GetComponent<SnakeBody>();
            Vector2 towardHead = (i == 0)
                ? (Vector2)transform.position
                : (Vector2)snakeBodyList[i - 1].transform.position;

            if (i < snakeBodyList.Count - 1)
            {
                Vector2 towardTail = snakeBodyList[i + 1].transform.position;
                body.UpdateSprite(towardHead, towardTail, true);
            }
            else
            {
                body.UpdateSprite(towardHead, towardHead, false);
            }
        }
    }
    void gameOver()
    {
        for (int i = 0; i < snakeBodyList.Count; i++)
        {
            if (Vector2.Distance(snakeBodyList[i].transform.position, transform.position) < 0.1f)
            {
                isDead = true;
                break;
            }
        }
    }
}