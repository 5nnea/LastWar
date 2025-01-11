using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] float speed = 5;
    
    private float maxZ = 20;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if(transform.position.z > maxZ){
            Destroy(gameObject);
        }
    }
}
