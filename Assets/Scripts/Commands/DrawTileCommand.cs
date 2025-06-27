using System.Collections.Generic;
using UnityEngine;

namespace FEV
{
    public class DrawTileCommand : ICommand
    {
        public static System.Action OnDrawTile;
        public string Label => "Draw card";
        
        public void Execute()
        {
            OnDrawTile?.Invoke();
        }

        public void Destroy()
        {
            // noop
        }
    }
}