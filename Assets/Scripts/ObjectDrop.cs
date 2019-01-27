using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrop : MonoBehaviour
{
    [SerializeField] private float _castDistance;

    [SerializeField] private SfxPlayer _player;

    [SerializeField] private bool isGrounded = true;


    public void Start()
    {
        _player = transform.GetComponent<SfxPlayer>();
    }

    public void Update()
    { 
        CheckGround();
    }

    public void CheckGround()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, -transform.up * _castDistance, Color.yellow);

        if (Physics.Raycast(transform.position, -transform.up, out hit, _castDistance, LayerMask.GetMask("Ground")))
        {
            if (hit.transform)
            {      
                if (!isGrounded)
                {
                    _player.Play();
                }

                isGrounded = true;
            }
        }
        else
        {
            isGrounded = false;
        }
    }
}
