using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MemoryFragmentShootable : MonoBehaviour, IShootable
{
    private static int fragmentCount = 0;
    [SerializeField] private UnityEvent _transition;
    [SerializeField] private int amountOfHitsNeeded;
    [Range(0, 100)]
    [SerializeField] private int percentScaleIncrease;
    private float sizeScale;
    private SpringValue spring;

    [Header("Spring Settings")]
    [SerializeField] private float stiffness;
    [SerializeField] private float damping;
    //[SerializeField] private float clampVelocity;
    [SerializeField] private float bounce;

    private void Start()
    {
        spring = new SpringValue(this.transform.localScale.x, stiffness, damping);
        sizeScale = (percentScaleIncrease / 100f) * transform.localScale.x;
        fragmentCount += 1;
        Debug.Log(fragmentCount);
    }
    private void Update()
    {
        spring.Update(Time.deltaTime);
        //spring.ClampVelocity();
        float temp = Mathf.Clamp(spring.value, 0, 10);
        transform.localScale = new Vector3(temp, temp, temp);
    }

    public void Hit()
    {
        amountOfHitsNeeded -= 1;
        spring.targetValue += sizeScale;
        spring.Nudge(bounce);
        if (amountOfHitsNeeded <= 0)
        {
            Destroy(gameObject.GetComponent<BoxCollider>());
            spring.targetValue = 0;
            Destroy(gameObject, 2);
            //enter behavior to change level
        }
    }

    private void OnDestroy()
    {
        fragmentCount -= 1;
        if (fragmentCount == 0)
        {
            _transition.Invoke();
        }
    }

}
