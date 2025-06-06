using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    private VisualElement _rootElement;
    private Vector2IntField _gameSizeField;
    private void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        
        _rootElement = uiDocument.rootVisualElement.AddNew<VisualElement>();
        _gameSizeField = _rootElement.AddNew<Vector2IntField>();

        var playButton = _rootElement.AddNew<Button>();
        playButton.text = "Play";
        playButton.clicked += () => { SceneLoader.LoadScene("scn_game"); };
        
        var exitButton = _rootElement.AddNew<Button>();
        exitButton.text = "Exit";
        exitButton.clicked += () => { SceneLoader.ExitGame(); };
    }
}
