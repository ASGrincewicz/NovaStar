using UnityEngine;

namespace Veganimus.GDHQcert
{
    ///<summary>
    ///@author
    ///Aaron Grincewicz
    ///</summary>
    public class ScrollingBG : MonoBehaviour
    {
        
        [SerializeField] private float _speed;
        public enum ScrollDirection {Left, Right, Up, Down}
        [SerializeField] private ScrollDirection _scrollDirection;
        private Vector2 _matTextureOffset;
       
        private void Start()
        {
            _matTextureOffset = GetComponent<Renderer>().material.mainTextureOffset;
            if(_matTextureOffset == null)
                Debug.LogError("Renderer: ScrollingBG is NULL!");
            
        }
        private void Update() => Scroll();

        private void Scroll()
        {
            switch(_scrollDirection)
            {
                case ScrollDirection.Left:
                    _matTextureOffset = new Vector2(Time.time * _speed, 0);
                    break;
                case ScrollDirection.Right:
                    _matTextureOffset = new Vector2(Time.time * -_speed, 0);
                    break;
                case ScrollDirection.Up:
                    _matTextureOffset = new Vector2(0,Time.time * _speed);
                    break;
                case ScrollDirection.Down:
                    _matTextureOffset = new Vector2(0, Time.time * -_speed);
                    break;
            }
        }
    }
  }
