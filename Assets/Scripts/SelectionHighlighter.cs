using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace FEV
{
    public class SelectionHighlighter : MonoBehaviour
    {
        [SerializeField] private Transform faceSelectionObject;
        [SerializeField] private Transform vEdgeSelectionObject;
        [SerializeField] private Transform hEdgeSelectionObject;
        [SerializeField] private Transform vertexSelectionObject;

        private void OnEnable()
        {
            Blackboard.Instance.OnUpdate += HandleBlackboardUpdate;
        }

        private void OnDisable()
        {
            Blackboard.Instance.OnUpdate -= HandleBlackboardUpdate;
        }

        private void HandleBlackboardUpdate()
        {
            if (Blackboard.Instance.SelectedCell == null) return;
            
            HideSelectionObjects();
            
            var featureMode = Blackboard.Instance.FeatureMode;
            var cell = Blackboard.Instance.SelectedCell.Value;
            var index = Blackboard.Instance.SelectedComponent;
            
            switch (featureMode)
            {
                case FeatureMode.Vertex:
                    HandleVertexHover(cell, index); break;
                case FeatureMode.Face:
                    HandleFaceHover(cell); break;
                case FeatureMode.Edge:
                    HandleEdgeHover(cell, index); break;
                default:
                    break;
            }
        }
        
        private void HandleFaceHover(Cell cell)
        {
            transform.position = GridUtilities.GetCellPosition(cell);
            faceSelectionObject.gameObject.SetActive(true);
        }

        private void HandleEdgeHover(Cell cell, int edgeIndex)
        {
            transform.position = GridUtilities.GetCellPosition(cell);
            HideSelectionObjects();
            
            if (edgeIndex % 2 == 0)
            {
                vEdgeSelectionObject.gameObject.SetActive(true);
                vEdgeSelectionObject.position = GridUtilities.GetEdgePosition(cell, edgeIndex);
            }
            else
            {
                hEdgeSelectionObject.gameObject.SetActive(true);
                hEdgeSelectionObject.position = GridUtilities.GetEdgePosition(cell, edgeIndex);
            }
        }

        private void HandleVertexHover(Cell cell, int vertexIndex)
        {
            transform.position = GridUtilities.GetCellPosition(cell);
            HideSelectionObjects();
            vertexSelectionObject.gameObject.SetActive(true);
            vertexSelectionObject.position = GridUtilities.GetVertexPosition(cell, vertexIndex);
        }

        private void HideSelectionObjects()
        {
            faceSelectionObject.gameObject.SetActive(false);
            vEdgeSelectionObject.gameObject.SetActive(false);
            hEdgeSelectionObject.gameObject.SetActive(false);
            vertexSelectionObject.gameObject.SetActive(false);
        }
        
    }
}