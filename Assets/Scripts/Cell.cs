using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    Point coordinate;
    Device prtDevice;

    public void Trigger()
    {
        prtDevice.Trigger();
    }
}
