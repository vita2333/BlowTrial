﻿using BlowTrial.Models;
using BlowTrial.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace BlowTrial.ViewModel
{
    public sealed class BackupDirectionViewModel : ViewModelBase, IDataErrorInfo
    {
        #region fields
        BackupDirectionModel _backupModel;
        #endregion

        #region constructors
        public BackupDirectionViewModel()
        {
            _backupModel = new BackupDirectionModel();
        }
        #endregion

        #region properties
        public bool PatientsPreviouslyRandomised
        {
            get
            {
                return _backupModel.PatientsPreviouslyRandomised;
            }
            set
            {
                if (_backupModel.PatientsPreviouslyRandomised == value) { return; }
                _backupModel.PatientsPreviouslyRandomised = value;
                NotifyPropertyChanged("PatientsPreviouslyRandomised");
            }
        }
        public bool? BackupToCloud
        {
            get
            {
                return _backupModel.BackupToCloud;
            }
            set
            {
                if (_backupModel.BackupToCloud == value) { return; }
                _backupModel.BackupToCloud = value;
                NotifyPropertyChanged("BackupToCloud");
            }
        }
        public bool IsValid
        {
            get
            {
                return _backupModel.IsValid;
            }
        }
        #endregion

        #region IDataError implementation
        string IDataErrorInfo.Error { get { return null; } }

        string IDataErrorInfo.this[string propertyName]
        {
            get
            {
                string error = this.GetValidationError(propertyName);
                CommandManager.InvalidateRequerySuggested();
                return error;
            }
        }
        string GetValidationError(string propertyName)
        {
            return ((IDataErrorInfo)_backupModel)[propertyName];
        }
        #endregion

        #region ListBoxOptions
        KeyValuePair<bool?, string>[] _backupToCloudOptions;
        public KeyValuePair<bool?, string>[] BackupToCloudOptions
        {
            get
            {
                return _backupToCloudOptions ??
                    (_backupToCloudOptions = CreateBoolPairs(Strings.CloudDirectoryVm_DataCollectingSite, Strings.CloudDirectoryVm_DataReceivingSite));
            }
        }

        KeyValuePair<bool, string>[] _previouslyRandomisedOptions;
        public KeyValuePair<bool, string>[] PreviouslyRandomisedOptions
        {
            get
            {
                return _previouslyRandomisedOptions ??
                    (_previouslyRandomisedOptions = 
                        new KeyValuePair<bool,string>[] {
                            new KeyValuePair<bool,string>(true,Strings.PreviouslyRandomisedOptions_True),
                            new KeyValuePair<bool,string>(false,Strings.PreviouslyRandomisedOptions_False)
                        });
            }
        }
        #endregion
    }

}