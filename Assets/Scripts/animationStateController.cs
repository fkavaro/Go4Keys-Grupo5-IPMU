using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;
    int isJumpingHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    // Update is called once per frame
    void Update()
    {
        bool isJumping = animator.GetBool(isJumpingHash);
        bool jumpPressed = Input.GetKey(KeyCode.Space);
        // if player pressed space key
        if (!isJumping && jumpPressed)
        {
            // then set the isJumping boolean to be true
            animator.SetBool(isJumpingHash, true);
        }

        // if player is not pressing space key
        if (isJumping && !jumpPressed)
        {
            // then set the isJumping boolean to be false
            animator.SetBool(isJumpingHash, false);
        }
    }
}
