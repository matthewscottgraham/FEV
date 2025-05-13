using UnityEngine;

namespace FEV
{
    public class Blackboard
    {
        public static Blackboard Instance { get; } = new Blackboard();

        public System.Action OnUpdate;
        
        public FeatureMode FeatureMode { get; private set; } = FeatureMode.Face;
        public Cell? SelectedCell { get; private set; } = null;
        public int SelectedComponent { get; private set; } = 0;

        public void SetFeatureMode(FeatureMode featureMode)
        {
            FeatureMode = featureMode;
            OnUpdate?.Invoke();
        }
        
        public void SetSelection(Cell cell, int componentIndex)
        {
            SelectedCell = cell;
            SelectedComponent = componentIndex;
            OnUpdate?.Invoke();
        }

        public void ClearSelection()
        {
            SelectedCell = null;
            SelectedComponent = 0;
            OnUpdate?.Invoke();
        }
    }
}