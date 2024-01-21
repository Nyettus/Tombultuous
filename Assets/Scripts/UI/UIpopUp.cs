using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIpopUp : MonoBehaviour
{
    public TextMeshProUGUI notifText;
    public Animator anim;
    private Queue<string> notifString = new Queue<string>();
    private Coroutine checks;
    // Start is called before the first frame update
    void Start()
    {
        UIManager._.popUp = this;
    }
    private void Update()
    {

    }

    public void AddNotification(string input)
    {
        notifString.Enqueue(input);
        if(checks == null)
        {
            checks = StartCoroutine(RunQueue());
        }
    }

    private void ShowNotif(string input)
    {
        notifText.text = input;
        anim.Play("Notification");
    }

    private IEnumerator RunQueue()
    {
        do
        {
            ShowNotif(notifString.Dequeue());
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
