using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

[Serializable()]
public struct UIElements
{
    [SerializeField] TextMeshProUGUI textObj;
    public TextMeshProUGUI TextObj { get { return textObj; } }

    [SerializeField] TextMeshProUGUI subscript;
    public TextMeshProUGUI Subscript { get { return subscript; } }

    [SerializeField] CanvasGroup subscriptGroup;
    public CanvasGroup SubscriptGroup { get { return subscriptGroup; } }

    [SerializeField] Image page;
    public Image Page { get { return page; } }

    [SerializeField] Image lines;
    public Image Lines { get { return lines; } }

    [SerializeField] CanvasGroup noteCanvasGroup;
    public CanvasGroup NoteCanvasGroup { get { return noteCanvasGroup; } }

    [SerializeField] CanvasGroup listCanvasGroup;
    public CanvasGroup ListCanvasGroup { get { return listCanvasGroup; } }

    [SerializeField] CanvasGroup readButton;
    public CanvasGroup ReadButton { get { return readButton; } }

    [SerializeField] CanvasGroup nextButton;
    public CanvasGroup NextButton { get { return nextButton; } }

    [SerializeField] CanvasGroup previousButton;
    public CanvasGroup PreviousButton { get { return previousButton; } }

    [SerializeField] NoteData noteDataPrefab;
    public NoteData NoteDataPrefab { get { return noteDataPrefab; } }

    [SerializeField] RectTransform listRect;
    public RectTransform ListRect { get { return listRect; } }
}

public class NotesSystem : MonoBehaviour
{
    #region Data and Actions
    [SerializeField] UIElements UI = new UIElements();

    [SerializeField] Color color1 = Color.gray;
    [SerializeField] Color color2 = Color.gray;

    private static Dictionary<String, Note> Notes = new Dictionary<string, Note>();
    private List<NoteData> noteDatas = new List<NoteData>();
    private static Action<Note> A_Display = delegate { };
    #endregion

    #region Audio
    [SerializeField] private AudioSource[] sources = null;
    [Space]
    [SerializeField] private AudioClip openNoteSFX = null;
    [SerializeField] private AudioClip closeNoteSFX = null;
    [Space]
    [SerializeField] private AudioClip turnPageSFX = null;
    #endregion

    #region Properties and Private

    private Note activeNote = null;
    private Page ActivePage
    {
        get 
        {
            return activeNote.Pages[currentPage];
        }
    }
    private int currentPage = 0;
    private bool readSubscript = false;
    private Sprite defaultPageTeture = null;
    private bool usingNoteSystem;
    #endregion

    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }

    public void Open()
    {
        UnityEngine.Object.FindObjectOfType<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        UpdateList();
    }
    
    private void UpdateList()
    {
        ClearList();

        var index = 0;
        var height = 0.0f;
        foreach (var note in Notes)
        {
            var color = index % 2 == 0 ? color1 : color2;

            var newNotePrefab = Instantiate(UI.NoteDataPrefab, UI.ListRect);
            noteDatas.Add(newNotePrefab);

            newNotePrefab.UpdateInfo(note.Value, color);

            newNotePrefab.Rect.anchoredPosition = new Vector2(0, height);
            height -= newNotePrefab.Rect.sizeDelta.y;

            UI.ListRect.sizeDelta = new Vector2(UI.ListRect.sizeDelta.x, height * -1);

            index++;
        }
    }

    private void ClearList()
    {
        foreach (var note in noteDatas)
        {
            Destroy(note.gameObject);
        }
        noteDatas.Clear();
    }
}
