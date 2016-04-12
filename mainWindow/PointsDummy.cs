using System;
using System.ComponentModel;

namespace mainWindow
{
    class PointsDummy : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public PointsDummy()
        {
            _modelData1 = new ModelData();
            _modelData2 = new ModelData();
            _modelData3 = new ModelData();
            _modelData4 = new ModelData();
            _modelData5 = new ModelData();
        }







        #region private_fileds
        private ModelData _modelData1;
        private ModelData _modelData2;
        private ModelData _modelData3;
        private ModelData _modelData4;
        private ModelData _modelData5;
        #endregion

       

        public ModelData ModelData1
        {
            get { return _modelData1; }
            set { _modelData1 = value; OnPropertyChanged("ModelData1"); }
        }

        public ModelData ModelData2
        {
            get { return _modelData2; }
            set { _modelData2 = value; OnPropertyChanged("ModelData2"); }
        }

        public ModelData ModelData3
        {
            get { return _modelData3; }
            set { _modelData3 = value; OnPropertyChanged("ModelData3"); }
        }

        public ModelData ModelData4
        {
            get { return _modelData4; }
            set { _modelData4 = value; OnPropertyChanged("ModelData4"); }
        }

        public ModelData ModelData5
        {
            get { return _modelData5; }
            set { _modelData5 = value; OnPropertyChanged("ModelData5"); }
        }
    }
}
