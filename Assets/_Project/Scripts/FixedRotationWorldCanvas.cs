using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedRotationWorldCanvas : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, transform.root.rotation.z * -1.0f);
    }
}
