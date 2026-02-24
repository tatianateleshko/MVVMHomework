using System;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;
using R3;
using System.Collections.Generic;
using System.Linq;
using Player;


namespace UI.ViewComponents
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerName;

        [SerializeField] private TextMeshProUGUI _description;

        [SerializeField] private Image _icon;

        [SerializeField] private Button _closeButton;

        private IPlayerViewModel _viewModel;

        private CompositeDisposable _disposable = new CompositeDisposable();

        private void Awake()
        {
            _closeButton.onClick.AddListener(Hide);
        }

        private void OnDestroy()
        {
            _closeButton.onClick.RemoveListener(Hide);
        }

        public void Show(IViewModel viewModel)
        {
            if (viewModel is not IPlayerViewModel playerViewModel)
            {
                throw new Exception("Invalid view model type");
            }

            _viewModel = playerViewModel;

            _viewModel.Name
                .Subscribe(x => { _playerName.text = x; })
                .AddTo(_disposable);

            _viewModel.Description
                .Subscribe(x => { _description.text = x; })
                .AddTo(_disposable);

            _viewModel.PlayerIcon.
                Subscribe(x => { _icon.sprite = x; })
                .AddTo(_disposable);
            gameObject.SetActive(true);


        }

     
        private void Hide()
        {
            gameObject.SetActive(false);
            _disposable.Dispose();
            _viewModel.Dispose();
        }

    }
}


