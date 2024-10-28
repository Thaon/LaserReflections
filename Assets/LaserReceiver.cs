using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserReceiver : MonoBehaviour
{
    #region member variables

    public Material _normalMat,
        _hitMat;
    public UnityEvent OnHit;

    #endregion

    public void Activate()
    {
        OnHit?.Invoke();
        GetComponent<Renderer>().material = _hitMat;
    }

    void Update()
    {
        GetComponent<Renderer>().material = _normalMat;
    }
}
