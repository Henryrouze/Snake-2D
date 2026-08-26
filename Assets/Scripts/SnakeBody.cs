using UnityEngine;

public class SnakeBody : MonoBehaviour
{
    private SpriteRenderer spriteBody;
    [SerializeField] Sprite bodyVertical;
    [SerializeField] Sprite bodyHorizontal;
    [SerializeField] Sprite bodyCornerTopLeft;
    [SerializeField] Sprite bodyCornerTopRight;
    [SerializeField] Sprite bodyCornerBottomLeft;
    [SerializeField] Sprite bodyCornerBottomRight;
    void Start()
    {
        spriteBody = GetComponent<SpriteRenderer>();
        spriteBody.sprite = bodyHorizontal;
    }

    public void UpdateSprite(Vector2 towardHead, Vector2 towardTail, bool hasTailNeighbor)
    {
        if (spriteBody == null)
            spriteBody = GetComponent<SpriteRenderer>();

        if (!hasTailNeighbor)
        {
            if (Mathf.Abs(towardHead.y - transform.position.y) < 0.1f)
                spriteBody.sprite = bodyHorizontal;
            else
                spriteBody.sprite = bodyVertical;
            return;
        }

        bool left = IsLeft(towardHead) || IsLeft(towardTail);
        bool right = IsRight(towardHead) || IsRight(towardTail);
        bool up = IsUp(towardHead) || IsUp(towardTail);
        bool down = IsDown(towardHead) || IsDown(towardTail);

        if (left && right)
            spriteBody.sprite = bodyHorizontal;
        else if (up && down)
            spriteBody.sprite = bodyVertical;
        else if (left && up)
            spriteBody.sprite = bodyCornerTopLeft;
        else if (right && up)
            spriteBody.sprite = bodyCornerTopRight;
        else if (left && down)
            spriteBody.sprite = bodyCornerBottomLeft;
        else if (right && down)
            spriteBody.sprite = bodyCornerBottomRight;
    }

    bool IsLeft(Vector2 neighbor)
    {
        return Mathf.Abs(neighbor.y - transform.position.y) < 0.1f && neighbor.x < transform.position.x;
    }

    bool IsRight(Vector2 neighbor)
    {
        return Mathf.Abs(neighbor.y - transform.position.y) < 0.1f && neighbor.x > transform.position.x;
    }

    bool IsUp(Vector2 neighbor)
    {
        return Mathf.Abs(neighbor.x - transform.position.x) < 0.1f && neighbor.y > transform.position.y;
    }

    bool IsDown(Vector2 neighbor)
    {
        return Mathf.Abs(neighbor.x - transform.position.x) < 0.1f && neighbor.y < transform.position.y;
    }
}
