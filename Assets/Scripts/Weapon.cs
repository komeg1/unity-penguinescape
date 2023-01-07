using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform hand;
    [SerializeField] private GameObject bulletPrefab;

    private PlayerItems items;

    private void Start()
    {
        items = GetComponent<PlayerItems>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (items.pickedSnowAmount >= 1)
                Shoot();

        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, hand.position, Quaternion.identity);
        items.pickedSnowAmount -= 0.5f;
    }
}
