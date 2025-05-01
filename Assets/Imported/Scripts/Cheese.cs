using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Cheese : MonoBehaviour
{
    [SerializeField] private intVariable _cheeseCount;


    
    [SerializeField] float rotationSpeed = 30.0f; // Adjust the speed as needed

    [SerializeField] private TextMeshProUGUI _cheeseCountText;
    [SerializeField] private Collider _collider;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private ParticleSystem _particles;
    [SerializeField] private AudioClip _clip;

    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = FindObjectOfType<AudioSource>();
        _cheeseCount.Value = 0;
    }

    private void Update()
    {
        // Rotate the object around the Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        _cheeseCountText.text = "" + _cheeseCount.Value;
        if (_cheeseCount.Value >= 6)
        {
            WinGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cheeseCount.Value++;
            _collider.enabled = false;
            _meshRenderer.enabled = false;

            _particles.Play();
            _audioSource.PlayOneShot(_clip);
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cheeseCount.Value++;
            _collider.enabled = false;
            _meshRenderer.enabled = false;

            _particles.Play();
            _audioSource.PlayOneShot(_clip);
            
        }
    }
    
    private void WinGame()
    {
        SceneManager.LoadScene("Win Screen");
    }
}
