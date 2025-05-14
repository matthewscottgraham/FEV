using UnityEngine;

namespace FEV
{
    public class Blackboard
    {
        public static Blackboard Instance { get; } = new Blackboard();

        public System.Action OnUpdate;
        
        public FeatureMode FeatureMode { get; private set; } = FeatureMode.Face;
        public Cell? HoveredCell { get; private set; } = null;
        public int HoveredCellComponent { get; private set; } = 0;
        public Cell? SelectedCell { get; private set; } = null;
        public int SelectedCellComponent { get; private set; } = 0;
        
        public void SetFeatureMode(FeatureMode featureMode)
        {
            FeatureMode = featureMode;
            OnUpdate?.Invoke();
        }
        
        public void SetHovered(Cell cell, int componentIndex)
        {
            HoveredCell = cell;
            HoveredCellComponent = componentIndex;
            OnUpdate?.Invoke();
        }

        public void ClearHovered()
        {
            HoveredCell = null;
            HoveredCellComponent = 0;
            OnUpdate?.Invoke();
        }

        public void SelectHovered()
        {
            if (!HoveredCell.HasValue)
            {
                ClearSelection();
                return;
            }
            
            SetSelection(HoveredCell.Value, HoveredCellComponent);
        }
        public void SetSelection(Cell cell, int componentIndex)
        {
            SelectedCell = cell;
            SelectedCellComponent = componentIndex;
            OnUpdate?.Invoke();
            Debug.Log(SelectedCell.Value.Coordinates);
        }

        public void ClearSelection()
        {
            HoveredCell = null;
            HoveredCellComponent = 0;
            OnUpdate?.Invoke();
        }
    }
}