using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TrigData[] trigDatas;
    public int waves = 0;

    [SerializeField] GameObject trigs;
    [SerializeField] Material goodTrig;
    [SerializeField] Material badTrig;

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
        SetTrigInfo();     
    }

    void SetTrigInfo(){
        GameObject newTrigs = Instantiate(trigs, trigs.transform.position, trigs.transform.rotation);
        GameObject trig1 = newTrigs.transform.GetChild(0).gameObject;
        GameObject trig2 = newTrigs.transform.GetChild(1).gameObject;
        TMP_Text trigText1 = trig1.transform.GetChild(0).GetComponent<TMP_Text>();
        TMP_Text trigText2 = trig2.transform.GetChild(0).GetComponent<TMP_Text>();
        
        trigText1.text = trigDatas[waves-1].sign1 == "*" ? "X" + trigDatas[waves-1].num1.ToString() : 
        trigDatas[waves-1].sign1.ToString() + trigDatas[waves-1].num1.ToString();

        trigText2.text = trigDatas[waves-1].sign2 == "*" ? "X" + trigDatas[waves-1].num2.ToString() :
        trigDatas[waves-1].sign2.ToString() + trigDatas[waves-1].num2.ToString();

        trig1.GetComponent<MeshRenderer>().material = trigDatas[waves-1].sign1 == "-" ? badTrig : goodTrig;
        trig2.GetComponent<MeshRenderer>().material = trigDatas[waves-1].sign2 == "-" ? badTrig : goodTrig;

    }
}
