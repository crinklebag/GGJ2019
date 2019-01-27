using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealtorMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed;
    [SerializeField] GameObject npcTarget;
    [SerializeField] float npcFollowDistance;
    [SerializeField] AISpawner aiSpawner;
    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        animator = this.GetComponentInChildren<Animator>();
        StartCoroutine(GetNewNPCTarget());
    }

    // Update is called once per frame
    void Update()
    {
        if (npcTarget)
        {
            FollowNPC();
        }
    }

    void FollowNPC ()
    {
        float distance = Vector3.Distance(this.transform.position, npcTarget.transform.position);
        Vector3 moveDirection = (npcTarget.transform.position - this.transform.position).normalized;

        // Move realtor
        if (distance > npcFollowDistance)
        {
            this.GetComponent<Rigidbody>().MovePosition(this.transform.position + moveSpeed * moveDirection);
            animator.SetBool("isMoving", true);
        }
        else if (distance < 1.5f)
        {
            moveDirection *= -1;
            this.GetComponent<Rigidbody>().MovePosition(this.transform.position + moveSpeed * moveDirection);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    IEnumerator GetNewNPCTarget ()
    {
        npcTarget = aiSpawner.GetRandomNPC();

        if(npcTarget != null)
        {
            yield return new WaitForSeconds(Random.Range(15, 30));
        }
        else
        {
            yield return new WaitForSeconds(Random.Range(3, 6));
        }

        StartCoroutine(GetNewNPCTarget());
    }
}
