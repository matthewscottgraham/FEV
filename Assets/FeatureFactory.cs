using System;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace FEV
{
    public class FeatureFactory : MonoBehaviour
    {
        [SerializeField] private Transform facePrefab;
        [SerializeField] private Transform vEdgePrefab;
        [SerializeField] private Transform hEdgePrefab;
        [SerializeField] private Transform vertexPrefab;

        private void OnEnable()
        {
            PlaceFeatureCommand.OnConfirmPlaceFeature += HandleConfirmPlaceFeature;
        }

        private void OnDisable()
        {
            PlaceFeatureCommand.OnConfirmPlaceFeature -= HandleConfirmPlaceFeature;
        }

        private void HandleConfirmPlaceFeature()
        {
            if (Blackboard.Instance.SelectedCell == null) return;
            
            var featureType = Blackboard.Instance.FeatureMode;
            var cell = Blackboard.Instance.SelectedCell.Value;
            switch (featureType)
            {
                case FeatureMode.Face:
                    InstantiateFaceFeature(cell);
                    break;
                case FeatureMode.Edge:
                    InstantiateEdgeFeature(cell, Blackboard.Instance.SelectedCellComponent);
                    break;
                case FeatureMode.Vertex:
                    InstantiateVertexFeature(cell, Blackboard.Instance.SelectedCellComponent);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        } 

        private void InstantiateFaceFeature(Cell cell)
        {
            InstantiateFeature(facePrefab, GridUtilities.GetCellPosition(cell));
        }

        private void InstantiateVertexFeature(Cell cell, int vertexIndex)
        {
            InstantiateFeature(vertexPrefab, GridUtilities.GetVertexPosition(cell, vertexIndex));
        }

        private void InstantiateEdgeFeature(Cell cell, int edgeIndex)
        {
            var prefab = (edgeIndex % 2 == 0) ? hEdgePrefab : vEdgePrefab;
            InstantiateFeature(prefab, GridUtilities.GetEdgePosition(cell, edgeIndex));
        }

        private void InstantiateFeature(Transform prefab, Vector3 position)
        {
            var feature = Instantiate(prefab, this.transform);
            feature.position = position;
            feature.gameObject.SetActive(true);
        }
    }
}