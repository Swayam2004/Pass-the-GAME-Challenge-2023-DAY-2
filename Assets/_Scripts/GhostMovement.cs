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
        }
        else
        {
            Vector3 movement = GameInput.Instance.GetGhostMovementNormalized();
            transform.position += movement * _flySpeed * Time.deltaTime;

            bool isMoving = !(movement.magnitude == 0);
            _animator.SetBool("isMoving", isMoving);
        }
    }

    public bool IsPlayerControlled()
    {
        return _isPlayerControlled;
    }
}
