using UnityEngine;

public class Attacking : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameInput.Instance.OnGhostFireAction += GameInput_OnGhostFireAction;
    }

    private void GameInput_OnGhostFireAction(object sender, System.EventArgs e)
    {
        if (!GhostMovement.Instance.IsPlayerControlled())
        {
            _animator.SetBool("isAttacking", true);
            Invoke(nameof(StopAttacking), 1f);
        }
    }

    private void StopAttacking()
    {
        _animator.SetBool("isAttacking", false);
    }
}
