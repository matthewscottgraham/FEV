using System;
using UnityEngine;

namespace FEV
{
    public class SelectionHighlighter : MonoBehaviour
    {
        [SerializeField] private Transform faceSelectionObject;
        [SerializeField] private Transform edgeSelectionObject;
        [SerializeField] private Transform vertexSelectionObject;

        private void OnEnable()
        {
            Selector.OnFaceHover += HandleFaceHover;
            Selector.OnEdgeHover += HandleEdgeHover;
            Selector.OnVertexHover += HandleVertexHover;
        }

        private void OnDisable()
        {
            Selector.OnFaceHover -= HandleFaceHover;
            Selector.OnEdgeHover -= HandleEdgeHover;
            Selector.OnVertexHover -= HandleVertexHover;
        }

        private void HandleFaceHover(Cell cell)
        {
            transform.position = GridUtilities.GetCellPosition(cell);
            HideSelectionObjects();
            faceSelectionObject.gameObject.SetActive(true);
        }

        private void HandleEdgeHover(Cell cell, int edgeIndex)
        {
            transform.position = GridUtilities.GetCellPosition(cell);
            HideSelectionObjects();
            edgeSelectionObject.gameObject.SetActive(true);
        }

        private void HandleVertexHover(Cell cell, int vertexIndex)
        {
            transform.position = GridUtilities.GetCellPosition(cell);
            HideSelectionObjects();
            vertexSelectionObject.gameObject.SetActive(true);
        }

        private void HideSelectionObjects()
        {
            faceSelectionObject.gameObject.SetActive(false);
            edgeSelectionObject.gameObject.SetActive(false);
            vertexSelectionObject.gameObject.SetActive(false);
        }
        
    }
}