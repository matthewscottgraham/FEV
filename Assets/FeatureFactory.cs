using System;
using System.Collections.Generic;
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

        private Transform _ghostFeature;
        
        private void OnEnable()
        {
            Blackboard.Instance.OnUpdate += PlaceGhostFeature;
            PlaceFeatureCommand.OnConfirmPlaceFeature += HandleConfirmPlaceFeature;
        }

        private void OnDisable()
        {
            Blackboard.Instance.OnUpdate -= PlaceGhostFeature;
            PlaceFeatureCommand.OnConfirmPlaceFeature -= HandleConfirmPlaceFeature;
        }

        private void PlaceGhostFeature()
        {
            if (Blackboard.Instance.SelectedCell == null) return;
            
            var featureType = Blackboard.Instance.FeatureMode;
            var cell = Blackboard.Instance.SelectedCell.Value;
            
            if (_ghostFeature != null)
            {
                Destroy(_ghostFeature.gameObject);
            }

            _ghostFeature = InstantiateNewFeature(featureType, cell);
        }
        private void HandleConfirmPlaceFeature()
        {
            if (Blackboard.Instance.SelectedCell == null) return;
            
            var featureType = Blackboard.Instance.FeatureMode;
            var cell = Blackboard.Instance.SelectedCell.Value;
            var feature = InstantiateNewFeature(featureType, cell);
            feature.GetComponent<MeshRenderer>().material.color = Blackboard.Instance.CurrentPlayer.Color;
            
            if (_ghostFeature != null)
                Destroy(_ghostFeature.gameObject);
        }

        private Transform InstantiateNewFeature(FeatureMode featureType, Cell cell)
        {
            switch (featureType)
            {
                case FeatureMode.Face:
                    return InstantiateFaceFeature(cell);
                case FeatureMode.Edge:
                    return InstantiateEdgeFeature(cell, Blackboard.Instance.SelectedCellComponent);
                case FeatureMode.Vertex:
                    return InstantiateVertexFeature(cell, Blackboard.Instance.SelectedCellComponent);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Transform InstantiateFaceFeature(Cell cell)
        {
            return InstantiateFeature(facePrefab, GridUtilities.GetCellPosition(cell));
        }

        private Transform InstantiateVertexFeature(Cell cell, int vertexIndex)
        {
            return InstantiateFeature(vertexPrefab, GridUtilities.GetVertexPosition(cell, vertexIndex));
        }

        private Transform InstantiateEdgeFeature(Cell cell, int edgeIndex)
        {
            var prefab = (edgeIndex % 2 == 0) ? hEdgePrefab : vEdgePrefab;
            return InstantiateFeature(prefab, GridUtilities.GetEdgePosition(cell, edgeIndex));
        }

        private Transform InstantiateFeature(Transform prefab, Vector3 position)
        {
            var feature = Instantiate(prefab, this.transform);
            feature.position = position;
            feature.gameObject.SetActive(true);
            return feature;
        }
    }
}