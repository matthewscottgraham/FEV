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

        var heading = _rootElement.AddNew<Label>("heading");
        heading.text = "Rules";
        
        var label = _rootElement.AddNew<Label>();
        label.text = $"* {RuleFactory.GetRuleText(typeof(IsTileInBounds))}\n" +
                     $"* {RuleFactory.GetRuleText(typeof(IsTileObstructed))}\n" +
                     $"* {RuleFactory.GetRuleText(typeof(IsTileAdjacentToOwnedPegs))}";
        
        var spacer = _rootElement.AddNew<VisualElement>();
        spacer.style.height = 24;
        
        var playButton = _rootElement.AddNew<Button>();
        playButton.text = "Play";
        playButton.clicked += () => { SceneLoader.LoadScene("scn_game"); };
        
        var exitButton = _rootElement.AddNew<Button>();
        exitButton.text = "Exit";
        exitButton.clicked += () => { SceneLoader.ExitGame(); };
    }
}
