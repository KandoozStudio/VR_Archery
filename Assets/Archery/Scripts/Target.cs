using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Material white;
    public Material black;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Arrow"))
        {
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            ChangeColor();
        }
    }


    public void ChangeColor()
    {
        if (this.GetComponent<MeshRenderer>().material= white)
        {
            this.GetComponent<MeshRenderer>().material = black;
        }
        else if (this.GetComponent<MeshRenderer>().material = black)
        {
            this.GetComponent<MeshRenderer>().material = white;
        }
        
    }
}
