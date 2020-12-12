using UnityEngine;
using UnityEngine.UI;
using System;
using Ink.Runtime;
using TMPro;
using System.Linq;

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
    private Button buttonPrefab = null;
    [SerializeField]
    private Button buttonContinue = null;
    //[SerializeField]
    //private TextMeshProUGUI currentText = null;


    void Awake()
    {
        //RemoveChildren();
        StartStory();
    }

    void StartStory()
    {
        story = new Story(inkJSONAsset.text);
        if (OnCreateStory != null) OnCreateStory(story);
        RefreshView();
    }

    void RefreshView()
    {
        // Remove all the UI on screen
        //RemoveChildren();

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
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim());
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
        RefreshView();
    }

    void CreateContentView(string text)
    {
        // Text storyText = Instantiate(textPrefab) as Text;
        // storyText.text = text;
        // storyText.transform.SetParent(canvas.transform, false);

        if (story.currentTags.FirstOrDefault(tag => tag == "left") != null)
        {
            leftCharacter.ShowText(text);
            rightCharacter.HideText();
        }

        if (story.currentTags.FirstOrDefault(tag => tag == "right") != null)
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
        if (story.currentTags.FirstOrDefault(tag => tag == "left") != null)
        {

        }

        if (story.currentTags.FirstOrDefault(tag => tag == "right") != null)
        {

        }

    }

    Button CreateChoiceView(string text)
    {
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(canvas.transform, false);

        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

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

}
