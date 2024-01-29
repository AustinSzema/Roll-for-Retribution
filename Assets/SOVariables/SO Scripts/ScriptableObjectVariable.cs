using UnityEngine;

/// <summary>
/// Delegate to signal that a ScriptableObjectVariable has been updated.
/// </summary>
public delegate void VariableUpdatedDelegate();

/// <summary>
/// Variable stored at the asset level. Invokes a delegate when the value is 
/// updated.
/// </summary>
/// <typeparam name="T">Type of variable to be stored.</typeparam>
public abstract class ScriptableObjectVariable<T> : ScriptableObject
{
    /// <summary>
    /// Delegate to signal that this ScriptableObjectVariable has been updated.
    /// </summary>
    public VariableUpdatedDelegate VariableUpdated;

    /// <summary>
    /// The value of this variable as a <T>.
    /// </summary>
    [SerializeField] private T value;

    /// <summary>
    /// The value of this variable as a <T>. Invokes VariableUpdated when set.
    /// </summary>
    public T Value
    {
        get
        {
            return this.value;
        }
        set
        {
            this.value = value;
            VariableUpdated?.Invoke();
        }
    }
}
