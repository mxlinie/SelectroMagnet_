using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    //Variables
    public TextMeshProUGUI UIText;
    public string[] sentences; //Sentences you can type in inspector
    private int index;
    public float textSpeed; //How fast the dialogue goes
    public GameObject nextSentenceButton;

    //public Animator UITextAnim;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
        if(UIText.text == sentences[index])
        {
            nextSentenceButton.SetActive(true);
        }
    }

    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            UIText.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    public void StartNextSentence()
    {
        //UITextAnim.SetTrigger("Change");
        nextSentenceButton.SetActive(false);

        if(index < sentences.Length - 1)
        {
            index++;
            UIText.text = "";
            StartCoroutine(Type());
        }
        else
        {
            UIText.text = "";
        }
    }
}
