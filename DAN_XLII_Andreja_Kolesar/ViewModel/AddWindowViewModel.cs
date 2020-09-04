using DAN_XLII_Andreja_Kolesar.Command;
using DAN_XLII_Andreja_Kolesar.Service;
using DAN_XLII_Andreja_Kolesar.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLII_Andreja_Kolesar.ViewModel
{
    class AddWindowViewModel : ViewModelBase
    {
        AddAndEditEmployeeWindow addEmployee;

        BackgroundWorker backgroundWorker = new BackgroundWorker();

        #region property
        private vwEmployee _newEmployee;
        public vwEmployee newEmployee
        {
            get
            {
                return _newEmployee; ;
            }
            set
            {
                _newEmployee = value;
                OnPropertyChanged("newEmployee");
            }
        }


        //if this is true, update main window
        private bool _isUpdatedEmployee;
        public bool isUpdatedEmployee
        {
            get
            {
                return _isUpdatedEmployee;
            }
            set
            {
                _isUpdatedEmployee = value;
            }
        }

        private bool isEditingWindow = false;
        #endregion

        #region lists
        private List<tblLocation> _locationList;
        public List<tblLocation> locationList
        {
            get
            {
                return _locationList;
            }
            set
            {
                _locationList = value;
                OnPropertyChanged("locationList");
            }
        }


        private List<tblGender> _genderList;
        public List<tblGender> genderList
        {
            get
            {
                return _genderList;
            }
            set
            {
                _genderList = value;
                OnPropertyChanged("genderList");
            }
        }

        private List<tblEmployee> _managerList;
        public List<tblEmployee> managerList
        {
            get
            {
                return _managerList;
            }
            set
            {
                _managerList = value;
                OnPropertyChanged("managerList");
            }
        }
        #endregion


        #region constructor
        /// <summary>
        /// Constructor for adding new employee
        /// </summary>
        public AddWindowViewModel(AddAndEditEmployeeWindow addEmployeeOpen)
        {
            addEmployee = addEmployeeOpen;
            newEmployee = new vwEmployee();
            locationList = Service.Service.GetAllLocations();
            managerList = Service.Service.GetAllManagers();
            genderList = Service.Service.GetAllGenders();
            backgroundWorker.DoWork += DoWorkAdd;
        }
        /// <summary>
        /// Constructor for updating employee
        /// </summary>
        public AddWindowViewModel(AddAndEditEmployeeWindow addEmployeeOpen, vwEmployee employee )
        {
            isEditingWindow = true;
            addEmployee = addEmployeeOpen;
            newEmployee = employee;
            locationList = Service.Service.GetAllLocations();
            managerList = Service.Service.GetAllManagers(employee.employeeId);
            genderList = Service.Service.GetAllGenders();
            backgroundWorker.DoWork += DoWorkAdd;
        }
        #endregion

        #region BackgroundWorkers's DoWork event handler
        public void DoWorkAdd(object sender, DoWorkEventArgs e)
        {
            string content = "Employee " + newEmployee.fullname + " with employeeId " + newEmployee.employeeId;
            if (isEditingWindow)
            {
                content +=  " has been updated.";
            }
            else
            {
                content += " has been added.";
            }
            
            LogIntoFile.getInstance().PrintActionIntoFile(content);
        }
        #endregion


        #region Commands

        private ICommand _save;
        public ICommand save
        {
            get
            {
                if (_save == null)
                {
                    _save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                
                return _save;
            }
        }

        private void SaveExecute()
        {
            try
            {
                Service.Service.AddNewEmployee(newEmployee);
                //run backgroundworker
                backgroundWorker.RunWorkerAsync();
                isUpdatedEmployee = true;
                addEmployee.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //save when all fileds(except manager) are filled
        private bool CanSaveExecute()
        {
            if (!String.IsNullOrEmpty(newEmployee.fullname) && newEmployee.dateOfBirth != null && !String.IsNullOrEmpty(newEmployee.jmbg) && !String.IsNullOrEmpty(newEmployee.IdentityCardNumber) && !String.IsNullOrEmpty(newEmployee.phone) && newEmployee.getTblLocation != null && !String.IsNullOrEmpty(newEmployee.sectorName) && newEmployee.getTblGender !=null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private ICommand _close;
        public ICommand close
        {
            get
            {
                if (_close == null)
                {
                    _close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return _close;
            }
        }

        private void CloseExecute()
        {
            try
            {
                addEmployee.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CanCloseExecute()
        {
            return true;
        }
        #endregion
    }

 
}
