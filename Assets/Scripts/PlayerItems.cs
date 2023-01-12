using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public bool hasAxes = false;
    [SerializeField] public float snowParticleValue = 0.5f;
    [SerializeField] private float startingSnow = 10f;

    //snieg
    public float pickedSnowAmount = 0.0f;
    public float maxSnowAmount = 100f;

    private void Start()
    {
        pickedSnowAmount = startingSnow;
    }
    public void UpdateSnowAmount()
    {

        if (pickedSnowAmount < 100.0f)
            pickedSnowAmount += snowParticleValue;
    }

}
