using UnityEngine;

public class PickUpScript : MonoBehaviour
{

    [SerializeField] LightTwinkleScript lightEffect;
    private bool isPickable = true;
    public void PickedUp()
    {
        GetComponent<Animator>().SetBool("PickedUp", true);
        if (lightEffect != null)
            lightEffect.StartTwinkling();
    }

    public void disappear()
    {
        this.gameObject.SetActive(false);
    }
    public void DisablePickUp()
    {
        isPickable= false;
    }
    public bool IsPickable()
    {
        return isPickable;
    }
}
