using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("할당O")]
    [SerializeField] GameObject enemyChild;

    [Header("할당X")]
    public int count = 3;
    public TMP_Text countText;

    //public bool isBoss = false;

    private float childNomalPosition = 1.1f;


    void Start()
    {   
        countText = transform.GetChild(0).GetComponent<TMP_Text>();
        countText.text = count.ToString();
        ChangeEnemyCount();
    }

    public void ChangeEnemyCount(){
        
            for(int i = 1; i < transform.childCount; i++){
                Destroy(transform.GetChild(i).gameObject);
            }

            Vector3 childPosition;
            int falseCount = count > 19 ? 20 : count;
            int rows = (falseCount / 3 == 0) ? 1 : falseCount/3 + 1;
            int lastRowCount = falseCount % 3;

            for(int i = 0; i< rows; i++){
                int InRow = (i+1 == rows) ? lastRowCount : 3;
                for(int j = 1; j<=InRow; j++){
                    childPosition = transform.position;

                    if (i == 0){ 
                        if (j % 2 == 0){
                            childPosition.x = childNomalPosition * (j / 2) + childPosition.x; 
                        } 
                        else if (j > 1){
                            childPosition.x = -childNomalPosition * (j - 1) / 2 + childPosition.x; 
                        } 
                        else {
                            continue;
                        }
                    } 
                    else{

                        if (j % 2 == 0){
                            childPosition.x = childNomalPosition * (j / 2) + childPosition.x; 
                        } 
                        else if (j > 1){
                            childPosition.x = -childNomalPosition * (j - 1) / 2 + childPosition.x; 
                        }
                    }

                    childPosition.z =  childNomalPosition * i + childPosition.z;
                    Instantiate(enemyChild,childPosition, enemyChild.transform.rotation)
                    .transform.parent = transform;
                }
            }
        }
}
