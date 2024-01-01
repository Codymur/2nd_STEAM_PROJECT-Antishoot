using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Swinging : MonoBehaviour
{
    [Header("References")]
    public Transform gunTip, cam, player;
    public LayerMask whatIsGrappleable;
    public PlayerController pm;

    [Header("Swinging")]
    public float maxSwingDistance;
    private Vector3 swingPoint;
    private SpringJoint joint;

    [Header("OdmGear")]
    public Transform Cam_Holder;
    public Transform orientation;
    public Rigidbody rb;
    public float horizontalThrustForce;
    public float forwardThrustForce;
    public float extendCableSpeed;

    [Header("Prediction")]
    public RaycastHit predictionHit;
    public float predictionSphereCastRadius;
    public Transform predictionPoint;

    [Header("Input")]
    public KeyCode swingKey = KeyCode.Mouse1;
    public KeyCode swingKeyController = KeyCode.JoystickButton4;



    public Transform LineEnd;


    public Image DashBarImage;

    private void Start()
    {
        DashBarImage.fillAmount = 1f;
        var tempColor = DashBarImage.color;
        tempColor.a = 0f;
        DashBarImage.color = tempColor;
    }

    private void Update()
    {

        if (DashBarImage.fillAmount < 1)
        {
            var tempColor = DashBarImage.color;
            tempColor.a = 1f;
            DashBarImage.color = tempColor;
            DashBarCoolDown();
        }

        if (DashBarImage.fillAmount >= 0.9f)
        {
            StartCoroutine(FadeAwayUI());
        }
        if (DashBarImage.fillAmount <= 0.9f)
        {
            var tempColor = DashBarImage.color;
            tempColor.a = 1f;
            DashBarImage.color = tempColor;
        }

        if ((Input.GetKeyDown(swingKey) || Input.GetKeyDown(swingKeyController)) && DashBarImage.fillAmount >= 0.5f) StartSwing();
        if ((Input.GetKeyUp(swingKey) || Input.GetKeyUp(swingKeyController)) || DashBarImage.fillAmount == 0f) StopSwing();

        CheckForSwingPoints();

        if (joint != null) OdmGearMovement();
    }

    void DashBarCoolDown()
    {
        DashBarImage.fillAmount = Mathf.Lerp(DashBarImage.fillAmount, 1, 0.5f * Time.deltaTime);
    }

    IEnumerator FadeAwayUI()
    {
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            DashBarImage.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }


    private void CheckForSwingPoints()
    {
        if (joint != null) return;

        RaycastHit sphereCastHit;
        Physics.SphereCast(cam.position, predictionSphereCastRadius, cam.forward,
                            out sphereCastHit, maxSwingDistance, whatIsGrappleable);

        RaycastHit raycastHit;
        Physics.Raycast(cam.position, cam.forward,
                            out raycastHit, maxSwingDistance, whatIsGrappleable);

        Vector3 realHitPoint;

        // Option 1 - Direct Hit
        if (raycastHit.point != Vector3.zero)
            realHitPoint = raycastHit.point;

        // Option 2 - Indirect (predicted) Hit
        else if (sphereCastHit.point != Vector3.zero)
            realHitPoint = sphereCastHit.point;

        // Option 3 - Miss
        else
            realHitPoint = Vector3.zero;

        // realHitPoint found
        if (realHitPoint != Vector3.zero)
        {
            predictionPoint.gameObject.SetActive(true);
            predictionPoint.position = realHitPoint;
        }
        // realHitPoint not found
        else
        {
            predictionPoint.gameObject.SetActive(false);
        }

        predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;
    }


    private void StartSwing()
    {
        DashBarImage.fillAmount -= 0.5f;

        // return if predictionHit not found
        if (predictionHit.point == Vector3.zero) return;

        // deactivate active grapple
        if (GetComponent<Grappling>() != null)
            GetComponent<Grappling>().StopGrapple();
        pm.ResetRestrictions();

        pm.swinging = true;

        swingPoint = predictionHit.point;
        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = swingPoint;

        float distanceFromPoint = Vector3.Distance(player.position, swingPoint);

        // the distance grapple will try to keep from grapple point. 
        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0.25f;

        // customize values as you like
        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;


    }

    public void StopSwing()
    {
        pm.swinging = false;

        Destroy(joint);
    }

    private void OdmGearMovement()
    {
        // right
        if (Input.GetKey(KeyCode.D)) rb.AddForce(orientation.right * horizontalThrustForce * Time.deltaTime);
        // left
        if (Input.GetKey(KeyCode.A)) rb.AddForce(-orientation.right * horizontalThrustForce * Time.deltaTime);

        // forward
        if (Input.GetKey(KeyCode.W)) rb.AddForce(orientation.forward * horizontalThrustForce * Time.deltaTime);

        // shorten cable
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.JoystickButton0))
        {
            Vector3 directionToPoint = swingPoint - transform.position;
            rb.AddForce(directionToPoint.normalized * forwardThrustForce * Time.deltaTime);

            float distanceFromPoint = Vector3.Distance(transform.position, swingPoint);

            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;
        }
        // extend cable
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.JoystickButton1))
        {
            float extendedDistanceFromPoint = Vector3.Distance(transform.position, swingPoint) + extendCableSpeed;

            joint.maxDistance = extendedDistanceFromPoint * 0.8f;
            joint.minDistance = extendedDistanceFromPoint * 0.25f;
        }
    }

    private Vector3 currentGrapplePosition;


    public bool IsGrappling()
    {
        return joint != null;
    }

    public Vector3 GetGrapplePoint()
    {
        return swingPoint;
    }

}
