using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public bool hasAxes = false;
    [SerializeField] private float snowToAddAmount = 0.5f;
    [SerializeField] private float startingSnow = 5f;

    //snieg
    public float pickedSnowAmount = 0.0f;

    private void Start()
    {
        pickedSnowAmount = startingSnow;
    }
    public void UpdateSnowAmount()
    {

        if (pickedSnowAmount < 100.0f)
            pickedSnowAmount += snowToAddAmount;
    }

}
