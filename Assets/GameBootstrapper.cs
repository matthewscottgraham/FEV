using FEV;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour
{
    [SerializeField] private GridVisualizer gridVisualizer;

    private void Start()
    {
        gridVisualizer.Initialize();
    }
}
