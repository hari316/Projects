using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSmart_BL.iSmart
{
    public class iSmart_InputEntity
    {
        // TODO: Add constructor logic here 
        private List<iSmart_UpdateInputEntity> _obj;
        //iSmart_UpdateInputEntity _obj;
        public List<iSmart_UpdateInputEntity> obj
        {

            get { return _obj; }

            set { _obj = value; }

        }
    }       
   
}
