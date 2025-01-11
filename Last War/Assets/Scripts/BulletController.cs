using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float bulletspeed = 12; //총알속도
    private float maxZ = 20; //총알의 최대 z값

    void Update()
    {
        transform.Translate(Vector3.forward * bulletspeed * Time.deltaTime); //생성후 계속 앞으로 이동

        //maxZ보다 총알의 Z값이 커지면 총알 삭제하기
        if(transform.position.z > maxZ){
            Destroy(gameObject);
        }
    }
}
