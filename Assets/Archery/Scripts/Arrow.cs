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
                     if (Input.GetAxis("GripRight") > .5f && Input.GetAxis("TriggerRight") > .5f)
                    {
                        AttachArrow();
                    }
                    break;

                case grabbingHand.RightHand:

                    if (Input.GetAxis("GripLeft") > .5f && Input.GetAxis("TriggerLeft") > .5f)
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
    private void OnCollisionEnter(Collision collision)
    {
    //    Debug.Log("Collided with : " + collision.gameObject.name);
    }
    void Update()
    {
        if (isFired && transform.GetComponent<Rigidbody>().velocity.magnitude > 5f)
        {

            this.transform.forward = this.gameObject.GetComponent<Rigidbody>().velocity;

      //      transform.LookAt(transform.position + transform.GetComponent<Rigidbody>().velocity);
        }
    }

    public void Fired()
    {
        isFired = true;
    }

    private void AttachArrow()
    {

        if (!isAttached)

        {


            ArrowManager.Instance.AttachBowToArrow();
            isAttached = true;
            Debug.Log("Attached");

        }
    }


}
