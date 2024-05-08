using System;
using System.Collections;
using UnityEngine;

public class MagicCircleEnclosure : MonoBehaviour
{
    private GameManager _gameManager;

    [SerializeField] private ParticleSystem _expansionParticles;

    [Tooltip("The initial threshold at which the enclosure should deactivate")]
    [SerializeField] private int initialThreshold = 5000;

    private int deactivationThreshold;

    private bool _hasReachedThreshold = false;

    private void Start()
    {
        _gameManager = GameManager.Instance;
        deactivationThreshold = initialThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_hasReachedThreshold && _gameManager.killCount >= deactivationThreshold)
        {
            StartCoroutine(PlayParticlesAndExpand());
            _hasReachedThreshold = true;
        }
    }

    IEnumerator PlayParticlesAndExpand()
    {
        // Play particle system
        _expansionParticles.Play();
        float particleDuration = _expansionParticles.main.duration;
        yield return new WaitForSeconds(particleDuration);

        // Get the main module of the particle system
        var mainModule = _expansionParticles.main;

        // Double the start speed of the particle system
        float originalStartSpeed = mainModule.startSpeed.constant;
        mainModule.startSpeed = originalStartSpeed * 2;

        // Expand the object's size by 2x
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * 2f;
        float duration = 1.0f; // Adjust as needed
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale; // Ensure the final scale is set

        // Double the threshold
        deactivationThreshold *= 2;
    }
}