using Unity.VisualScripting;
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

    void OnTriggerEnter(Collider collider){
        EnemyController enemyController;
        if(collider.CompareTag("Enemy")){
            enemyController = collider.GetComponent<EnemyController>();
            DamageEnemy(enemyController, collider);
        }
        else if(collider.CompareTag("EnemyChild")){
            enemyController = collider.transform.parent.GetComponent<EnemyController>();
            DamageEnemy(enemyController, collider);
        }
    }

    void DamageEnemy(EnemyController enemyController, Collider collider){
        Destroy(gameObject);
        enemyController.countText.text = (--enemyController.count).ToString();
        if(enemyController.count <= 0){
            Destroy(enemyController.gameObject);
        }
    }
}
