using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 8f;            // units per second
    [SerializeField] float minX = -9.75f;
    [SerializeField] float maxX =  9.75f;

    [Header("References")]
    public Animator animator;                     // optional: auto-filled in Awake
    private Rigidbody2D rb;
    private SoundManager SoundManager;

    private Vector2 targetPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
    }

    void Start()
    {
        targetPos = rb.position;

        // Find the audio manager by tag "Music" (change if your tag differs)
        GameObject musicGO = GameObject.FindGameObjectWithTag("Music");
        if (musicGO != null)
            SoundManager = musicGO.GetComponent<SoundManager>();
        else
            Debug.LogWarning("Move: No GameObject with tag 'music' found.");
    }

    void Update()
    {
        // Gather horizontal input from arrow keys only (keep your original intent)
        float input = 0f;
        if (Input.GetKey(KeyCode.RightArrow)) input = 1f;
        else if (Input.GetKey(KeyCode.LeftArrow)) input = -1f;

        bool isMoving = Mathf.Abs(input) > 0.01f;

        // Set animator only if present
        if (animator != null) animator.SetBool("isRunning", isMoving);

        // Compute the new x (clamped to bounds)
        if (isMoving)
        {
            float nextX = Mathf.Clamp(
                targetPos.x + input * speed * Time.deltaTime,
                minX, maxX
            );
            targetPos.x = nextX;
        }
    }

    void FixedUpdate()
    {
        // Apply movement in physics step
        rb.MovePosition(targetPos);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            Debug.Log("Stumble Sound");
            if (SoundManager != null)
                SoundManager.PlaySFX(SoundManager.stumbleSound);
        }
    }
}
