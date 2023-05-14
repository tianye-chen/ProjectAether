using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroDialogue : MonoBehaviour
{
  [SerializeField]
  private TextMeshProUGUI textDisplay;
  [SerializeField]
  private string[] dialogues;
  [SerializeField]
  private float typingSpeed;
  private int dialogueIndex;


  void Start()
  {
    textDisplay.text = "";
    StartDialogue();
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Mouse0))
    {
      if (textDisplay.text == dialogues[dialogueIndex])
      {
        nextDialogue();
      }
      else
      {
        StopAllCoroutines();
        textDisplay.text = dialogues[dialogueIndex];
      }
    }
  }

  private void StartDialogue()
  {
    dialogueIndex = 0;
    StartCoroutine(Type());
  }

  private IEnumerator Type()
  {
    foreach (char letter in dialogues[dialogueIndex].ToCharArray())
    {
      textDisplay.text += letter;
      yield return new WaitForSeconds(typingSpeed);
    }
  }

  private void nextDialogue()
  {
    if (dialogueIndex < dialogues.Length - 1)
    {
      dialogueIndex++;
      textDisplay.text = "";
      StartCoroutine(Type());
    }
    else
    {
      SceneManager.LoadScene("generation");
    }
  }
}
