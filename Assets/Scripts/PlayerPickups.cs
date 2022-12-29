using UnityEngine;

public class PlayerPickups : MonoBehaviour
{
    private PlayerLife playerLifeScript;
    private AudioSource audioSource;
    [SerializeField] AudioClip bonusSound;
    [SerializeField] AudioClip victorySound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip keySound;
    [SerializeField] AudioClip heartSound;
    private void Start()
    {
        playerLifeScript = GetComponent<PlayerLife>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            other.GetComponent<PickUpScript>().PickedUp();
            GameManager.instance.AddPoints(GameManager.instance.cherryScore);
            audioSource.PlayOneShot(bonusSound, AudioListener.volume);
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
                GameManager.instance.AddPoints(playerLifeScript.health * 100);
                GameManager.instance.Win();
            }
        }
    }
}
