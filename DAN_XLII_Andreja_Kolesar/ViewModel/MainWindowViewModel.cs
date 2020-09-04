using DAN_XLII_Andreja_Kolesar.Command;
using DAN_XLII_Andreja_Kolesar.Service;
using DAN_XLII_Andreja_Kolesar.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace DAN_XLII_Andreja_Kolesar.View
{
    class MainWindowViewModel : ViewModelBase
    {
        MainWindow main;

        #region Property
        BackgroundWorker backgroundWorker = new BackgroundWorker();

        private List<vwEmployee> _employeesList;
        public List<vwEmployee> employeesList
        {
            get
            {
                return _employeesList;
            }
            set
            {
                _employeesList = value;
                OnPropertyChanged("employeesList");
            }
        }

        private vwEmployee _viewEmployee;
        public vwEmployee viewEmployee
        {
            get
            {
                return _viewEmployee;
            }
            set
            {
                _viewEmployee = value;
                OnPropertyChanged("viewEmployee");
            }
        }
        private bool _isDeletedEmployee;
        public bool isDeletedEmployee
        {
            get
            {
                return _isDeletedEmployee;
            }
            set
            {
                _isDeletedEmployee = value;
            }
        }
        #endregion

        #region Constructor
        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
            Service.Service.AddLocationsToDb();
            employeesList = Service.Service.GetAllEmployees();
            // adding method to DoWork event
            backgroundWorker.DoWork += DoWorkDelete;
        }
        #endregion

        #region BackgroundWorkers's DoWork event handler
        public void DoWorkDelete(object sender, DoWorkEventArgs e)
        {
            vwEmployee emp = viewEmployee;
            string content = "Employee " + viewEmployee.fullname + " with employeeId " + viewEmployee.employeeId + " has been deleted.";
            LogIntoFile.getInstance().PrintActionIntoFile(content);

            //log update for every employee who's manager has ben deleted
            List<tblEmployee> managersEmployee = Service.Service.GetManagersEmployees(emp.employeeId);
            for(int i = 0; i <managersEmployee.Count; i++)
            {
                content = "Employee " + managersEmployee[i].fullname + " with employeeId " + managersEmployee[i].employeeId + " has been updated.";
                LogIntoFile.getInstance().PrintActionIntoFile(content);
            }
        }
        #endregion

        #region delete
        //Open messagebox where user can confirm deleting data
        private ICommand _deleteThisEmployee;
        public ICommand deleteThisEmployee
        {
            get
            {
                if (_deleteThisEmployee == null)
                {
                    _deleteThisEmployee = new RelayCommand(param => DeleteThisUserExecute(), param => CanDeleteThisUserExecute());

                }
                return _deleteThisEmployee;
            }
        }

        private void DeleteThisUserExecute()
        {
            //print confirm message
            MessageBoxResult result = MessageBox.Show("Do you realy want to delete this employee?", "Delete Employee", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Service.Service.DeleteEmployee(viewEmployee.employeeId);
                //run backgroundWorker
                backgroundWorker.RunWorkerAsync();
                isDeletedEmployee = true;
                employeesList = Service.Service.GetAllEmployees();
            }
        }
        private bool CanDeleteThisUserExecute()
        {
            return true;
        }

        #endregion

        #region update

        //open window for editing user's data
        private ICommand _editThisEmployee;
        public ICommand editThisEmployee
        {
            get
            {
                if (_editThisEmployee == null)
                {
                    _editThisEmployee = new RelayCommand(param => EditThisUserExecute(), param => CanEditThisUserExecute());
                }
                return _editThisEmployee;
            }
        }

        private void EditThisUserExecute()
        {
            try
            {
                AddAndEditEmployeeWindow editEmployee = new AddAndEditEmployeeWindow(viewEmployee);
                editEmployee.ShowDialog();
                if ((editEmployee.DataContext as AddWindowViewModel).isUpdatedEmployee == true)
                {
                    employeesList = Service.Service.GetAllEmployees();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanEditThisUserExecute()
        {
            return true;
        }

        #endregion

        #region add employee
        //open AddEmployee window
        private ICommand _addNewEmployee;
        public ICommand addNewEmployee
        {
            get
            {
                if (_addNewEmployee == null)
                {
                    _addNewEmployee = new RelayCommand(param => AddNewEmployeeExecute(), param => CanAddNewEmployeeExecute());
                }
                return _addNewEmployee;
            }
        }

        private void AddNewEmployeeExecute()
        {
            try
            {
                AddAndEditEmployeeWindow addEmployee = new AddAndEditEmployeeWindow();
                addEmployee.ShowDialog();
                if ((addEmployee.DataContext as AddWindowViewModel).isUpdatedEmployee == true)
                {
                    employeesList = Service.Service.GetAllEmployees();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanAddNewEmployeeExecute()
        {
            return true;
        }
    }
    #endregion
}