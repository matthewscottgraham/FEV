using FEV;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    private VisualElement _rootElement;
    private Vector2IntField _gameSizeField;
    
    private void Start()
    {
        //var matchStateObject = Resources.Load<MatchConfiguration>("MatchState");
        //var serializedMatchStateObject = new SerializedObject(matchStateObject);
        //var gridSizeProperty = serializedMatchStateObject.FindProperty("gridSize");
        
        var uiDocument = GetComponent<UIDocument>();
        
        _rootElement = uiDocument.rootVisualElement.AddNew<VisualElement>();

        //_gameSizeField = _rootElement.AddNew<Vector2IntField>();
        //_gameSizeField.BindProperty(gridSizeProperty);
        
        var playButton = _rootElement.AddNew<Button>();
        playButton.text = "Play";
        playButton.clicked += () => { SceneLoader.LoadScene("scn_game"); };
        
        var exitButton = _rootElement.AddNew<Button>();
        exitButton.text = "Exit";
        exitButton.clicked += () => { SceneLoader.ExitGame(); };
    }
}
