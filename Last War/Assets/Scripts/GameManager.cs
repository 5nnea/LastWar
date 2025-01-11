using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("할당 O")]
    public TrigData[] trigDatas; //생성할 트리그 데이터
    [SerializeField] GameObject trigs; //복제할 트리그모음 프리팹
    [SerializeField] Material goodTrig; //+,* 트리그에 색깔
    [SerializeField] Material badTrig;  //- 트리그에 색깔

    [Header("할당 X")]
    public int waves = 0; //게임 웨이브


    void Start(){
        InvokeRepeating("CreatTrig",1f,10f); //1초가 지난뒤 10초마다 CreatTrig함수 실행
    }

    void CreatTrig(){
        waves++; //wave에 1더하기

        //trigDatas에 길이가 웨이브값보다 작으면 실행
        if(trigDatas.Length < waves){
            SceneManager.LoadScene(1); //씬 인덱스 값이 1인 씬으로 이동
        }

        SetTrigInfo(); //트리그에 색, 값등을 설정해주는 함수   
    }

    //주석달기 귀찮기도 하고 여기를 설명할지를 모르겠어서 그냥 안달아야지 - 1/11의 나
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
