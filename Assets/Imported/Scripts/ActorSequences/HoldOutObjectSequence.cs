using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HoldOutObjectSequence : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private Animator _actorAnimator;
    [SerializeField] private GameObject _objectToHold;

    [Header("Animation Parameters")]
    [SerializeField] private float _lowerHandDuration = 1.0f;

    [Header("Events")]
    [SerializeField] private UnityEvent _onHandLowered;

    public void Begin()
    {
        _objectToHold.SetActive(true);
        _actorAnimator.SetTrigger("ExtendHand");
    }

    public void LowerHand()
    {
        StartCoroutine(LowerHandRoutine());
    }

    private IEnumerator LowerHandRoutine()
    {
        _actorAnimator.SetTrigger("LowerHand");
        yield return new WaitForSeconds(_lowerHandDuration);
        _onHandLowered?.Invoke();
    }
}
