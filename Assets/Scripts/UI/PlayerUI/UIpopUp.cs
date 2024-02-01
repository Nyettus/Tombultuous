using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIpopUp : MonoBehaviour
{
    public TextMeshProUGUI notifText;
    public Animator anim;
    private Queue<string> notifString = new Queue<string>();
    private Queue<float> notifSpeed = new Queue<float>();
    private Coroutine checks;
    // Start is called before the first frame update
    void Start()
    {
        UIManager._.popUp = this;
    }
    private void Update()
    {

    }

    public void AddNotification(string input,float speed)
    {
        notifString.Enqueue(input);
        notifSpeed.Enqueue(speed);
        if(checks == null)
        {
            checks = StartCoroutine(RunQueue());
        }
    }

    private void ShowNotif(string input,float speed)
    {
        notifText.text = input;
        anim.speed = speed;
        anim.Play("Notification");
    }

    private IEnumerator RunQueue()
    {
        do
        {
            ShowNotif(notifString.Dequeue(),notifSpeed.Dequeue());
            do
            {
                yield return null;
            }
            while (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
        }
        while(notifString.Count> 0);
        checks = null;
    }

}
