using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform _groundCheck;
    [Header("Moving")]
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _slipFactor = 1f;
    [Header("Jumping")]
    [SerializeField] float _jumpSpeed = 5f;
    [SerializeField] float _fallingSpeed = 2f;
    [SerializeField] float _maxJumpDuration = 0.1f;
    [SerializeField] int _maxJumps = 2;

    Vector2 _startPosition;
    Rigidbody2D _rb;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    int _jumpCount;
    float _fallTimer = 5;
    float _jumpTimer;
    float _horizontal;
    bool _isGrounded;
    bool _isSlipping;

    private void Start()
    {
        _startPosition = transform.position;
        _jumpCount = _maxJumps;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        CheckIfGrounded();

        ReadMoveHorizontal();

        if(_isSlipping)
            SlipHorizontal();
        else
            MoveHorizontal();

        AnimateRunning();

        FlipSprite();

        if (ShouldJump())
            Jump();
        else if (ShouldLongJump())
            LongJump();

        _jumpTimer += Time.deltaTime;

        if (_isGrounded && _fallTimer > 0 && _rb.velocity.y == 0)
        {
            _fallTimer = 0;
            _jumpCount = _maxJumps;
        }
        else if (!_isGrounded)
        {
            _fallTimer += Time.deltaTime;
            var downForce = _fallingSpeed * _fallTimer * _fallTimer;
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y - downForce);
        }
    }

    void LongJump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpSpeed);
        _fallTimer = 0;
    }

    bool ShouldLongJump()
    {
        return Input.GetButton("Jump") && _jumpTimer <= _maxJumpDuration;
    }

    void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpSpeed);
        _jumpCount--;
        _fallTimer = 0;
        _jumpTimer = 0;
    }

    bool ShouldJump()
    {
        return Input.GetButtonDown("Jump") && _jumpCount > 0;
    }

    void SlipHorizontal()
    {
        var desiredVelocity = new Vector2(_horizontal * _moveSpeed / 2, _rb.velocity.y);
        var smoothedVelocity = Vector2.Lerp(_rb.velocity, desiredVelocity, Time.deltaTime / _slipFactor);
        _rb.velocity = smoothedVelocity;
    }

    void MoveHorizontal()
    {
        _rb.velocity = new Vector2(_horizontal, _rb.velocity.y);
    }

    void ReadMoveHorizontal()
    {
        _horizontal = Input.GetAxisRaw("Horizontal") * _moveSpeed;
    }

    void FlipSprite()
    {
        if (_horizontal != 0)
            _spriteRenderer.flipX = _horizontal < 0;
    }

    void AnimateRunning()
    {
        bool running = _horizontal != 0;
        _animator.SetBool("Run", running);
    }

    void CheckIfGrounded()
    {        
        var hit = Physics2D.OverlapCircle(_groundCheck.position, 0.1f, LayerMask.GetMask("Ground"));
        _isGrounded = hit != null;
        // "hit?" = if hit != null then compare tag ||||||| "??" = otherwise if hit == null then do what is after ??, so = false
        _isSlipping = hit?.CompareTag("Slippery") ?? false; 
    }

    internal void ResetToStart()
    {
        transform.position = _startPosition;
    }
}
