using UnityEngine;
using UnityEngine.UIElements;

namespace FEV
{
    public class CommandPallet : MonoBehaviour
    {
        private void Start()
        {
            var uiDocument = GetComponent<UIDocument>();
            var commandPanel = uiDocument.rootVisualElement.Q("commandPanel");
            
            commandPanel.Add(CreateCommandButton("Face", new SelectFaceCommand()));
            commandPanel.Add(CreateCommandButton("Edge", new SelectEdgeCommand()));
            commandPanel.Add(CreateCommandButton("Vertex", new SelectVertexCommand()));
            commandPanel.Add(CreateCommandButton("Place", new PlaceFeatureCommand()));
        }

        private Button CreateCommandButton(string label, ICommand command)
        {
            var button = new Button { text = label };
            button.clicked += command.Execute;
            return button;
        }
    }
}