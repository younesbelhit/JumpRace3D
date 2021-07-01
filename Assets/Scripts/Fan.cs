using UnityEngine;

[DisallowMultipleComponent]
public class Fan : MonoBehaviour
{

    public float speed = 50f;

    void Update()
    {
        transform.Rotate(Vector3.back * speed * Time.deltaTime);
    }

}
