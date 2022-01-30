using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct Pairs
{
    public GameObject left;
    public GameObject right;
}

public class Goal : ButtonReactor
{
    public int requiredSignals;
    public GameObject visualObject;

    public Transform spawnPoint_1;
    public Transform spawnPoint_2;

    public float yRotateSpeed = 50;
    public float zRotateSpeed = 50;

    public Pairs reward;

    public Camera endCamera;

    protected bool completed = false;
    protected int currentSignals;
    private GameManager gameManager;

    protected override void Start()
    {
        base.Start();
        gameManager = GameManager.Instance;
    }


    protected void Update()
    {
        if (!completed)
        {
            if (requiredSignals == currentSignals)
            {
                //Debug.Log("level ended completed");
                completed = true;
                gameManager.EndGame();
                GameObject left = GameObject.Instantiate<GameObject>(reward.left,spawnPoint_1,false);
                GameObject right = GameObject.Instantiate<GameObject>(reward.right,spawnPoint_2,false);
                left.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                right.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

                if (endCamera)
                {
                    Camera.main.enabled = false;
                    endCamera.enabled = true;
                }

                SoundManager.Instance.PlaySound(SoundNames.win);
                visualObject.GetComponent<MeshRenderer>().material.DOFade(0, 0.5f).OnComplete(()=> visualObject.GetComponent<MeshRenderer>().enabled = false);
                left.GetComponent<MeshRenderer>().material.DOFade(1, 2f);
                right.GetComponent<MeshRenderer>().material.DOFade(1, 2f);

                StartCoroutine(MoveToMainMenu());
                
            }
        }

        if (completed)
        {
            visualObject.transform.localRotation = Quaternion.Euler(visualObject.transform.localEulerAngles + (Vector3.up * yRotateSpeed * Time.deltaTime) + (Vector3.forward * zRotateSpeed * Time.deltaTime));
        }
    }

    IEnumerator MoveToMainMenu()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("MainMenu");
    }

    protected override void LogicEventCalled(LogicGatesBus.LogicGateEvent e)
    {
        if (e.type == LogicGatesBus.LogicGates.triggerButton && e.id == id)
        {
            if (e.actionType == LogicGatesBus.ActionType.exit)
            {
                currentSignals--;
            }
            else if (e.actionType == LogicGatesBus.ActionType.enter)
            {
                currentSignals++;
            }
        }
    }
}
