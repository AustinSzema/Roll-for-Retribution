using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    [SerializeField] private float _spinSpeed = 100.0f;
    [SerializeField] private float _slowdownRate = 5.0f; // Rate at which the spin slows down
    [SerializeField] private float _minSpeed = 1.0f; // Minimum speed to consider the die stopped
    [SerializeField] private bool _isSpinning = false; // Flag to track if the die is spinning
    [SerializeField] private Rigidbody _rigidbody; // Reference to the Rigidbody component

    private Quaternion _originalRotation;
    private Vector3 _originalAngularVelocity;
    private float _originalSpinSpeed;
    private void Start()
    {
        // Cache the Rigidbody component
        _rigidbody = GetComponent<Rigidbody>();

        // Store the original rotation and angular velocity
        _originalRotation = transform.rotation;
        _originalAngularVelocity = _rigidbody.angularVelocity;
        // Store the original spin speed
        _originalSpinSpeed = _spinSpeed;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_isSpinning)
        {
            
            // Reset the die's rotation and angular velocity to the original values
            transform.rotation = _originalRotation;
            _rigidbody.linearVelocity = Vector3.zero; // Reset velocity as well
            _rigidbody.angularVelocity = _originalAngularVelocity;
            
            // Reset spin speed
            _spinSpeed = _originalSpinSpeed;
            
            // Start spinning
            _isSpinning = true;
            _rigidbody.AddTorque(Random.insideUnitSphere * _spinSpeed);
        }

        if (_isSpinning)
        {
            // Gradually reduce the spinning speed
            _spinSpeed -= _slowdownRate * Time.deltaTime;

            // Check if the die has slowed down enough to consider it stopped
            if (_spinSpeed <= _minSpeed)
            {
                // Stop the die from spinning further
                _isSpinning = false;
                _spinSpeed = 0.0f;

                // Stop the Rigidbody's rotation
                _rigidbody.angularVelocity = Vector3.zero;

                // Align the die to the nearest face
                AlignToNearestFace();
            }
        }
    }

    private void AlignToNearestFace()
    {
        // Define a list of possible face orientations (assuming standard die)
        Quaternion[] faceRotations = new Quaternion[]
        {
            Quaternion.Euler(0, 0, 0),     // Face 1
            Quaternion.Euler(180, 0, 0),   // Face 6
            Quaternion.Euler(0, 90, 0),    // Face 3
            Quaternion.Euler(0, -90, 0),   // Face 4
            Quaternion.Euler(90, 0, 0),    // Face 2
            Quaternion.Euler(-90, 0, 0)    // Face 5
        };

        // Get the current rotation of the die
        Quaternion currentRotation = transform.rotation;

        // Find the closest face rotation
        Quaternion closestRotation = Quaternion.identity;
        float closestAngleDifference = float.MaxValue;
        foreach (Quaternion faceRotation in faceRotations)
        {
            float angleDifference = Quaternion.Angle(currentRotation, faceRotation);
            if (angleDifference < closestAngleDifference)
            {
                closestAngleDifference = angleDifference;
                closestRotation = faceRotation;
            }
        }

        // Set the die's rotation to the closest face rotation
        transform.rotation = closestRotation;
    }
}
