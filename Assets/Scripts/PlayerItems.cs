using UnityEngine;
using UnityEngine.UI;

public class PlayerItems : MonoBehaviour
{
    public bool hasAxes = false;
    [SerializeField] public float snowParticleValue = 0.01f;
    [SerializeField] private float startingSnow = 5f;
    [SerializeField] private float air = 10f;
    [SerializeField] public SnowBar snowBar;
    private PlayerMovement player;
    private PlayerLife playerLife;

    //snieg
    public float pickedSnowAmount = 0.0f;

    public float maxSnowAmount = 10f;
    public float maxAirAmount = 10f;

    private void Start()
    {
        pickedSnowAmount = startingSnow;
        snowBar.SetMaxSnow(maxSnowAmount);
        snowBar.SetSnow(startingSnow);

        player = GetComponent<PlayerMovement>();
        playerLife = GetComponent<PlayerLife>();

        InvokeRepeating("SetAir", 0.0f, 1f);
    }
    
    public void UpdateSnowAmount()
    {

        if (pickedSnowAmount < maxSnowAmount)
            pickedSnowAmount += snowParticleValue;
            snowBar.SetSnow(pickedSnowAmount);
    }
    public void addSnow(float amount)
    {
        pickedSnowAmount += amount;
        snowBar.SetSnow(pickedSnowAmount);
    }

    public void SetAir()
    {
        Debug.Log("Air amount " + air);
        if(player.inWater)
        {
            if (air > 0f)
                air -= 0.3f;
            else
                playerLife.Hurt();

        }
        else
        {
            if (air < maxAirAmount)
                air += 0.5f;
        }
    }
    

}
