using Attack;
using GenericScripts;
using UnityEngine;
using UnityEngine.UI;
using EventType = GenericScripts.EventType;

namespace GUI.Scripts
{
    public class SwitchWeaponBar : MonoBehaviour
    {
        [SerializeField] private GameObject tripleWeaponButton;
        [SerializeField] private GameObject singleWeaponButton;
        private Image[] _singleWeaponButtonImage;
        private Image[] _tripleWeaponButtonImage;
    
        private string _currentWeaponType;

        [SerializeField] private float unselectedWeaponAlpha;
    
        private void Start()
        {
            unselectedWeaponAlpha /= 255f; // Unity stores color in normalized float3
        
            _singleWeaponButtonImage = singleWeaponButton.GetComponentsInChildren<Image>();
            _tripleWeaponButtonImage = tripleWeaponButton.GetComponentsInChildren<Image>();
        
            tripleWeaponButton.GetComponent<Button>().onClick.AddListener(delegate
            {
                ChangeButtonImageAlpha(_singleWeaponButtonImage, unselectedWeaponAlpha);
                ChangeButtonImageAlpha(_tripleWeaponButtonImage, 1f);
                EventManager.Instance.PostNostrification(EventType.WeaponChanged, this, nameof(TripleShootingWeapon));
            });
            singleWeaponButton.GetComponent<Button>().onClick.AddListener(delegate
            {
                ChangeButtonImageAlpha(_singleWeaponButtonImage, 1f);
                ChangeButtonImageAlpha(_tripleWeaponButtonImage, unselectedWeaponAlpha);
                EventManager.Instance.PostNostrification(EventType.WeaponChanged, this, nameof(SingleShootingWeapon));
            });
        }

        private void ChangeButtonImageAlpha(Image[] image, float alpha)
        {
            foreach (var imagePart in image)
            {
                var color = imagePart.color;
                color.a = alpha;
                imagePart.color = color;
            }
        }
    }
}
