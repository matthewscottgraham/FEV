using Rules;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;

public class MainMenuController : MonoBehaviour
{
    private VisualElement _rootElement;
    private Vector2IntField _gameSizeField;
    
    private void Start()
    {
        var uiDocument = GetComponent<UIDocument>();
        
        _rootElement = uiDocument.rootVisualElement.AddNew<VisualElement>();
        _rootElement.AddToClassList("main-menu-container");

        var title = _rootElement.AddNew<Label>("title");
        title.text = "Tetsi";
        
        var heading = _rootElement.AddNew<Label>("heading");
        heading.text = "Rules";
        
        var label = _rootElement.AddNew<Label>();
        label.text = $"* {RuleFactory.GetRuleText(typeof(IsTileInBounds))}\n" +
                     $"* {RuleFactory.GetRuleText(typeof(IsTileObstructed))}";
        
        var spacer = _rootElement.AddNew<VisualElement>();
        spacer.style.height = 24;
        
        var buttonContainer = _rootElement.AddNew<VisualElement>("menu-button-container");
        
        var playButton = buttonContainer.AddNew<Button>();
        playButton.text = "PLAY";
        playButton.clicked += () => { SceneLoader.LoadScene("scn_game"); };
        
        var settingsButton = buttonContainer.AddNew<Button>();
        settingsButton.text = "CONFIG";
        
        var exitButton = buttonContainer.AddNew<Button>();
        exitButton.text = "EXIT";
        exitButton.clicked += SceneLoader.ExitGame;
    }
}
