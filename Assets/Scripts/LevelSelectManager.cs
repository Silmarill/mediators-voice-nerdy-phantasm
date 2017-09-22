using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour {


    //it can be struct

   //TODO: get from JSON


    public int posIndex;

    public float distanceBelowBlock = 3f;
    public float moveSpeed;
    public GameObject[] locks;
    public string[] levelNames;
    public string[] levelTags;
    public bool[] levelUnlocked;

    private bool isPressed;
    private Transform _tr;
    private Transform[] locksTransform;

    public bool isToucheModeEnabled;

    // Use this for initialization
    void Start () {
        posIndex = PlayerPrefs.GetInt("LevelIndexPosStore", 0);
        _tr = GetComponent <Transform>();
        locksTransform = new Transform[levelTags.Length];

        for (int i = 0; i < levelTags.Length; ++i) {

             locksTransform[i] = locks[i].GetComponent <Transform>();

            if (!PlayerPrefs.HasKey(levelTags[i])) {
                levelUnlocked[i] = false;
            } else if (PlayerPrefs.GetInt(levelTags[i]) == 0) {
                levelUnlocked[i] = false;
            } else {
                levelUnlocked[i] = true;
            }
           
            locks[i].SetActive(!levelUnlocked[i]);

        }

        _tr.position = locksTransform[posIndex].position + new Vector3(0, -distanceBelowBlock, 0);
    }
    
    // Update is called once per frame
    void Update () {
        if (!isPressed) {
            if (Input.GetAxis("Horizontal") > 0) {
                posIndex += 1;
                isPressed = true;
            }

            if (Input.GetAxis("Horizontal") < 0) {
                posIndex -= 1;
                isPressed = true;
            }

            if (posIndex >= levelTags.Length) {
                posIndex = levelTags.Length -1;
            }

            if (posIndex < 0) {
                posIndex = 0;
            }



        }

        if (isPressed) {
            if (Input.GetAxis("Horizontal") == 0) {
                isPressed = false;

            }
        }

        Vector3 targetPos = locksTransform[posIndex].position + new Vector3(0, -distanceBelowBlock, 0);
        
        //TODO: Use DOTween for map with roads
        _tr.position = Vector3.MoveTowards(_tr.position, targetPos, moveSpeed * Time.deltaTime);

         #if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_EDITOR
        if (Input.GetButtonDown("Fire1") || Input.GetButtonDown("Jump")){
            if (levelUnlocked[posIndex] && !isToucheModeEnabled) {
               PlayerPrefs.SetInt("LevelIndexPosStore", posIndex);
               SceneManager.LoadScene(levelNames[posIndex]);
            }
        }
        #endif

    }
}
