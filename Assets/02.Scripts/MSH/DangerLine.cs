using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerLine : MonoBehaviour
{
    TrailRenderer tr;
    public Vector3 EndPosition;
    void Start()
    {
        tr = GetComponent<TrailRenderer>();
        tr.startColor = new Color(1, 0, 0, 0.7f);
        tr.endColor = new Color(1, 0, 0, 0.7f);
        Destroy(gameObject, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, EndPosition, Time.deltaTime * 100f);
    }
}
