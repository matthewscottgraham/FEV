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
            Selector.OnFaceHover += HandleFaceHover;
            Selector.OnEdgeHover += HandleEdgeHover;
            Selector.OnVertexHover += HandleVertexHover;
            Selector.OnNullHover += HideSelectionObjects;
        }

        private void OnDisable()
        {
            Selector.OnFaceHover -= HandleFaceHover;
            Selector.OnEdgeHover -= HandleEdgeHover;
            Selector.OnVertexHover -= HandleVertexHover;
            Selector.OnNullHover -= HideSelectionObjects;
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