using UnityEngine;
using UnityEngine.UI;

public class PlayerItems : MonoBehaviour
{
    public bool hasAxes = false;
    [SerializeField] public float snowParticleValue = 0.01f;
    [SerializeField] private float startingSnow = 5f;
    [SerializeField] public SnowBar snowBar;
    [SerializeField] private SpriteRenderer drowningEffect;

    private PlayerMovement player;
    private PlayerLife playerLife;

    //snieg
    public float pickedSnowAmount = 0.0f;

    [SerializeField] public float maxSnowAmount = 10f;
    [SerializeField] float maxAirAmount = 10f;
    private float air;

    private void Start()
    {
        air = maxAirAmount;
        pickedSnowAmount = startingSnow;
        snowBar.SetMaxSnow(maxSnowAmount);
        snowBar.SetSnow(startingSnow);

        player = GetComponent<PlayerMovement>();
        playerLife = GetComponent<PlayerLife>();
    }

    private void Update()
    {
        UpdateAir();
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
    float Sin0x(float value, float max, float maxvalue)
    {
        Debug.Log(value + " out of " + max);
        return Mathf.Sin(value / max * Mathf.PI/2) * maxvalue;
    }
    public async void UpdateAir()
    {
        drowningEffect.color = new Color(1f, 1f, 1f,Sin0x(maxAirAmount-air, maxAirAmount, 1f));
        if (player.inWater)
        {
            if (air > 0f)
            {
                air -= Time.deltaTime;
                if (air <= 1f)
                    drowningEffect.transform.GetChild(0).GetComponent<ParticleSystem>().enableEmission = false;
                else
                    drowningEffect.transform.GetChild(0).GetComponent<ParticleSystem>().enableEmission = true;
            }
            else
            {
                playerLife.Hurt();
            }
            
        }
        else
        {
            if (air < maxAirAmount)
                air += 4f * Time.deltaTime;
        }
    }
    

}
