using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TrigData[] trigDatas;
    public int waves = 0;
    [SerializeField] GameObject trigs;

    void Start(){
        InvokeRepeating("CreatTrig",1f,10f);
    }

    void Update(){
        if(waves > trigDatas.Length){
            gameObject.SetActive(false);
        }
    }

    void CreatTrig(){
        waves++;
        if(trigDatas.Length < waves){
            SceneManager.LoadScene(1);
        }
        Instantiate(trigs, trigs.transform.position, trigs.transform.rotation);
    }
}
