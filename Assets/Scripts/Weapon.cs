using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform hand;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeReference] public GameManager gameManager;

    private PlayerItems items;
    private SnowBlockBuilder blockBuilder;

    private void Start()
    {
        items = GetComponent<PlayerItems>();
        blockBuilder = GetComponent<SnowBlockBuilder>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && gameManager.state == GameManager.GameState.Game)
        {
            if (items.pickedSnowAmount >= blockBuilder.snowballStartCost)
                Shoot();

        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, hand.position, Quaternion.identity);
        items.addSnow(-blockBuilder.snowballStartCost);
    }
}
