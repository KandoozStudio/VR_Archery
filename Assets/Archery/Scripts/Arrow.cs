using UnityEngine;

public class Arrow : MonoBehaviour
{

    private bool isAttached = false;

    private bool isFired = false;
    public string bowTag = null;

    void OnTriggerStay(Collider collider)
    {

        if (collider.gameObject.CompareTag(bowTag))
        {


            switch (ArrowManager.Instance.currentgrabbingHand)
            {
                case grabbingHand.LeftHand:
                    if (Input.GetKey(KeyCode.JoystickButton15))
                    {
                        AttachArrow();
                    }
                    break;

                case grabbingHand.RightHand:

                    if (Input.GetKey(KeyCode.JoystickButton14))
                    {
                        AttachArrow();
                    }

                    break;
                default:
                    break;
            }
        }

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(bowTag))
        {

            //  AttachArrow();

        }

    }

    void Update()
    {
        if (isFired && transform.GetComponent<Rigidbody>().velocity.magnitude > 5f)
        {
            transform.LookAt(transform.position + transform.GetComponent<Rigidbody>().velocity);
        }
    }

    public void Fired()
    {
        isFired = true;
    }

    private void AttachArrow()
    {
        //var device = SteamVR_Controller.Input((int)ArrowManager.Instance.trackedObj.index);
        //	if (!isAttached && device.GetTouch (SteamVR_Controller.ButtonMask.Trigger)) {

        //   if (Input.GetKeyDown(KeyCode.A))
        if (!isAttached)

        {


            ArrowManager.Instance.AttachBowToArrow();
            isAttached = true;
            Debug.Log("Attached");

        }
    }


}
