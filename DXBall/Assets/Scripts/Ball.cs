using UnityEngine;
public class ball : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    public float speed;
    public Vector2 direction;
    public int brickCount = 0;
    public ScoreManager score;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
        direction= Vector2.one.normalized; //(1,1)
        score = GameObject.FindGameObjectWithTag("Logic").GetComponent<ScoreManager>();

    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity= direction * speed;
    }

    void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.gameObject.CompareTag("Paddle"))
            direction.y = -direction.y;

        else if (collison.gameObject.CompareTag("Brick"))
        {
            direction.y = -direction.y;
            Destroy(collison.gameObject);
            brickCount++;
            Debug.Log("Brick count: " + brickCount);
            score.addScore(1);  // when ball hits brick, score + 1
        }

        else if (collison.gameObject.CompareTag("Side Wall"))
            direction.x = -direction.x;

        else if (collison.gameObject.CompareTag("Top Wall"))
            direction.y = -direction.y;

        else if (collison.gameObject.CompareTag("Bottom Wall"))
        {
            Debug.Log("Game over");
            gameObject.SetActive(false);
            score.addScore(0);  // when ball hits bottom floor, score = 0
        }
    }
}