using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainWindow
{
    class DataViewModel
    {
        public DataViewModel() {
            _data = new Data();
        }

        private Data _data;

        public Data Data
        {
            get { return _data; }
        }

    }
}
