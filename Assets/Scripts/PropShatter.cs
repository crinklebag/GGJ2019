using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PropShatter : MonoBehaviour
{
    [SerializeField] GameObject brokenObject; //grabs the broken version of the object to instantiate on break
    [SerializeField] float explosionRadius = 10f; //the radius around the center of the explosion that the explosion power is applied	
    [SerializeField] float explosionPower = 5f; //how strong the explion is
    [SerializeField] float upwardModifier = 1f; // the direction on the y plane the force is added

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShatterObject();
        }
    }

    void ShatterObject ()
    {
        GameObject newObject = Instantiate(brokenObject, transform.position, transform.rotation) as GameObject;
        while(newObject.transform.childCount > 0) { newObject.transform.GetChild(0).SetParent(null, true); }
    
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius); //checks the explosion radius for colliders and adds them to an array

        foreach (Collider shard in colliders) //applies the explosion force to each collider in the colliders array.
        {
            if (shard.GetComponent<Rigidbody>() != null) //if the collider 'shard' has a rigidbody the explosion is applied.
                shard.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, explosionPos, explosionRadius, upwardModifier);
        }

        Destroy(newObject);
        Destroy(gameObject);
    }
}