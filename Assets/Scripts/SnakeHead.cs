using UnityEngine;

public class SnakeHead : MonoBehaviour
{
    private Vector2 startPosition = new Vector2(-0.2f,0);
    private Vector2 direction = Vector2.right;
    private float timer;
    private float step = 0.4f;

    void Start()
    {
        transform.position = startPosition;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            transform.position = new Vector2(transform.position.x + direction.x * step, transform.position.y);
            timer = 0;
        }
    }
}
