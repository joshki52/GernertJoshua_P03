using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputBroadcaster : MonoBehaviour
{
    public bool IsTapPressed { get; private set; } = false;

    private void Update()
    {
        //NOTE: put better Input Detection here, this is just for testing
        if (Input.GetKeyDown(KeyCode.E))
        {
            IsTapPressed = true;
            Debug.Log("Pressed");
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            IsTapPressed = false;
            Debug.Log("Released");
        }
    }
}
