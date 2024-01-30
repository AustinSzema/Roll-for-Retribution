using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    int healthPoints { get; set; }
    void takeDamage(int hitPoints);
}
