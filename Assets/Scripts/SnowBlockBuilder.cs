using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBlockBuilder : MonoBehaviour
{
    public GameObject spherePrefab;

    // A reference to the current sphere
    private GameObject sphere;
    private Rigidbody2D sphereRigidBody;
    private PlayerItems items;

    [SerializeField] private ParticleSystem destroyParticleSystem;

    private float shootForce = 20.0f;

    Vector3 mousePos;
    Vector3 rotation;

    [SerializeField] float snowballStartCost = 1f;
    [SerializeField] float snowballBuildingDelay = 0.3f;
    float buildingTime = 0f;
    bool buildingSnowball = false;
    [SerializeField] float snowballMaxMass = 50f;
    [SerializeField] float snowballMassIncrease = 10f;
    [SerializeField] float snowballBuildCost = 1f;
    [SerializeField] float snowballMassToScaleMultiplier = 3f;
    [SerializeField] Vector3 snowballInitialScale = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] float snowballDeadlyVelocity = 0f;

    bool hasBeenDestroyed = false;
    float totalSphereCost = 0f;

    private void Start()
    {
        items = GetComponent<PlayerItems>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        { 
            if (items.pickedSnowAmount >= snowballStartCost)
            {
                if (!buildingSnowball)
                {
                    buildingTime = 0f;
                    buildingSnowball = true;
                    items.pickedSnowAmount -= snowballStartCost;
                    totalSphereCost += snowballStartCost;
                }

                buildingTime += Time.deltaTime;


                // Create the sphere at the player's position if it doesn't already exist
                if (sphere == null)
                {
                    Vector3 playerPos = transform.position;
                    sphere = Instantiate(spherePrefab, playerPos, Quaternion.identity);
                    sphereRigidBody = sphere.GetComponent<Rigidbody2D>();
                    sphereRigidBody.mass = 1f;
                    sphere.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                }

                if (buildingTime >= snowballBuildingDelay && sphereRigidBody.mass <= snowballMaxMass)
                {
                    items.pickedSnowAmount -= snowballBuildCost * Time.deltaTime;
                    totalSphereCost += snowballBuildCost * Time.deltaTime;

                    // Scale up the sphere over time             
                    sphereRigidBody.mass += snowballMassIncrease * Time.deltaTime;
                    sphere.transform.localScale = snowballInitialScale * (1f + Mathf.Sqrt(sphereRigidBody.mass) / Mathf.PI * snowballMassToScaleMultiplier);
                }
            }

            if (sphere != null)
            {
                //move the sphere around the player
                mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
                rotation = (mousePos - transform.position).normalized;
                sphere.transform.position = transform.position + rotation * (Vector3.Magnitude(sphere.transform.localScale) / 4 + 0.5f);
                //GetComponent<PlayerMovement>().BlockMove(true);
            }
        }
        else
        {
            buildingSnowball = false;
            if (sphere != null)
            {
                mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
                rotation = (mousePos - transform.position).normalized;

                //Debug.Log("Shoot at " + rotation);

                if(sphereRigidBody.mass < 10f)
                    sphereRigidBody.AddForce(rotation * shootForce, ForceMode2D.Impulse);
                //GetComponent<PlayerMovement>().BlockMove(false);
                // Leave the sphere if the right mouse button is not being held down
                sphere = null;
                sphereRigidBody = null;
            }
        }
    }

}

