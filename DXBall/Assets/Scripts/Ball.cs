using UnityEngine;

public class ball : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public Vector2 direction;
    public int brickCount = 0;
    public ScoreManager score;
    public static bool stopAllBalls = false; // shared flag

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.freezeRotation = true;
        direction = Vector2.one.normalized; //(1,1)
        score = GameObject.FindGameObjectWithTag("Logic").GetComponent<ScoreManager>();
    }

    void Update()
    {
        if (!stopAllBalls)
        {
            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (stopAllBalls) return; // don’t process further collisions after game over

        if (collision.gameObject.CompareTag("Paddle"))
        {
            direction.y = -direction.y;
        }
        else if (collision.gameObject.CompareTag("Brick") || collision.gameObject.CompareTag("Permanent Brick"))
        {
            Vector2 contactPoint = transform.position - collision.transform.position;

            if (Mathf.Abs(contactPoint.x) > Mathf.Abs(contactPoint.y))
                direction.x = -direction.x;
            else
                direction.y = -direction.y;

            if (collision.gameObject.CompareTag("Brick"))
            {
                Destroy(collision.gameObject);
                brickCount++;
                score.addScore(1);
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

            // freeze and hide ALL balls
            stopAllBalls = true;

            GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
            foreach (GameObject b in balls)
            {
                b.SetActive(false);
            }

            score.addScore(0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (stopAllBalls) return;

        if (collision.gameObject.CompareTag("Ball"))
        {
            Vector2 normal = collision.contacts[0].normal;
            direction = Vector2.Reflect(direction, normal).normalized;
            rb.linearVelocity = direction * speed;
        }
    }
}
