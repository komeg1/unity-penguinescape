using UnityEngine;

public class PlayerPickups : MonoBehaviour
{
    private PlayerLife playerLifeScript;

    private void Start()
    {
        playerLifeScript = GetComponent<PlayerLife>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            other.GetComponent<PickUpScript>().PickedUp();
            GameManager.instance.AddPoints(GameManager.instance.cherryScore);
        }
        else if (other.CompareTag("Key"))
        {
            other.GetComponent<PickUpScript>().PickedUp();
            GameManager.instance.AddKey();
        }
        else if (other.CompareTag("Life"))
        {
            if (playerLifeScript.MaxHealth())
                return;
            playerLifeScript.IncreaseHealth();
            other.GetComponent<PickUpScript>().PickedUp();
        }
        else if (other.CompareTag("IceAxes"))
        {
            GetComponent<PlayerItems>().hasAxes = true;
            other.GetComponent<PickUpScript>().disappear();
        }
        else if (other.CompareTag("Finish Line"))
        {
            if (GameManager.instance.enoughKeys())
            {
                GameManager.instance.Win();
            }
        }
    }
}
