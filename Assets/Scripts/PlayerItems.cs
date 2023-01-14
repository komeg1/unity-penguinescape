using UnityEngine;
using UnityEngine.UI;

public class PlayerItems : MonoBehaviour
{
    public bool hasAxes = false;
    [SerializeField] public float snowParticleValue = 0.1f;
    [SerializeField] private float startingSnow = 10f;
    [SerializeField] public SnowBar snowBar;

    //snieg
    public float pickedSnowAmount = 0.0f;
    public float maxSnowAmount = 100f;

    private void Start()
    {
        pickedSnowAmount = startingSnow;
        snowBar.SetMaxSnow(maxSnowAmount);
        snowBar.SetSnow(startingSnow);
    }
    public void UpdateSnowAmount()
    {

        if (pickedSnowAmount < 100.0f)
            pickedSnowAmount += snowParticleValue;
            snowBar.SetSnow(pickedSnowAmount);
    }

}
