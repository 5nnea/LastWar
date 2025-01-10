using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float speed = 5;
    
    private float maxZ = 8;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(transform.position.z > maxZ){
            Destroy(gameObject);
        }
    }
}
