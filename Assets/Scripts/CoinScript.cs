using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public void pickedUp()
    {
        GetComponent<Animator>().SetBool("PickedUp", true);
    }

    public void disappear()
    {
        this.gameObject.active = false;
    }
}
