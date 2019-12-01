﻿
using UnityEngine;

public enum grabbingHand
{
    LeftHand,
    RightHand
}
public class ArrowManager : MonoBehaviour
{

    public static ArrowManager Instance;

    public grabbingHand currentgrabbingHand;
    public GameObject trackedLeftHand;
    public GameObject trackedRightHand;
    private GameObject currentArrow;

    public GameObject stringAttachPoint;
    public GameObject arrowStartPoint;
    public GameObject stringStartPoint;

    public GameObject arrowPrefab;

    public bool bowGrabbed = false;
    private bool isAttached = false;
    
    private float withdrawDist;

    private AudioSource _as;
    public AudioClip arrowSound;

    void Awake()
    {
        if (Instance == null)
            Instance = this;

        _as = this.GetComponent<AudioSource>();
        _as.clip = arrowSound;

    }

    void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    // Use this for initialization
    void Start()
    {

    }


    void Update()
    {
        if (bowGrabbed)
        {
            AttachArrow();
            PullString();
        }
        //Debug.Log(Input.GetKey(KeyCode.JoystickButton15));
        //Debug.Log(Input.GetKey(KeyCode.JoystickButton15));

        if (isAttached)
        {
           
        }
    }

    private void PullString()
    {


        if (isAttached)
        {
           
            switch (currentgrabbingHand)
            {

                case grabbingHand.LeftHand:
                    withdrawDist = (stringStartPoint.transform.position - trackedRightHand.transform.position).magnitude;
                    stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition + new Vector3(0f, -.75f * withdrawDist, 0f);


                    if (Input.GetKeyUp(KeyCode.JoystickButton15))
                    {
                        Fire();

                    }


                    break;
                case grabbingHand.RightHand:
                    withdrawDist = (stringStartPoint.transform.position - trackedLeftHand.transform.position).magnitude;
                    stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition + new Vector3(0f, -.75f * withdrawDist, 0f);
                    if (Input.GetKeyUp(KeyCode.JoystickButton14))
                    {
                        Fire();

                    }

                    break;
                default:
                    break;
            }

           
            //var device = SteamVR_Controller.Input((int)trackedObj.index);
            //if (device.GetTouchUp (SteamVR_Controller.ButtonMask.Trigger)) {
            if (Input.GetKeyDown(KeyCode.F))
            {


                Fire();
            }
        }
    }

    public void Fire()
    {
        playArrowSound();
        currentArrow.transform.parent = null;
        currentArrow.GetComponent<Arrow>().Fired();

        Rigidbody r = currentArrow.GetComponent<Rigidbody>();
        r.velocity = currentArrow.transform.forward * 25f * withdrawDist;
        r.isKinematic = false;
        r.useGravity = true;

        currentArrow.GetComponent<Collider>().isTrigger = false;

        stringAttachPoint.transform.position = stringStartPoint.transform.position;

        currentArrow = null;
        isAttached = false;
    }

    private void AttachArrow()
    {
        if (currentArrow == null)
        {
            switch (currentgrabbingHand)
            {
                case grabbingHand.LeftHand:
                    currentArrow = Instantiate(arrowPrefab);
                    currentArrow.transform.parent = trackedRightHand.transform;
                    currentArrow.transform.localPosition = new Vector3(0f, 0f, .342f);
                    currentArrow.transform.localRotation = Quaternion.identity;
                    break;
                case grabbingHand.RightHand:
                    currentArrow = Instantiate(arrowPrefab);
                    currentArrow.transform.parent = trackedLeftHand.transform;
                    currentArrow.transform.localPosition = new Vector3(0f, 0f, .342f);
                    currentArrow.transform.localRotation = Quaternion.identity;
                    break;
                default:
                    break;
            }

            
        }
    }

    public void AttachBowToArrow()
    {
        currentArrow.transform.parent = stringAttachPoint.transform;
        currentArrow.transform.position = arrowStartPoint.transform.position;
        currentArrow.transform.rotation = arrowStartPoint.transform.rotation;

        isAttached = true;
    }



    public void
        BowGrabbedStatus(bool grabbed)
    {
        bowGrabbed = grabbed;
    }

    private void playArrowSound()
    {
        _as.Stop();
        _as.Play();
    }
}