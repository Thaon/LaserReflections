using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsControl : MonoBehaviour
{
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Rotate(0, 0, horizontal * 100 * Time.deltaTime);
    }
}
