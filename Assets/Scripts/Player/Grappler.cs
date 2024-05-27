using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Grappler : MonoBehaviour
{
    [Header("Mouse Position On Screen(Game:MainCamera)")]
    [SerializeField] Vector3 screenPosition;

    [Header("Grappling")]
    [SerializeField] bool isGrappling;
    [HideInInspector] public bool retracting = false;

    [Header("Layer")]
    [SerializeField] LayerMask grapplableMask;
    Vector2 grappleTarget;


    [Header("Input")]
    [SerializeField] KeyCode grappleKey;   //  public KeyCode grappleKey = KeyCode.Mouse0;   (Kovakoodaa)

    [Header("Components")]
    [SerializeField] Camera mainCamera;
    [SerializeField] LineRenderer line;
    [SerializeField] DistanceJoint2D _distanceJoint;
    [SerializeField] RaycastHit2D hitPoint;

    [Header("Adjust")]
    [SerializeField] float maxDistance = 10f;
    [SerializeField] float grappleSpeed = 10f;
    [SerializeField] float grappleShootSpeed = 20f;





    void Start()
    {
        _distanceJoint.enabled = false;
        isGrappling = false;
    }


    void Update()
    {
        screenPosition = Input.mousePosition;

        Vector2 direction = screenPosition;
        hitPoint = Physics2D.Raycast(transform.position, direction, maxDistance, grapplableMask);
        
        
        if (Input.GetKeyDown(grappleKey))
        {
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            line.SetPosition(0, mousePos);
            line.SetPosition(1, transform.position);
            _distanceJoint.connectedAnchor = mousePos;
            _distanceJoint.enabled = true;
            line.enabled = true;
            isGrappling = true;
        }
        else if (Input.GetKeyUp(grappleKey))
        {
            _distanceJoint.enabled = false;
            line.enabled = false;
            isGrappling = false;

        }
        if (_distanceJoint.enabled)
        {
            line.SetPosition(1, transform.position);
        }

        //Debug.Log(hitPoint.collider);

    }
}
//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
//private void Update()
//{ 
//    screenPosition = Input.mousePosition;

//    if (Input.GetKeyDown(grappleKey) && !isGrappling)
//    {
//        StartGrapple();
//    }
//    else if (Input.GetKeyUp(grappleKey))
//    {
//        return;
//    }
//        _distanceJoint.enabled = false;
//        line.enabled = false;
//        isGrappling = false;

//    }
//    if (_distanceJoint.enabled) 
//    {
//        line.SetPosition(1, transform.position);
//    }
//}

//if (retracting)
//{
//    Vector2 grapplePos = Vector2.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);

//    transform.position = grapplePos;

//    line.SetPosition(0, transform.position);

//    if (Vector2.Distance(transform.position, target) < 0.5f)
//    {
//        retracting = false;
//        isGrappling = false;
//        line.enabled = false;
//    }
//}

//private void StartGrapple()
//{
//    Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

//    RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grapplableMask);

//    if (hit.collider != null)
//    {
//        isGrappling = true;
//        target = hit.point;
//        line.enabled = true;
//        line.positionCount = 2;

//        StartCoroutine(Grapple());
//    }
//}
//IEnumerator Grapple()
//{
//    float t = 0;
//    float time = 10;
//    Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
//    line.SetPosition(0, mousePos);
//    line.SetPosition(1, transform.position);

//    Vector2 newPos;

//    for (; t < time; t += grappleShootSpeed * Time.deltaTime)
//    {
//        newPos = Vector2.Lerp(transform.position, target, t / time);
//        line.SetPosition(0, transform.position);
//        line.SetPosition(1, newPos);
//        yield return null;
//    }

//    line.SetPosition(1, target);
//    retracting = true;
//}

//// VANHA PÄTKÄ TOIMI JÄI KESKEN YHDISTELEMINEN 2D TOP GRAPPLE sekä muut ja omat ideat

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GrappleHook : MonoBehaviour
//{
//    [Header("Mouse Position On Screen(Game:MainCamera)")]
//    [SerializeField] Vector3 screenPosition;

//    [Header("Grappling")]
//    [SerializeField] bool isGrappling;
//    [HideInInspector] public bool retracting = false;

//    [Header("Layer")]
//    [SerializeField] LayerMask grapplableMask;
//    Vector2 target;

//    [Header("Input")]
//    [SerializeField] KeyCode grappleKey;   //  public KeyCode grappleKey = KeyCode.Mouse0;   (Kovakoodaa)

//    [Header("Components")]
//    [SerializeField] Camera mainCamera;
//    [SerializeField] LineRenderer line;
//    [SerializeField] DistanceJoint2D _distanceJoint;


//    [Header("Adjust")]
//    [SerializeField] float maxDistance = 10f;
//    [SerializeField] float grappleSpeed = 10f;
//    [SerializeField] float grappleShootSpeed = 20f;
//    private void Start()
//    {
//        line = GetComponent<LineRenderer>();
//    }

//    private void Update()
//    {
//        screenPosition = Input.mousePosition;
//        if (Input.GetMouseButtonDown(0) && !isGrappling)
//        {
//            StartGrapple();
//        }

//        if (retracting)
//        {
//            Vector2 grapplePos = Vector2.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);

//            transform.position = grapplePos;

//            line.SetPosition(0, transform.position);

//            if (Vector2.Distance(transform.position, target) < 0.5f)
//            {
//                retracting = false;
//                isGrappling = false;
//                line.enabled = false;
//            }
//        }
//    }

//    private void StartGrapple()
//    {
//        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

//        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grapplableMask);

//        if (hit.collider != null)
//        {
//            isGrappling = true;
//            target = hit.point;
//            line.enabled = true;
//            line.positionCount = 2;

//            StartCoroutine(Grapple());
//        }
//    }

//    IEnumerator Grapple()
//    {
//        float t = 0;
//        float time = 10;

//        line.SetPosition(0, transform.position);
//        line.SetPosition(1, transform.position);

//        Vector2 newPos;

//        for (; t < time; t += grappleShootSpeed * Time.deltaTime)
//        {
//            newPos = Vector2.Lerp(transform.position, target, t / time);
//            line.SetPosition(0, transform.position);
//            line.SetPosition(1, newPos);
//            yield return null;
//        }

//        line.SetPosition(1, target);
//        retracting = true;
//    }
//}