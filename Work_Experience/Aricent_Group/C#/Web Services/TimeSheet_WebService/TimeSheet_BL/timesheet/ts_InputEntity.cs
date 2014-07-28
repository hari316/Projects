using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeSheet_BL.timesheet
{
    public class ts_InputEntity
    {
        public ts_InputEntity()
        {
            // TODO: Add constructor logic here 
        }

        private List<ts_UpdateInputEntity> _obj;

        public List<ts_UpdateInputEntity> obj
        {

            get { return _obj; }

            set { _obj = value; }

        }
    }
}
