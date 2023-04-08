using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
        [Tooltip("Time in seconds that passes between each letter appearing on the screen.")]
        [SerializeField] private float textAppearingSpeed;

        [Header("Events")]
        [SerializeField] private UnityEvent onEnableDialogue;

        private List<string> optionsSentences;
        
        private DialogueManager dialogueManager;

        private WaitForSeconds waitForSeconds;
        private Coroutine fillDialogueBoxCoroutine;

        private void Start()
        {
            optionsSentences = new List<string>();
            waitForSeconds = new WaitForSeconds(textAppearingSpeed);
        }

        /// <summary>
        /// Initiates the dialogue with the NPC specified by <paramref name="manager"/>.
        /// </summary>
        /// <param name="manager"><see cref="DialogueManager"/> of some NPC.</param>
        public void InitiateDialogue(DialogueManager manager)
        {
            onEnableDialogue.Invoke();
            
            dialogueManager = manager;

            Cursor.lockState = CursorLockMode.Confined;
            
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
                    dialogueManager.ChangeCurrentDialogueObject(options[localI].text);
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