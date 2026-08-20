using UnityEngine;

public class Apple : MonoBehaviour
{
    private int gx;
    private int gy;
    private float x;
    private float y;
    public GameObject applePrefab;
    void Start()
    {
        Randomizer();
        transform.position = new Vector2(x, y);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InstantiateApple();
            Destroy(gameObject);
            
        }
    }
    void Randomizer()
    {
        gx = Random.Range(-9, 11);
        gy = Random.Range(-7, 8);
        x = -0.2f + 0.4f * gx;
        y = 0 + 0.4f * gy;
    }
    void InstantiateApple()
    {
        Instantiate(applePrefab);
    }
}
