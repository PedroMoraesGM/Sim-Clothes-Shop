using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveController : MonoBehaviour
{
    private PlayerControls controlsInput;

    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public Vector2 Movement { get => movement; }

    void Start()
    {
        controlsInput = new PlayerControls();
        controlsInput.Enable();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Input
        Vector2 move = new Vector2(0, 0);
        float horizontal = controlsInput.Gameplay.Horizontal.ReadValue<float>();
        float vertical = controlsInput.Gameplay.Vertical.ReadValue<float>();

        if (horizontal != 0)
        {
            move.x = horizontal;
        }
        else if(vertical != 0)
        {
            move.y = vertical;
        }
        movement = move;
    }

    void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
