using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("할당 O")]
    [SerializeField] GameObject spawner;
    [SerializeField] GameObject bullet;
    private GameObject bullets;
    
    public float yeonsaSpeed = 0.15f;
    void Start()
    {
        InvokeRepeating("CreatBullet", 0f, yeonsaSpeed);
        bullets = transform.root.gameObject.GetComponent<PlayerController>().bullets;
    }

    void CreatBullet(){
        Instantiate(bullet, spawner.transform.position, bullet.transform.rotation).transform.parent = bullets.transform;
    }
}
