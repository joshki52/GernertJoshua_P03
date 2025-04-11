using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    // used so that we can check if player has "Weapon" regardless of what template it uses
    public abstract void Fire();
}
