using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenager : MonoBehaviour {


    public RectTransform Towers;
    public RectTransform GameOverCanvas;
    Vector3 OverDestiny;
    Vector3 TowerDestiny;

    Vector3 StartTowerDestiny;
    Vector3 StartOverDestiny;

    private void Start()
    {
        StartOverDestiny= GameOverCanvas.position;
        OverDestiny = GameOverCanvas.position;
        StartTowerDestiny = Towers.position;
    }

    private void Update()
    {
        if(Vector2.Distance(GameOverCanvas.transform.position, OverDestiny) > 1)
        { GameOverCanvas.transform.Translate((OverDestiny - GameOverCanvas.position) * Time.deltaTime * 3f, Space.World); }
        if (Input.mousePosition.x / Screen.dpi > 9.0f)
        {
            TowerDestiny = StartTowerDestiny + new Vector3(-160, 0, 0);
        }
        else { TowerDestiny = StartTowerDestiny; }
        Towers.transform.Translate((TowerDestiny - Towers.position) * Time.deltaTime * 3f, Space.World);

    }

    void GameOver()
    {
        StartCoroutine(End());
    }

    IEnumerator End() {
        //GameObject.Find("Player").GetComponent<Animator>().SetBool("GameOver", true);
        yield return new WaitForSeconds(2f);
        OverDestiny = StartOverDestiny +new Vector3(0,-528f,0);
    }
}
