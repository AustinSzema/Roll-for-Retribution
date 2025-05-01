using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringValue
{
    public float value { get; private set; }
    public float targetValue { get; set; }
    public float stiffness { get; set; }
    public float damping { get; set; }
    public float velocity { get; private set; }

    public float clampVelocity { get; set; }
    
    public SpringValue()
    {
        value = 0;
        targetValue = 0;
        stiffness = 0;
        damping = 0;
        velocity = 0;
    }

    public SpringValue(float targetValue, float stiffness, float damping)
    {
        this.value = 0;
        this.targetValue = targetValue;
        this.stiffness = stiffness;
        this.damping = damping;
        this.velocity = 0;
    }

    public SpringValue(float targetValue, float stiffness, float damping, float clampVelocity)
    {
        this.value = 0;
        this.targetValue = targetValue;
        this.stiffness = stiffness;
        this.damping = damping;
        this.velocity = 0;
        this.clampVelocity = clampVelocity;
    }

    public void Update(float deltaTime)
    {
        float force = (targetValue - value) * stiffness;
        velocity += force * deltaTime;
        velocity *= Mathf.Pow(damping, deltaTime);
        value += velocity * deltaTime;
    }

    public void Nudge (float nudgeValue)
    {
        velocity += nudgeValue;
    }

    public void ClampVelocity()
    {
        velocity = Mathf.Clamp(velocity, -clampVelocity, clampVelocity);
    }
}
