using UnityEngine;
using UnityEngine.UI;
using System;
using Ink.Runtime;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class DialogueController : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;

    [SerializeField]
    private TextAsset inkJSONAsset = null;
    public Story story;

    [SerializeField]
    private Canvas canvas = null;

    [SerializeField]
    private Character leftCharacter;
    [SerializeField]
    private Character rightCharacter;

    private string currentCharacter;

    // UI Prefabs
    //[SerializeField]
    //private Text textPrefab = null;
    [SerializeField]
    private Button[] choices;
    [SerializeField]
    private Button buttonContinue = null;

    private int charsIsReady = 0;


    void Awake()
    {
        //RemoveChildren();
        StartStory();
    }

    void StartStory()
    {
        story = new Story(inkJSONAsset.text);
        if (OnCreateStory != null) OnCreateStory(story);
        charsIsReady = 0;
        StartCoroutine(ShowCharacter(leftCharacter, -600f, 600f, (x, y) => (x < y)));
        StartCoroutine(ShowCharacter(rightCharacter, 600f, -600f, (x, y) => (x > y)));
        //RefreshView();
    }

    void RefreshView()
    {
        // Remove all the UI on screen
        //RemoveChildren();
        foreach(var choice in choices)
        {
            choice.gameObject.SetActive(false);
        }

        if (story.canContinue)
        {
            string text = story.Continue();
            text = ParseText(text);
            CreateContentView(text);
        }
        else
        {
            gameObject.SetActive(false);
            // Button choice = CreateChoiceView("End of story.\nRestart?");
            // choice.onClick.AddListener(delegate
            // {
            //     StartStory();
            // });
        }

        if (story.currentChoices.Count > 0)
        {
            buttonContinue.gameObject.SetActive(false);
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = GetChoiceView(choice.text.Trim(), i);
                button.onClick.AddListener(delegate
                {
                    OnClickChoiceButton(choice);
                });
            }
        }
        else
        {
            buttonContinue.gameObject.SetActive(true);
            buttonContinue.onClick.RemoveAllListeners();
            buttonContinue.onClick.AddListener(() => RefreshView());
        }
    }

    string ParseText(string text)
    {
        text = text.Trim();
        int index = text.IndexOf("::");
        if (index > -1)
        {
            //CreateCharacterView(text.Substring(0, index));
            text = text.Substring(index + 2);
        }
        return text;
    }
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        story.Continue();
        RefreshView();
    }

    void CreateContentView(string text)
    {
        // Text storyText = Instantiate(textPrefab) as Text;
        // storyText.text = text;
        // storyText.transform.SetParent(canvas.transform, false);

        if (story.currentTags.Contains("left"))
        {
            leftCharacter.ShowText(text);
            rightCharacter.HideText();
        }

        if (story.currentTags.Contains("right"))
        {
            rightCharacter.ShowText(text);
            leftCharacter.HideText();
        }

        //currentText.text = text;
    }

    void CreateCharacterView(string nameWithSprite)
    {
        var name = nameWithSprite;
        var spriteIndex = -1;
        var index = nameWithSprite.IndexOf('>');
        if (index > -1)
        {
            name = nameWithSprite.Substring(0, index);
            Int32.TryParse(nameWithSprite.Substring(index + 1, 1), out spriteIndex);
        }
        if (story.currentTags.Contains("left"))
        {

        }

        if (story.currentTags.Contains("right"))
        {

        }

    }

    Button GetChoiceView(string text, int index)
    {
        //Button choice = Instantiate(buttonPrefab) as Button;
        //choice.transform.SetParent(leftCharacter.transform, false);
        //choice.gameObject.SetActive(true);

        //TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
        //choiceText.text = text;

        //HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        //layoutGroup.childForceExpandHeight = false;
        var choice = choices[index];
        choice.gameObject.SetActive(true);

        TextMeshProUGUI choiceText = choice.GetComponentInChildren<TextMeshProUGUI>();
        choiceText.text = text;

        return choice;
    }

    void RemoveChildren()
    {
        int childCount = canvas.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }

    IEnumerator ShowCharacter(Character charr, float offsetX, float speed, Func<float, float, bool> f)
    {
        var charTransform = charr.transform;
        var oldPosition = charTransform.position;
        charTransform.position = new Vector3(oldPosition.x + offsetX, oldPosition.y, oldPosition.z);
        while (f(charTransform.position.x, oldPosition.x))
        {
            yield return new WaitForFixedUpdate();
            charTransform.position = new Vector3(charTransform.position.x + speed * Time.fixedDeltaTime, oldPosition.y, oldPosition.z);
        }
        CharIsReady();
    }

    void CharIsReady()
    {
        charsIsReady++;
        if (charsIsReady == 2)
        {
            RefreshView();
        }
    }
}
