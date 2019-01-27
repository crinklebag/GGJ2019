using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealtorMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    [SerializeField] GameObject npcTarget;
    [SerializeField] float npcFollowDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowNPC();
    }

    void FollowNPC ()
    {
        float distance = Vector3.Distance(this.transform.position, npcTarget.transform.position);

        if(distance > npcFollowDistance)
        {
            Vector3 moveDirection = (npcTarget.transform.position - this.transform.position).normalized;
            this.GetComponent<Rigidbody>().MovePosition(this.transform.position + moveSpeed * moveDirection);
        }

    }
}
