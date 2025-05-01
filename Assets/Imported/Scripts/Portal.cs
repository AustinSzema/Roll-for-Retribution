using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, IInteractable
{
    [SerializeField] private MeshRenderer portalMeshRenderer;
    [SerializeField] private MeshRenderer portalBackMeshRenderer;

    [SerializeField] private Material closedBackPortalMaterial;
    [SerializeField] private Material openBackPortalMaterial;

    [SerializeField] private Material closedPortalMaterial;
    [SerializeField] private Material openPortalMaterial;

    [SerializeField] private BoxCollider frameCollider;

    [SerializeField] private ParticleSystem portalParticles;


    [SerializeField] private AudioSource audioSource;

    private bool hasPlayedAudio = false;

    private void Start()
    {
        portalParticles.gameObject.SetActive(false);
    }

    private void Update()
    {
        frameCollider.enabled = !LevelManager.instance.AllCollectiblesCollected();
        if (LevelManager.instance.AllCollectiblesCollected())
        {
            portalMeshRenderer.material = openPortalMaterial;
            portalBackMeshRenderer.material = openBackPortalMaterial;
            portalParticles.gameObject.SetActive(true);
            if (!portalParticles.isPlaying) portalParticles.Play(); // Ensure it plays
            if (!hasPlayedAudio)
            {
                audioSource.Play();
                hasPlayedAudio = true;
            }
        }
        else
        {
            portalMeshRenderer.material = closedPortalMaterial;
            portalBackMeshRenderer.material = closedBackPortalMaterial;
            portalParticles.Stop();  // Stop if collectibles are missing
        }

    }

    public void Interact()
    {
        FindFirstObjectByType<PlayerMovement>(FindObjectsInactive.Include).Freeze();
        LevelManager.instance.NextLevel();
    }
}
