using UnityEngine;
using DG.Tweening;
using TMPro;

[DisallowMultipleComponent]
public class Bumper : MonoBehaviour
{
    public int index;
    public float bumperForce = 20f;
    public bool destroyOnCollision;
    public TextMeshPro textMesh;


    void Awake()
    {
        index = transform.parent.GetSiblingIndex();
    }


    void OnCollisionEnter(Collision col)
    {
        if(col.transform.CompareTag("Player") || col.transform.CompareTag("Opponent"))
        {

            transform.DOShakeScale(.2f);

            if(col.rigidbody != null)
            {
                col.rigidbody.AddForce(Vector3.up * bumperForce, ForceMode.Impulse);
            }

            if (destroyOnCollision)
            {
                Destroy(gameObject, 1f);
            }
                
        }
    }

    public void SetIndex(int index)
    {
        textMesh.text = index.ToString();
    }


}
