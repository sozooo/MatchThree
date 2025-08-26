using System;
using System.Collections.Generic;
using Project.Scripts.UI.Enums;
using UnityEngine;

namespace Project.Scripts.UI
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] private List<CanvasData> _canvases;
        
        public void ChangeCanvas(GameCanvasType canvasType)
        {
            foreach (var canvasData in _canvases)
            {
                canvasData.Canvas.gameObject.SetActive(canvasData.CanvasType == canvasType);
            }
        }
    }

    [Serializable]
    public struct CanvasData
    {
        [field: SerializeField] public GameCanvasType CanvasType { get; private set; }
        [field: SerializeField] public Canvas Canvas { get; private set; }
    }
}