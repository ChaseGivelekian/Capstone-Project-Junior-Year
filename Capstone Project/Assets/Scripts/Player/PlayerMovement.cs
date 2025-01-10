using Core;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int Grounded = Animator.StringToHash("grounded");

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime; //How much time the player can hang in the air before jumping
    private float _coyoteCounter; //How much time passed since the player ran off the edge

    [Header("Multiple Jumps")]
    [SerializeField] private int extraJumps;
    private int _jumpCounter;

    [Header("Wall Jumping")]
    [SerializeField] private float wallJumpX; //Horizontal wall jump force
    [SerializeField] private float wallJumpY; //Vertical wall jump force

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;

    private Rigidbody2D _body;
    private Animator _anim;
    private BoxCollider2D _boxCollider;
    // private float wallJumpCooldown;
    private float _horizontalInput;

    private void Awake()
    {
        //Gets references for rigidbody and animator objects
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {

        _horizontalInput = Input.GetAxis("Horizontal");

        transform.localScale = _horizontalInput switch
        {
            // Flip player when moving a different direction
            > .01f => new Vector3(1.2f, 1.2f, 1.2f),
            < -.01f => new Vector3(-1.2f, 1.2f, 1.2f),
            _ => transform.localScale
        };

        //Set animator parameters
        _anim.SetBool(Run, _horizontalInput != 0);
        _anim.SetBool(Grounded, IsGrounded());

        //Jump 
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        //Adjustable jump height
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) && _body.velocity.y > 0)
        {
            _body.velocity = new Vector2(_body.velocity.x, _body.velocity.y / 2);
        }

        if (OnWall())
        {
            _body.gravityScale = 0;
            _body.velocity = Vector2.zero;
        }
        else
        {
            _body.gravityScale = 7;
            _body.velocity = new Vector2(_horizontalInput * speed, _body.velocity.y);

            if (IsGrounded())
            {
                _coyoteCounter = coyoteTime; //Reset coyote counter when on the ground
                _jumpCounter = extraJumps; //Reset jump counter to extra jump value
            }
            else
            {
                _coyoteCounter -= Time.deltaTime; //Start decreasing coyote counter when not on the ground
            }
        }
    }
    private void Jump()
    {
        //If coyote counter is 0 or less and not on the wall and don't have any extra jumps don't do anything
        if (_coyoteCounter < 0 && !OnWall() && _jumpCounter <= 0) return;

        SoundManager.Instance.PlaySound(jumpSound);

        if (OnWall())
        {
            WallJump();
        }
        else
        {
            if (IsGrounded())
            {
                _body.velocity = new Vector2(_body.velocity.x, jumpPower);
            }
            else
            {
                //If not on the ground and coyote counter bigger than 0 do a normal jump
                if (_coyoteCounter > 0)
                {
                    _body.velocity = new Vector2(_body.velocity.x, jumpPower);
                }
                else
                {
                    if (_jumpCounter > 0) //If we have extra jumps then jump and decrease the jump counter
                    {
                        _body.velocity = new Vector2(_body.velocity.x, jumpPower);
                        _jumpCounter--;
                    }
                }
            }

            //Reset coyote counter to 0 to avoid double jumps
            _coyoteCounter = 0;
        }
    }
    private void WallJump()
    {
        _body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
        // wallJumpCooldown = 0;
    }

    private bool IsGrounded()
    {
        var raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider;
    }
    private bool OnWall()
    {
        var raycastHit = Physics2D.BoxCast(_boxCollider.bounds.center, _boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider;
    }
    public bool CanAttack()
    {
        return _horizontalInput == 0 && !OnWall();
    }
}
