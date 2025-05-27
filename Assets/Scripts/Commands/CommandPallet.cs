using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace FEV
{
    public class CommandPallet : MonoBehaviour
    {
        private Label _playerLabel;

        private void OnEnable()
        {
            Blackboard.Instance.OnUpdate += HandleBlackboardUpdate;
        }

        private void OnDisable()
        {
            Blackboard.Instance.OnUpdate -= HandleBlackboardUpdate;
        }

        private void Start()
        {
            var uiDocument = GetComponent<UIDocument>();
            
            _playerLabel = uiDocument.rootVisualElement.Q<Label>("playerLabel");
            
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

        private void HandleBlackboardUpdate()
        {
            if (_playerLabel == null) return;

            _playerLabel.text = $"Player {Blackboard.Instance.CurrentPlayer.Index}";
            _playerLabel.style.color = Blackboard.Instance.CurrentPlayer.Color;
        }
    }
}