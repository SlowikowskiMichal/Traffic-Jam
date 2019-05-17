using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isPlayer = false;
    public float maxThrowForce = 5f;
    bool isGrabbing = false;
    bool isHolding = false;
    Collider2D handCollider;
    Collider2D fistCollider;
    Rigidbody2D holdingObject;
    Rigidbody2D handBody;
    HingeJoint2D joint;
    Grabbable grabedObject;
    void Start()
    {
        fistCollider = GetComponent<CircleCollider2D>();
        handBody = GetComponent<Rigidbody2D>();
        joint = GetComponent<HingeJoint2D>();
        fistCollider.enabled = false;
    
    }

    void FixedUpdate()
    {
        if(isPlayer)
        {
           
           handBody.MovePosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));

           isGrabbing = Input.GetMouseButton(0);

           if(isGrabbing && !isHolding)
           {
               isHolding = true;
               handCollider = Physics2D.OverlapPoint(handBody.position);
               if(handCollider!=null)
               {
                    holdingObject = handCollider.GetComponent<Rigidbody2D>();
                    joint.connectedBody = holdingObject;

                    grabedObject = holdingObject.GetComponent<Grabbable>();
                    if(grabedObject != null)
                    {
                        grabedObject.Grabbed();
                    }
               }
               else
               {
                   fistCollider.enabled = true;
                   //Debug.Log($"Now I'm fist!");
               }
           }
           else if(!isGrabbing)
           {
               //Debug.Log("Now I'm free!");
               isHolding = isGrabbing;
               fistCollider.enabled = false;
               //holdingObject.velocity = Vector3.Normalize(holdingObject.velocity)*maxThrowForce*Time.deltaTime;
               holdingObject = null;
               if(grabedObject != null)
               {
                   grabedObject.LetGo();
               }
               grabedObject = null;
               joint.connectedBody = holdingObject;
           }

           if(isHolding)
           {

           }
        }
    }
}
