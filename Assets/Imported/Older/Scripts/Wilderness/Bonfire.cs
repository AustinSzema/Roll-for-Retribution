using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BonfireAudioRevealSettings
{
    public string Name;
    public float Volume;
    public float MaxDistance;
    public float Scale;
    public float BonfireAudioChangeTime = 1.0f;
}

public class Bonfire : MonoBehaviour
{
    [SerializeField] private GameEvent _logAddSuccessEvent;
    [SerializeField] private GameEvent _logAddFailedEvent;
    [SerializeField] private BoolVariable _playerHasLogVariable;
    [SerializeField] private AudioSource _ambientBonfireNoiseSource;
    [SerializeField] private AudioSource _impactBonfireNoiseSource;
    [SerializeField] private AudioClip _fireImpactClip;
    [SerializeField] private List<BonfireAudioRevealSettings> _bonfireLevelSettings;
    [SerializeField] private Transform _flameTransform;

    private int _levelIndex = 0;
    private float _elapsedTime = 0.0f;

    private void Start()
    {
        _levelIndex = 0;
        BonfireAudioRevealSettings newSettings = 
            _bonfireLevelSettings[_levelIndex];

        _flameTransform.localScale = Vector3.one * newSettings.Scale;
        _ambientBonfireNoiseSource.volume = newSettings.Volume;
        _ambientBonfireNoiseSource.maxDistance = newSettings.MaxDistance;
    }

    public void TryAddLog()
    {
        if (_playerHasLogVariable.Value)
        {
            _playerHasLogVariable.Value = false;
            _impactBonfireNoiseSource.PlayOneShot(_fireImpactClip);
            IncreaseLightLevel();
            _logAddSuccessEvent.Raise();
        }
        else
        {
            _logAddFailedEvent.Raise();
        }
    }

    public void IncreaseLightLevel()
    {
        if (_levelIndex < _bonfireLevelSettings.Count)
        {
            _levelIndex++;
            StartCoroutine(ChangeBonfireAudioRoutine(_levelIndex));
        }
    }

    private IEnumerator ChangeBonfireAudioRoutine(int levelIndex)
    {
        BonfireAudioRevealSettings newSettings = _bonfireLevelSettings[levelIndex];
        _elapsedTime = 0.0f;

        Vector3 bonfireStartScale = _flameTransform.localScale;
        float bonfireStartVolume = _ambientBonfireNoiseSource.volume;
        float bonfireStartDistance = _ambientBonfireNoiseSource.maxDistance;

        while (_elapsedTime < newSettings.BonfireAudioChangeTime)
        {
            float lerpPercentage = _elapsedTime / newSettings.BonfireAudioChangeTime;

            _flameTransform.localScale = Vector3.Lerp(bonfireStartScale, Vector3.one * newSettings.Scale, lerpPercentage);

            _ambientBonfireNoiseSource.volume =
                Mathf.Lerp(bonfireStartVolume, newSettings.Volume,
                lerpPercentage);

            _ambientBonfireNoiseSource.maxDistance =
                Mathf.Lerp(bonfireStartDistance, newSettings.MaxDistance,
                lerpPercentage);

            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        _flameTransform.localScale = Vector3.one * newSettings.Scale;
        _ambientBonfireNoiseSource.volume = newSettings.Volume;
        _ambientBonfireNoiseSource.maxDistance = newSettings.MaxDistance;

    }
}
