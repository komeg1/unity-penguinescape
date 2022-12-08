using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{
    public void PickedUp()
    {
        GetComponent<Animator>().SetBool("PickedUp", true);
    }

    public void disappear()
    {
        this.gameObject.SetActive(false);
    }
}
