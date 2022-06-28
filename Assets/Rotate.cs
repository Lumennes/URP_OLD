using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float y;
    Transform _transform;
    Vector3 vect;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (y == 0)
            return;
        var newy = vect.y - y;
        vect = new(0, newy, 0);
        _transform.Rotate(vect);
    }
}
