using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    [SerializeField] GateOpenerScript[] gateAffectorsAND;
    [SerializeField] GateOpenerScript[] gateAffectorsOR;
    [SerializeField] GameObject gatePartPrefab;
    [SerializeField] int height = 2;
    [SerializeField] float openTimePartSeconds = 0.3f;
    [SerializeField] bool openDirectionUp = false;
    [SerializeField] float closeDelay = 0.3f;
    [SerializeField] float openDelay = 0.0f;

    public bool targetOpen = false;
    public bool busy = false;
    public bool open = false;
    public bool interrupt = false;

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
        if (opened)
            yield return new WaitForSeconds(openDelay);
        else
            yield return new WaitForSeconds(closeDelay); 

        busy = true;

        if ((openDirectionUp && opened) || (!openDirectionUp && !opened))// update from bottom to top
        {
            collider.enabled = !opened;
            renderer.enabled = !opened;
            for (int i = 0; i < gateParts.Length; i ++)
            {
                gateParts[i].GetComponent<BoxCollider2D>().enabled = !opened;
                gateParts[i].GetComponent<SpriteRenderer>().enabled = !opened;
                yield return new WaitForSeconds(openTimePartSeconds);
            }
        }
        else if((!openDirectionUp && opened) || (openDirectionUp && !opened))// update from top to bottom
        {

            for (int i = gateParts.Length-1; i >= 0; i --)
            {
                yield return new WaitForSeconds(openTimePartSeconds);
                gateParts[i].GetComponent<BoxCollider2D>().enabled = !opened;
                gateParts[i].GetComponent<SpriteRenderer>().enabled = !opened;               
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
    public bool checkAND()
    {
        foreach (GateOpenerScript gate in gateAffectorsAND)
            if (!gate.open)
                return false;
        return true;
    }
    public bool checkOR()
    {
        foreach (GateOpenerScript gate in gateAffectorsOR)
            if (gate.open)
                return true;
        return false;
    }
    public void CheckAffectors()
    {
        bool or = false, and = false;
        if (gateAffectorsAND.Length > 0)
            and = checkAND();
        if(gateAffectorsOR.Length > 0)
            or = checkOR();
        targetOpen = or || and;
    }
    public void UpdateGate()
    {
        CheckAffectors();
        Debug.Log("Updating- target state- " + targetOpen);
    }
}
