using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LightRevealSettings
{
    public string Name;
    public Vector3 Position;
    public Color Color = Color.white;
    public float Intensity;
    public float Range;
    public float LightChangeTime = 1.0f;
}

public class LevelRevealLight : MonoBehaviour
{
    [SerializeField] private Light _bonfireLight;
    [SerializeField] private List<LightRevealSettings> _lightLevelSettings;

    private int _levelIndex = 0;
    private float _elapsedTime = 0.0f;

    private void Start()
    {
        _levelIndex = 0;
        LightRevealSettings newSettings = _lightLevelSettings[_levelIndex];
        
        _bonfireLight.transform.localPosition = newSettings.Position;
        _bonfireLight.color = newSettings.Color;
        _bonfireLight.intensity = newSettings.Intensity;
        _bonfireLight.range = newSettings.Range;
    }

    public void IncreaseLightLevel()
    {
        if (_levelIndex < _lightLevelSettings.Count)
        {
            _levelIndex++;
            StartCoroutine(ChangeLightRoutine(_levelIndex));
        }
    }

    private IEnumerator ChangeLightRoutine(int levelIndex)
    {
        LightRevealSettings newSettings = _lightLevelSettings[levelIndex];
        _elapsedTime = 0.0f;

        Vector3 lightStartPos = _bonfireLight.transform.localPosition;
        Color lightStartColor = _bonfireLight.color;
        float lightStartIntensity = _bonfireLight.intensity;
        float lightStartRange = _bonfireLight.range;

        while (_elapsedTime < newSettings.LightChangeTime)
        {
            float lerpPercentage = _elapsedTime / newSettings.LightChangeTime;

            _bonfireLight.transform.localPosition =
                Vector3.Lerp(lightStartPos, newSettings.Position,
                (_elapsedTime / newSettings.LightChangeTime));

            _bonfireLight.color =
                Color.Lerp(_bonfireLight.color, newSettings.Color,
                lerpPercentage);

            _bonfireLight.intensity =
                Mathf.Lerp(_bonfireLight.intensity, newSettings.Intensity,
                lerpPercentage);

            _bonfireLight.range =
                Mathf.Lerp(_bonfireLight.intensity, newSettings.Range,
                lerpPercentage);

            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        _bonfireLight.transform.localPosition = newSettings.Position;
        _bonfireLight.color = newSettings.Color;
        _bonfireLight.intensity = newSettings.Intensity;
        _bonfireLight.range = newSettings.Range;

    }
}
