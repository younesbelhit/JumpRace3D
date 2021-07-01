using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Ragdoll : MonoBehaviour
{

    [SerializeField] Transform character;
    [SerializeField] Animator animator;

    Collider[] colliders;
    Rigidbody[] rigidbodies;

    void Awake()
    {
        colliders   = character.GetComponentsInChildren<Collider>();
        rigidbodies = character.GetComponentsInChildren<Rigidbody>();
    }

    public void TurnOnRagoll()
    {
        animator.enabled = false;

        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
    }

    public void TurnOffRagdoll()
    {
        animator.enabled = true;

        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = true;
        }
    }

}
