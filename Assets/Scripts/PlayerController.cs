using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Cinemachine;

[DisallowMultipleComponent]
[RequireComponent(
    typeof(Rigidbody),
    typeof(AudioSource),
    typeof(Ragdoll)
)]
public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 8f;
    public float turnSpeed = 8f;
    public CinemachineVirtualCamera vcam;
    public Animator animator;
    public Transform character;
    [Header("Particles")]
    public GameObject waterSplashPrefab;
    public GameObject shockWavePrefab;
    [Header("Sounds")]
    public AudioClip victoryClip;
    public AudioClip waterSplashClip;
    [Header("Materials")]
    public Material lineMaterial;
    public Material cursorMaterial;

    Rigidbody rbody;
    Ragdoll ragdoll;
    AudioSource audioSource;
    LineRenderer line;
    GameObject cursor;
    Vector3 endPos;

    Vector3 touchStartPosition;
    Vector3 touchEndPosition;
    bool isDead;
    int currentBumperIndex;


    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        ragdoll = GetComponent<Ragdoll>();
        ragdoll.TurnOffRagdoll();

        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = .5f;
        line.endWidth = .5f;
        line.material = lineMaterial;

        cursor = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cursor.transform.localScale = new Vector3(.4f, .2f, .4f);
        cursor.transform.SetParent(transform);
        cursor.GetComponent<MeshRenderer>().material = cursorMaterial;
        Destroy(cursor.GetComponent<Collider>());
        
    }

    void Update()
    {

        if (line == null || cursor == null)
            return;
      
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1000f))
        {
            endPos = hit.point;
        }
            
        line.SetPosition(0, transform.position);
        line.SetPosition(1, endPos);

        cursor.transform.position = endPos;

    }

    void FixedUpdate()
    {

        if (GameManager.instance.gameState != GameState.Playing || isDead)
            return;

        #region Mouse Controls


        if (Input.GetMouseButtonDown(0))
        {
            touchStartPosition = Input.mousePosition;
        }
        else if(Input.GetMouseButton(0))
        {

            touchEndPosition = Input.mousePosition;

            // offset
            Vector3 offset = (touchEndPosition - touchStartPosition).normalized;

            if(offset.sqrMagnitude > .2f)
            {
                // apply rotation
                //transform.Rotate(new Vector3(0f, (offset.x + offset.y), 0f) * turnSpeed * Time.deltaTime);
                transform.Rotate(new Vector3(0f, (offset.x), 0f) * turnSpeed * Time.deltaTime);
            }

            rbody.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);

        }
        else if (Input.GetMouseButtonUp(0))
        {

        }


        #endregion


        // ----------------------------------------------


        #region Touch Controls


        if(Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) 
            {
                touchStartPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)  
            {
                touchEndPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) 
            {

            } 

        }


        #endregion

    }

    void OnCollisionEnter(Collision col)
    {

        if (col.transform.CompareTag("Bumper"))
        {
            animator.SetBool("Jump", true);
            animator.SetBool("BackFlip", false);
            EventManager.OnCollisionWithBumper(col.transform.GetComponent<Bumper>().index);
            GameObject shockWave = Instantiate(shockWavePrefab, col.transform.position + Vector3.up * .5f, Quaternion.Euler(-90f, 0f, 0f));
            Destroy(shockWave, 1f);
        }


        if (col.transform.CompareTag("Fan"))
        {
            isDead = true;
            Destroy(line);
            Destroy(cursor);
            ragdoll.TurnOnRagoll();
            EventManager.OnDie();
        }


        if (col.transform.CompareTag("Finish"))
        {
            Destroy(line);
            Destroy(cursor);
            rbody.isKinematic = true;
            animator.SetTrigger("Dance");
            audioSource.PlayOneShot(victoryClip);
            EventManager.OnCollisionWithBumper(FindObjectOfType<Bumpers>().transform.childCount);
        }

    }

    void OnCollisionExit(Collision col)
    {

        if (col.transform.CompareTag("Bumper"))
        {
            animator.SetBool("Jump", false);
            animator.SetBool("BackFlip", true);
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Water"))
        {
            Destroy(line);
            Destroy(cursor);
            EventManager.OnDie();
            audioSource.PlayOneShot(waterSplashClip);
            Instantiate(waterSplashPrefab, transform.position, Quaternion.identity);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.CompareTag("Water"))
        {
            rbody.isKinematic = true;
            Destroy(character.gameObject);
        }

    }



}
