using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpenerScript : MonoBehaviour
{
    public bool open = false;
    [SerializeField] GateScript[] affectedGates; 
    public void Open()
    {
        if (open == false)
        {
            open = true;
            informGates();
        }
    }

    public void Close()
    {
        if (open == true) 
        { 
            open = false;
            informGates();
        }
    }

    public void informGates()
    {
        foreach(GateScript gate in affectedGates)
        {
            gate.UpdateGate();
        }
    }
}
