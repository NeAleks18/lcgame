using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class TowerManager : MonoBehaviour, INotifyPropertyChanged
{
    //dont touch!
    private bool _isFrequencyDone;
    private bool _isPanelRepaired;

    public bool IsFrequencyDone { get { return _isFrequencyDone; } set { if (_isFrequencyDone != value) { _isFrequencyDone = value; OnPropertyChanged(nameof(IsFrequencyDone)); CheckRequirements(); } } }
    public bool IsPanelRepaired { get { return _isPanelRepaired; } set { if (_isPanelRepaired != value) { _isPanelRepaired = value; OnPropertyChanged(nameof(IsPanelRepaired)); CheckRequirements(); } } }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    public void CheckRequirements()
    {
        if (IsFrequencyDone && IsPanelRepaired) TowerStart();
    }

    private void TowerStart()
    {
        Debug.Log("Tower is started");
    }
}
