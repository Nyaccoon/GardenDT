using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UIElements;

public class LoadButton : MonoBehaviour
{
    private Button _loadButton;
    [SerializeField] private UIDocument _document;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _loadButton = _document.rootVisualElement.Q<Button>("LoadButton");
        _loadButton.clicked += LoadButtonClick;
    }

    private void LoadButtonClick()
    {
        TextEditor te = new TextEditor();
        te.Paste();
        te.SelectAll();
        string json = te.SelectedText;
        print(json);
        WrapperDTO wrap = JsonConvert.DeserializeObject<WrapperDTO>(json);
        print(wrap.objects[0].type);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
