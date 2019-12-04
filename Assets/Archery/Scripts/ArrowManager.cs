
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
    public GameObject stringAttachPoint;
    public GameObject arrowStartPoint;
    public GameObject stringStartPoint;
    public GameObject arrowPrefab;
    public bool bowGrabbed = false;
    public AudioClip arrowSound;
    public float maxWithdrawDistance;

    private bool isAttached = false;
    private GameObject currentArrow;
    private float withdrawDist;
    private AudioSource _as;
    private LineRenderer lr;
    private Vector3[] points;
    private Vector3 velocity, startingPosition;
    public float speed = 1;


    public SkinnedMeshRenderer LeftHand;
    public SkinnedMeshRenderer RightHand;
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
    void Start()
    {
        points = new Vector3[20];
        lr = GetComponent<LineRenderer>();

    }
    void Update()
    {
        if (bowGrabbed)
        {
            AttachArrow();
            PullString();
        }
        if (isAttached)
        {
            //velocity=
            ShowTrajecory();
        }
    }
    private void ShowTrajecory()
    {
        lr.positionCount = 20;
        Vector3 p = currentArrow.transform.position; ;
        Vector3 V = currentArrow.transform.forward * 35f * withdrawDist;
        for (int i = 0; i < 20; i++)
        {
            p = p + V * .051f;
            points[i] = (p);
            V += Physics.gravity * .051f;
        }
        lr.SetPositions(points);
    }
    public void clearTrajectory()
    {
        lr.positionCount = 0;
    }


    private void PullString()
    {
        if (isAttached)
        {
            switch (currentgrabbingHand)
            {
                case grabbingHand.LeftHand:
                    withdrawDist = (stringStartPoint.transform.position - trackedRightHand.transform.position).magnitude;
                    if (withdrawDist < maxWithdrawDistance)
                    {
                        stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition + new Vector3(0f, -.75f * withdrawDist, 0f);

                    }
                    else
                    {
                        withdrawDist = maxWithdrawDistance;
                    }

                    if (Input.GetAxis("TriggerRight") < .1f && Input.GetAxis("GripRight") < 0.1f)
                    {
                        Fire();
                    }
                    break;


                case grabbingHand.RightHand:
                    withdrawDist = (stringStartPoint.transform.position - trackedLeftHand.transform.position).magnitude;
                    if (withdrawDist < maxWithdrawDistance)
                    {
                        stringAttachPoint.transform.localPosition = stringStartPoint.transform.localPosition + new Vector3(0f, -.75f * withdrawDist, 0f);
                    }
                    else
                    {
                        withdrawDist = maxWithdrawDistance;
                    }
                    if (Input.GetAxis("TriggerLeft") < .1f && Input.GetAxis("GripLeft") < .1f)
                    {
                        Fire();
                    }
                    break;
                default:
                    break;
            }

        }
    }

    public void Fire()
    {

        playArrowSound();
        UnHideHand();
        currentArrow.transform.parent = null;
        currentArrow.GetComponent<Arrow>().Fired();
        Rigidbody r = currentArrow.GetComponent<Rigidbody>();
        r.velocity = currentArrow.transform.forward * 35f * withdrawDist;
        r.isKinematic = false;
        r.useGravity = true;
        currentArrow.GetComponent<Collider>().isTrigger = false;
        stringAttachPoint.transform.position = stringStartPoint.transform.position;
        currentArrow = null;
        isAttached = false;
        clearTrajectory();
    }

    private void AttachArrow()
    {
        if (currentArrow == null)
        {
            switch (currentgrabbingHand)
            {
                case grabbingHand.LeftHand:
                    {
                        currentArrow = Instantiate(arrowPrefab);
                        currentArrow.transform.parent = trackedRightHand.transform;
                        currentArrow.transform.localPosition = new Vector3(0f, 0f, .342f);
                        currentArrow.transform.localRotation = Quaternion.identity;
                    }

                    break;
                case grabbingHand.RightHand:
                    {
                        currentArrow = Instantiate(arrowPrefab);
                        currentArrow.transform.parent = trackedLeftHand.transform;
                        currentArrow.transform.localPosition = new Vector3(0f, 0f, .342f);
                        currentArrow.transform.localRotation = Quaternion.identity;
                    }

                    break;
                default:
                    break;
            }
        }
    }

    public void AttachBowToArrow()
    {

        HideHand();

        currentArrow.transform.parent = stringAttachPoint.transform;
        currentArrow.transform.position = arrowStartPoint.transform.position;
        currentArrow.transform.rotation = arrowStartPoint.transform.rotation;
        isAttached = true;

    }

    public void BowGrabbedStatus(bool grabbed)
    {
        bowGrabbed = grabbed;
    }

    private void playArrowSound()
    {
        _as.Stop();
        _as.Play();
    }


    private void HideHand()
    {
        switch (currentgrabbingHand)
        {
            case grabbingHand.LeftHand:

                RightHand.enabled = false;
                break;
            case grabbingHand.RightHand:
                LeftHand.enabled = false;
                break;
            default:
                break;
        }
    }
    private void UnHideHand()
    {
        switch (currentgrabbingHand)
        {
            case grabbingHand.LeftHand:

                RightHand.enabled = true;
                break;
            case grabbingHand.RightHand:
                LeftHand.enabled = true;
                break;
            default:
                break;
        }
    }

}
