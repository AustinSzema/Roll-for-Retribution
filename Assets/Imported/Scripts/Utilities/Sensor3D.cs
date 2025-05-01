using UnityEngine;

/// <summary>
/// Enum to determine the shape of the sensor.
/// </summary>
public enum SensorShape { Box, Sphere }

/// <summary>
/// Delegate to signal that the state of a sensor has changed.
/// </summary>
public delegate void SensorStateChanged();

public class Sensor3D : MonoBehaviour
{
    /// <summary>
    /// Signals the sensor's active state has changed.
    /// </summary>
    public SensorStateChanged SensorStateChanged;

    /// <summary>
    /// Primitive shape of the sensor.
    /// </summary>
    [Tooltip("Primitive shape of the sensor.")]
    public SensorShape SensorShape;

    /// <summary>
    /// Should this sensor only sense a specific tag?
    /// </summary>
    [Tooltip("Should this sensor only sense a specific tag?")]
    public bool SenseTag;

    /// <summary>
    /// The sensor is considered active when it overlaps an object on 
    /// layerToSense and with the tagToSense (if SenseTag is true).
    /// </summary>
    [Tooltip("The sensor is considered active when it overlaps an object on" +
        "layerToSense and with the tagToSense (if SenseTag is true).")]
    [SerializeField] private bool active = true;

    /// <summary>
    /// Which layer(s) should this sensor detect?
    /// </summary>
    [Tooltip("Which layer(s) should this sensor detect?")]
    [SerializeField] private LayerMask layerToSense;

    /// <summary>
    /// Radius of the sensor if it is a sphere.
    /// </summary>
    [Tooltip("Radius of the sensor if it is a sphere.")]
    [SerializeField] private float radius;

    /// <summary>
    /// Size of the sensor if it is a box.
    /// </summary>
    [Tooltip("Size of the sensor if it is a box.")]
    [SerializeField] private Vector3 boxSize;

    /// <summary>
    /// If SenseTag is true, this sensor will only sense objects on 
    /// layerToSense with this tag.
    /// </summary>
    [Tooltip("If SenseTag is true, this sensor will only sense objects on " +
        "layerToSense with this tag.")]
    [SerializeField] private string tagToSense;

    /// <summary>
    /// Color of the gizmo depicting this sensor when active.
    /// </summary>
    [Tooltip("Color of the gizmo depicting this sensor when active.")]
    [SerializeField] private Color activeColor = Color.white;

    /// <summary>
    /// Color of the gizmo depicting this sensor when inactive.
    /// </summary>
    [Tooltip("Color of the gizmo depicting this sensor when inactive.")]
    [SerializeField] private Color inactiveColor = Color.white;

    /// <summary>
    /// The sensor is considered active when it overlaps an object on 
    /// layerToSense and with the tagToSense (if SenseTag is true).
    /// </summary>
    public bool Active
    {
        get
        {
            return active && !IsDisabled;
        }
        set
        {
            if (value != active)
            {
                active = value;
                SensorStateChanged?.Invoke();
            }
        }
    }

    /// <summary>
    /// Timer for how long the sensor should be disabled. Disabled sensors
    /// are always considered not active.
    /// </summary>
    public float DisabledTimer { get; set; }

    /// <summary>
    /// Flag for whether this sensor is disabled. Disabled sensors are always
    /// considered not active.
    /// </summary>
    public bool IsDisabled
    {
        get
        {
            return DisabledTimer >= 0.0f;
        }
    }

    /// <summary>
    /// Array of colliders detected.
    /// </summary>
    private Collider[] detectedColliders;

    #region MonoBehaviour Methods
    private void FixedUpdate()
    {
        // Detect all colliders
        switch (SensorShape)
        {
            case SensorShape.Box:
                detectedColliders = Physics.OverlapBox(transform.position,
                    boxSize * 0.5f, Quaternion.identity, layerToSense);
                break;
            case SensorShape.Sphere:
                detectedColliders = Physics.OverlapSphere(transform.position,
                    radius, layerToSense);
                break;
            default:
                break;
        }

        // If sensing for a tag, check if at least one detected collider has
        // tagToSense. Otherwise, activate if at least one collider is detected.
        if (detectedColliders.Length > 0)
        {
            if (SenseTag && tagToSense != "")
            {
                bool atLeastOneTagSensed = false;
                foreach (Collider collider in detectedColliders)
                {
                    if (collider.CompareTag(tagToSense))
                    {
                        atLeastOneTagSensed = true;
                        break;
                    }
                }
                Active = atLeastOneTagSensed;
            }
            else
            {
                Active = true;
            }
        }
        else
        {
            Active = false;
        }
    }

    private void Update()
    {
        if (DisabledTimer >= 0.0f)
        {
            DisabledTimer -= Time.deltaTime;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Active ? activeColor : inactiveColor;

        switch (SensorShape)
        {
            case SensorShape.Box:
                Gizmos.DrawCube(transform.position, boxSize);
                break;
            case SensorShape.Sphere:
                Gizmos.DrawSphere(transform.position, radius);
                break;
            default:
                break;
        }
    }
    #endregion    
}
