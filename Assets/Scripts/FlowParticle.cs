using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowParticle : MonoBehaviour
{
    [SerializeField] private Material _flowParticleMaterial;

    [SerializeField] private MeshRenderer _flowParticleMeshRenderer;

    private Material _materialInstance;

    [SerializeField] private Rigidbody _rb;

    [SerializeField] private float _power = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        _materialInstance = Instantiate(_flowParticleMaterial);
        _flowParticleMeshRenderer.material = _materialInstance;
    }

    private float speed;

    private float power;

    private float scale;

    private float density;
    
    
    // Define the original range
    private float originalMin = -Mathf.Infinity;
    private float originalMax = Mathf.Infinity;

    // Define the clamped range
    private float clampedMin = 0.0f;
    private float clampedMax = 255.0f;

    
    // Update is called once per frame
    void Update()
    {
        speed = Mathf.Clamp(_rb.linearVelocity.magnitude, 0.0f, 255.0f);
        
        power = Mathf.Clamp(transform.position.magnitude, 0.0f, 255.0f);

        scale = Mathf.Clamp(transform.localScale.magnitude, 0.0f, 255.0f);

                
        /*speed = MapValue(_rb.velocity.magnitude, originalMin, originalMax, clampedMin, clampedMax);
        power = MapValue(transform.position.magnitude, originalMin, originalMax, clampedMin, clampedMax);
        scale = MapValue(transform.localScale.magnitude, originalMin, originalMax, clampedMin, clampedMax);*/
        
        density = 1f;
        
        _materialInstance.color = new Vector4(speed, power, scale, density);

        _materialInstance.color = new Vector4(transform.position.x, transform.position.y, transform.position.z, 1f);

    }
    
    // Function to map a value from one range to another
    float MapValue(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        // Map the value to a normalized range (between 0 and 1)
        float normalizedValue = Mathf.InverseLerp(fromMin, fromMax, value);
        
        // Map the normalized value to the clamped range
        float mappedValue = Mathf.Lerp(toMin, toMax, normalizedValue);
        
        // Clamp the mapped value to ensure it falls within the clamped range
        mappedValue = Mathf.Clamp(mappedValue, Mathf.Min(toMin, toMax), Mathf.Max(toMin, toMax));
        
        return mappedValue;
    }
}
