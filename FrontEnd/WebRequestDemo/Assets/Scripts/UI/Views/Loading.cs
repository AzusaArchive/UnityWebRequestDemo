using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class Loading : MonoBehaviour
    {
        public Text messageText;
        public Image rotateImage;
        public Button retryButton;
        public Button cancelButton;
        private Transform _rtf;
        private CanvasGroup _cg;

        private bool _activated;
        private bool _isFailed;

        public bool Activated
        {
            get
            {
                return _activated;
            }
            set
            {
                if(_cg)
                {
                    if (value)
                    {
                        _cg.alpha = 1;
                        _cg.blocksRaycasts = true;
                    }
                    else
                    {
                        _cg.alpha = 0;
                        _cg.blocksRaycasts = false;
                    }
                }
                _activated = value;
            }
        }

        public bool IsFailed
        {
            get { return _isFailed; }
            set
            {
                retryButton.gameObject.SetActive(value);
                _isFailed = value;
            }
        }

        public string Text
        {
            get { return messageText.text; }
            set
            {
                messageText.text = value;
            }
        }

        public event Action OnRetry;
        public event Action OnCancel;
        
        private void Awake()
        {
            retryButton.onClick.AddListener(() => IsFailed = false);
            
            _rtf = rotateImage.GetComponent<Transform>();
            _cg = gameObject.AddComponent<CanvasGroup>();
            _cg.blocksRaycasts = true;
            _cg.interactable = true;

            if (retryButton)
            {
                retryButton.onClick.AddListener(() => OnRetry?.Invoke());
            }

            if (cancelButton)
            {
                cancelButton.onClick.AddListener(() => OnCancel?.Invoke());
            }

            IsFailed = false;
        }

        private void Update()
        {
            _rtf.Rotate(0, 0, Time.deltaTime * 12);
        }
    }
}