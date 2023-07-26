using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public static GhostMovement Instance { get; private set; }

    [SerializeField] private Transform target;
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _maximumDistance;
    [SerializeField] private float _minimumDistance;
    [SerializeField] private float _flySpeed = 5f;

    private Animator _animator;
    private bool _isPlayerControlled = true;
    private float _ghostScaleX;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There are more than one GhostMovement");
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameInput.Instance.OnSwitchAction += GameInput_OnSwitchAction;

        _ghostScaleX = transform.localScale.x;
    }

    private void GameInput_OnSwitchAction(object sender, System.EventArgs e)
    {
        _isPlayerControlled = !_isPlayerControlled;
    }

    public void Update()
    {
        if (_isPlayerControlled)
        {
            if (Vector2.Distance(transform.position, target.position) < _maximumDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, _minSpeed * Time.deltaTime);
                _animator.SetBool("isMoving", true);
            }

            if (Vector2.Distance(transform.position, target.position) > _maximumDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, _maxSpeed * Time.deltaTime);
                _animator.SetBool("isMoving", true);
            }

            if (Vector2.Distance(transform.position, target.position) < _minimumDistance)
            {
                _animator.SetBool("isMoving", false);
            }

            if ((target.position - transform.position).x > 0)
            {
                transform.localScale = new Vector3(_ghostScaleX, transform.localScale.y, transform.localScale.z);
            }
            else if ((target.position - transform.position).x < 0)
            {
                transform.localScale = new Vector3(-_ghostScaleX, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            Vector3 movement = GameInput.Instance.GetGhostMovementNormalized();
            transform.position += movement * _flySpeed * Time.deltaTime;

            bool isMoving = !(movement.magnitude == 0);
            _animator.SetBool("isMoving", isMoving);

            if (movement.x > 0)
            {
                transform.localScale = new Vector3(_ghostScaleX, transform.localScale.y, transform.localScale.z);
            }
            else if (movement.x < 0)
            {
                transform.localScale = new Vector3(-_ghostScaleX, transform.localScale.y, transform.localScale.z);
            }
        }


    }

    public bool IsPlayerControlled()
    {
        return _isPlayerControlled;
    }
}
