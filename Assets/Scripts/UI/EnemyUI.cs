using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private List<Image> _heartImages;

    public void DeleteHeartImage()
    {
        int lastImageIndex = _heartImages.Count - 1;
    
        if (_heartImages[lastImageIndex] != null)
        {
            _heartImages[lastImageIndex].enabled = false;
            _heartImages.Remove(_heartImages[lastImageIndex]);
        }
    }
}
