using UnityEngine;
using UnityEngine.UI;

public class PlayerItems : MonoBehaviour
{
    public bool hasAxes = false;
    [SerializeField] public float snowParticleValue = 0.01f;
    [SerializeField] private float startingSnow = 5f;
    [SerializeField] public SnowBar snowBar;

    //snieg
    public float pickedSnowAmount = 0.0f;
    public float maxSnowAmount = 10f;

    private void Start()
    {
        pickedSnowAmount = startingSnow;
        snowBar.SetMaxSnow(maxSnowAmount);
        snowBar.SetSnow(startingSnow);
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
    

}
