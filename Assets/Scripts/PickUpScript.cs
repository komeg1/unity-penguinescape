using UnityEngine;

public class PickUpScript : MonoBehaviour
{

    [SerializeField] LightTwinkleScript lightEffect;
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
}
