using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] GameObject spawner;
    [SerializeField] GameObject bullet;
    
    public float yeonsaSpeed = 0.15f;
    void Start()
    {
        InvokeRepeating("CreatBullet", 0f, yeonsaSpeed);
    }

    void CreatBullet(){
        Instantiate(bullet, spawner.transform.position, bullet.transform.rotation);
    }
}
