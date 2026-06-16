using System.Collections.Generic;
using UnityEngine;
using Unity.UIToolkit;
using UnityEngine.UIElements;

public class CopyButton : MonoBehaviour
{
    private Button _copyButton;
    [SerializeField]
    private UIDocument _document;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _copyButton = _document.rootVisualElement.Q<Button>("CopyButton");
        _copyButton.clicked += copyClick;
        
    }

    private void copyClick()
    {
        WrapperDTO.objects.Clear();
        List<Building> buildings = new();
        buildings.AddRange(GetComponents<Building>());
        foreach (Building building in buildings)
        {
            //ObjectDTO buildDTO = new ObjectDTO(building.transform.position);
        }
        print("Copied");
        TextEditor te = new TextEditor();
        te.text = "sometimes it's like i can still hear her\ngay gay homosexual, gay!";
        te.SelectAll();
        te.Copy();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
