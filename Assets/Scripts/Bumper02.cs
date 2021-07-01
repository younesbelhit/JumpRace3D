using UnityEngine;

[DisallowMultipleComponent]
public class Bumper02 : MonoBehaviour
{

    public float bumperForce = 40f;

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag("Player") || col.transform.CompareTag("Opponent"))
        {
            if (col.rigidbody != null)
            {
                col.rigidbody.AddForce(Vector3.up * bumperForce, ForceMode.Impulse);
            }
        }
    }

    void OnCollisionExit(Collision col)
    {
        if (col.transform.CompareTag("Player") || col.transform.CompareTag("Opponent"))
        {
            Destroy(gameObject, 2f);
        }
    }

}
