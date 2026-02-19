using R3;
using TMPro;
using UnityEngine;

namespace UI.ViewComponents
{
    public class PlayerStatView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _statName;
        [SerializeField] private TextMeshProUGUI _statValue;

        private IPlayerStatsViewModel _viewModel;

        public IPlayerStatsViewModel ViewModel => _viewModel; 
        
        private CompositeDisposable _disposable =  new();
        public void Initialize(IPlayerStatsViewModel playerStatsViewModel)
        {
            _viewModel = playerStatsViewModel;
            _statName.text = playerStatsViewModel.Name;
            _statValue.text = playerStatsViewModel.StatValue.ToString();

            _viewModel.StatValue
              .Subscribe(x => {_statValue.text = x; })
              .AddTo(_disposable);
        }

        private void OnDestroy()
        {
            _disposable?.Dispose();
        }
    }
}

