using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NPC.Dialogue
{
    public class DialogueUI : MonoBehaviour
    {
        [Header("Canvas Objects")]
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private TMP_Text personName;
        [SerializeField] private TMP_Text dialogue;
        [SerializeField] private List<TMP_Text> options;

        [Header("Settings")]
        [SerializeField] private float textAppearingSpeed;

        private List<string> optionsSentences;
        
        private DialogueManager dialogueManager;

        private WaitForSeconds waitForSeconds;
        private Coroutine fillDialogueBoxCoroutine;

        private void Start()
        {
            optionsSentences = new List<string>();
            waitForSeconds = new WaitForSeconds(textAppearingSpeed);
        }

        public void InitiateDialogue(DialogueManager manager)
        {
            dialogueManager = manager;

            Cursor.lockState = CursorLockMode.Confined;

            dialogueBox.SetActive(true);
            personName.text = dialogueManager.NpcName;
            ShowNextDialogue();
        }

        private void DeactivateOldOptions()
        {
            optionsSentences.Clear();
            foreach (var item in options)
            {
                item.text = "";
                item.gameObject.SetActive(false);
            }
        }

        private void ShowNextDialogue()
        {
            DeactivateOldOptions();
            
            LoadOptions();
            
            FillDialogueBox();
        }

        private void FillDialogueBox()
        {
            if (fillDialogueBoxCoroutine != null)
            {
                StopCoroutine(fillDialogueBoxCoroutine);
            }

            fillDialogueBoxCoroutine = StartCoroutine(FillDialogueBoxCoroutine());
        }

        private void LoadOptions()
        {
            for (int i = 0; i < dialogueManager.CurrentDialogue.NextDialogues.Count; i++)
            {
                options[i].gameObject.SetActive(true);

                optionsSentences.Add(dialogueManager.CurrentDialogue.NextDialogues[i].Name);

                int localI = i;
                var button = options[i].gameObject.GetComponent<Button>();
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    dialogueManager.ChangeCurrentDialogue(options[localI].text);
                    ShowNextDialogue();
                });
            }
        }

        private IEnumerator FillDialogueBoxCoroutine()
        {
            string dialogueText = dialogueManager.CurrentDialogue.Text;
            yield return StartCoroutine(FillTextBox(dialogueText, dialogue));

            for (int i = 0; i < dialogueManager.CurrentDialogue.NextDialogues.Count; i++)
            {
                string optionText = optionsSentences[i];
                yield return StartCoroutine(FillTextBox(optionText, options[i]));
            }
        }

        private IEnumerator FillTextBox(string text, TMP_Text textField)
        {
            textField.text = "";
            foreach (char letter in text)
            {
                textField.text += letter;
                yield return waitForSeconds;
            }
        }
    }
}