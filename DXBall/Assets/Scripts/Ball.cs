using UnityEngine;

public class ball : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public Vector2 direction;
    public int brickCount = 0;
    public ScoreManager score;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Vector2.one.normalized; //(1,1)
        score = GameObject.FindGameObjectWithTag("Logic").GetComponent<ScoreManager>();
    }

    void Update()
    {
        rb.linearVelocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            direction.y = -direction.y;
        }
        else if (collision.gameObject.CompareTag("Brick") || collision.gameObject.CompareTag("Permanent Brick"))
        {
            // Determine which side of the brick was hit
            Vector2 contactPoint = transform.position - collision.transform.position;

            if (Mathf.Abs(contactPoint.x) > Mathf.Abs(contactPoint.y))
            {
                // Left/right hit
                direction.x = -direction.x;
            }
            else
            {
                // Top/bottom hit
                direction.y = -direction.y;
            }

            if (collision.gameObject.CompareTag("Brick"))
            {
                // Destroy and score for normal bricks
                Destroy(collision.gameObject);
                brickCount++;
                Debug.Log("Brick count: " + brickCount);
                score.addScore(1);
            }
            else
            {
                // Permanent brick → just bounce
                Debug.Log("Hit a Permanent Brick!");
            }
        }
        else if (collision.gameObject.CompareTag("Side Wall"))
        {
            direction.x = -direction.x;
        }
        else if (collision.gameObject.CompareTag("Top Wall"))
        {
            direction.y = -direction.y;
        }
        else if (collision.gameObject.CompareTag("Bottom Wall"))
        {
            Debug.Log("Game over");
            gameObject.SetActive(false);
            score.addScore(0);
        }
    }
}
