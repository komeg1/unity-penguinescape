using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    [SerializeField] GateOpenerScript[] gateAffectors;
    [SerializeField] GameObject gatePartPrefab;
    [SerializeField] int height = 2;
    [SerializeField] float openTimePartSeconds = 0.3f;

    public bool targetOpen = false;
    public bool busy = false;
    public bool open = false;

    public GameObject[] gateParts;

    SpriteRenderer renderer;
    BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();

        gateParts = new GameObject[height];
        for(int i = 0; i < height; i++)
        {
            GameObject part = Instantiate(gatePartPrefab, transform);
            part.transform.localPosition = Vector3.up * i;

            gateParts[i] = part;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(open != targetOpen)
        {
            ChangeState();
        }
    }

    IEnumerator OpenCloseAnimation(bool opened)
    {
        busy = true;
        if (opened)
        {
            collider.enabled = !opened;
            renderer.enabled = !opened;
            for (int i = 0; i < gateParts.Length; i ++)
            {
                yield return new WaitForSeconds(openTimePartSeconds);
                gateParts[i].GetComponent<BoxCollider2D>().enabled = !opened;
                gateParts[i].GetComponent<SpriteRenderer>().enabled = !opened;
                Debug.Log("Changed state of part " + i + " to " + opened);
            }
        }
        else if(!opened)
        {

            for (int i = gateParts.Length-1; i >= 0; i --)
            {
               
                gateParts[i].GetComponent<BoxCollider2D>().enabled = !opened;
                gateParts[i].GetComponent<SpriteRenderer>().enabled = !opened;
                yield return new WaitForSeconds(openTimePartSeconds);
            }
            collider.enabled = !opened;
            renderer.enabled = !opened;
        }
        open = opened;
        busy = false;
        Debug.Log("Changed state: Opened-" + open);
    }

    void ChangeState()
    {
        if (!busy)
        {
            StartCoroutine(OpenCloseAnimation(targetOpen));         
        }
    }

    public void UpdateGate()
    { 
        bool state = true;
        foreach(GateOpenerScript gate in gateAffectors)
        {
            if (!gate.open)
            {
                state = false;
                break;
            }
        }
        targetOpen = state;
        Debug.Log("Updating- target state- " + targetOpen);
    }
}
