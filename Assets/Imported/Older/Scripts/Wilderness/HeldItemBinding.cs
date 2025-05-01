using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldItemBinding : MonoBehaviour
{
    [SerializeField] private GameObject _heldItemGraphicsObject;
    [SerializeField] private BoolVariable _observedVariable;
    [SerializeField] private bool _startHolding;

    #region MonoBehaviour Methods
    private void OnEnable()
    {
        _observedVariable.VariableUpdated += UpdateHeldItemVisiblity;
    }
    private void Start()
    {
        if (_startHolding)
        {
            _observedVariable.Value = true;
        }
        else
        {
            _observedVariable.Value = false;
        }
        UpdateHeldItemVisiblity();
    }
    private void OnDisable()
    {
        _observedVariable.VariableUpdated -= UpdateHeldItemVisiblity;
    }
    #endregion

    public void UpdateHeldItemVisiblity()
    {
        if (_observedVariable.Value)
        {
            _heldItemGraphicsObject.SetActive(true);
        }
        else
        {
            _heldItemGraphicsObject.SetActive(false);
        }
    }
}
