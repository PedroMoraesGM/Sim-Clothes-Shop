using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private GameObject playerVisuals;
    public Animator baseAnimator;
    [SerializeField] private Animator legsAnimator;
    [SerializeField] private Animator shoesAnimator;
    [SerializeField] private Animator shirtAnimator;
    [SerializeField] private Animator hairAnimator;
    [SerializeField] private Animator headAnimator;


    [Header("Editor")]
    [SerializeField] public Sprite[] idleUp;
    [SerializeField] public Sprite[] idleDown;
    [SerializeField] public  Sprite[] idleSide;
    [SerializeField] public Sprite[] walkup;
    [SerializeField] public Sprite[] walkDown;
    [SerializeField] public Sprite[] walkSide;

    private PlayerMoveController moveController;

    void Start()
    {
        moveController = GetComponent<PlayerMoveController>();
    }

    void Update()
    {
        SetAnimationMove(baseAnimator);
        SetAnimationMove(legsAnimator);
        SetAnimationMove(shoesAnimator);
        SetAnimationMove(shirtAnimator);
        SetAnimationMove(hairAnimator);
        SetAnimationMove(headAnimator);

        SetLookSide();
    }

    private void SetAnimationMove(Animator anim)
    {
        // Set animator parameters based on movement input
        float horizontalMovement = moveController.Movement.x;
        float verticalMovement = moveController.Movement.y;

        anim.SetFloat("Horizontal", horizontalMovement);
        anim.SetFloat("Vertical", verticalMovement);
    }

    private void SetLookSide()
    {
        if (moveController.Movement.x > 0)
        {
            playerVisuals.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(moveController.Movement.x < 0)
        {
            playerVisuals.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
