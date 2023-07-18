using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int _playerNumber = 1;
    [SerializeField] Transform _groundCheck;
    [Header("Moving")]
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _slipFactor = 1f;
    [Header("Jumping")]
    [SerializeField] float _jumpSpeed = 5f;
    [SerializeField] float _fallingSpeed = 2f;
    [SerializeField] float _maxJumpDuration = 0.1f;
    [SerializeField] int _maxJumps = 2;
    [SerializeField] Sprite _jumpSprite;
    public int _jumpCount;

    Vector2 _startPosition;
    Rigidbody2D _rb;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    public float _fallTimer = 5;
    float _jumpTimer;
    float _horizontal;
    bool _isGrounded;
    bool _isSlipping;
    string _jumpButton;
    string _horizontalAxis;
    int _groundMask;

    public int PlayerNumber => _playerNumber;

    private void Start()
    {
        _groundMask = LayerMask.GetMask("Ground");
        _horizontalAxis = $"P{_playerNumber}Horizontal";
        _jumpButton = $"P{_playerNumber}Jump";
        _startPosition = transform.position;
        _jumpCount = _maxJumps;
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (_groundCheck.transform.position.y <= -25)
            ResetToStart();

        CheckIfGrounded();

        ReadMoveHorizontal();

        if (_isSlipping)
            SlipHorizontal();
        else
            MoveHorizontal();

        UpdateAnimator();

        FlipSprite();

        if (ShouldJump())
            Jump();
        else if (ShouldContinueJump())
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

    bool ShouldJump()
    {
        return Input.GetButtonDown(_jumpButton) && _jumpCount > 0;
    }

    void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpSpeed);
        _jumpCount--;
        _fallTimer = 0;
        _jumpTimer = 0;
    }

    bool ShouldContinueJump()
    {
        return Input.GetButton(_jumpButton) && _jumpTimer <= _maxJumpDuration;
    }

    void LongJump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, _jumpSpeed);
        _fallTimer = 0;
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
        _horizontal = Input.GetAxisRaw(_horizontalAxis) * _moveSpeed;
    }

    void FlipSprite()
    {
        if (_horizontal != 0)
            _spriteRenderer.flipX = _horizontal < 0;
    }

    void UpdateAnimator()
    {
        bool running = _horizontal != 0;
        _animator.SetBool("Run", running);
        _animator.SetBool("Jump", ShouldContinueJump());
    }

    void CheckIfGrounded()
    {
        var hit = Physics2D.OverlapCircle(_groundCheck.position, 0.1f, _groundMask);
        _isGrounded = hit != null;
        // "hit?" = if hit != null then compare tag ||||||| "??" = otherwise if hit == null then do what is after ??, so = false
        _isSlipping = hit?.CompareTag("Slippery") ?? false;
    }

    internal void ResetToStart()
    {
        _rb.velocity = Vector2.zero;
        _rb.position = _startPosition;
    }

    internal void TeleportTo(Vector3 position)
    {
        _rb.position = position;
        _rb.velocity = Vector2.zero;
    }
}
