using System.Collections;
using Blyman94.CommonSolutions;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private GameObject _lookLeftObject;
    [SerializeField] private GameObject _lookRightObject;
    [SerializeField] private GameObject _lookForwardObject;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private ApplicationManager _appManager;

    [Header("UI Component References")]
    [SerializeField] private CanvasGroupFader _sceneFader;

    [Header("Data")]
    [SerializeField] private BoolVariable _hasLookedLeft;
    [SerializeField] private BoolVariable _hasLookedRight;
    [SerializeField] private BoolVariable _hasLookedForward;
    [SerializeField] private BoolVariable _hasWalkedForward;

    private void Awake()
    {
        _hasLookedLeft.Value = false;
        _hasLookedRight.Value = false;
        _hasWalkedForward.Value = false;

        _lookRightObject.SetActive(false);
        _lookForwardObject.SetActive(false);
    }

    private IEnumerator Start()
    {
        _playerController.CanMove = false;
        _playerController.CinemachineInputProvider.enabled = false;
        yield return _sceneFader.FadeRoutine(false);
        _playerController.CanMove = true;
        _playerController.CinemachineInputProvider.enabled = true;
    }

    public void FinishTutorial()
    {
        _playerController.CanMove = false;
        StartCoroutine(TutorialFinishedRoutine());
    }

    public IEnumerator TutorialFinishedRoutine()
    {
        yield return _sceneFader.FadeRoutine(true);
        _appManager.LoadSceneSingle(2);
    }
}
