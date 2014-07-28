using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSmart_BL.iSmart
{
    public class iSmart_UpdateInputEntity
    {

        public iSmart_UpdateInputEntity()
        {
            // TODO: Add constructor logic here 
        }     

        string _PReqNo = string.Empty;
        int _Status = 0;
        string _EmpID = string.Empty;
        string _PurGrpMember = string.Empty;
        string _ItemTransactionId = string.Empty;
        string _AucNo = string.Empty;
        string _CustomerPONum = string.Empty;
        string _PReqRemarks = string.Empty;
        string _RejectedTo = string.Empty;
        string _AssignTo = string.Empty;
        string _ActionByRole = string.Empty;

        public string PReqNo
        {

            get { return _PReqNo; }

            set { _PReqNo = value; }

        }

        public int Status
        {

            get { return _Status; }

            set { _Status = value; }

        }

        public string EmpID
        {

            get { return _EmpID; }

            set { _EmpID = value; }

        }

        public string PurGrpMember
        {

            get { return _PurGrpMember; }

            set { _PurGrpMember = value; }

        }

        public String ItemTransactionId
        {

            get { return _ItemTransactionId; }

            set { _ItemTransactionId = value; }

        }

        public String AucNo
        {

            get { return _AucNo; }

            set { _AucNo = value; }

        }

        public String CustomerPONum
        {

            get { return _CustomerPONum; }

            set { _CustomerPONum = value; }

        }

        public String PReqRemarks
        {

            get { return _PReqRemarks; }

            set { _PReqRemarks = value; }

        }

        public String RejectedTo
        {

            get { return _RejectedTo; }

            set { _RejectedTo = value; }

        }

        public String AssignTo
        {

            get { return _AssignTo; }

            set { _AssignTo = value; }

        }

        public String ActionByRole
        {

            get { return _ActionByRole; }

            set { _ActionByRole = value; }

        }

    }

}
