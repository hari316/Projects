using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace iSmart_BL.iSmart
{
    public class iSmartEntity
    {

        public headerDetails IS_headerDetails;
        public itemDetails[] IS_child_itemDetails;
        public purchaseGroupMembers[] IS_child_purchaseGroupMembers;
        public historyDetails[] IS_child_historyDetails;
        public SendForClarification[] IS_child_sendForClrDetails;

        public iSmartEntity()
        {
            // TODO: Add constructor logic here  
            IS_headerDetails = new headerDetails();
        }

        public iSmartEntity(int itemDetails_count,int pGrpM_count,int historyDetails_count,int sendForClr_count)
        {
            // TODO: Add constructor logic here  
             IS_headerDetails = new headerDetails();
             IS_child_itemDetails = new itemDetails[itemDetails_count];
             IS_child_purchaseGroupMembers = new purchaseGroupMembers[pGrpM_count];
             IS_child_historyDetails = new historyDetails[historyDetails_count];
             IS_child_sendForClrDetails = new SendForClarification[sendForClr_count];
        }

        public class headerDetails
        {
            public headerDetails()
            {
                // TODO: Add constructor logic here 
            }

            string _PReqNo = string.Empty;
            string _Capex_Revenue = string.Empty;
            string _BorneBy = string.Empty;
            string _ProjectID = string.Empty;
            double _TotalUSDValue= 0;
            string _PReqRemarks = string.Empty;
            string _RequestedBy = string.Empty;
            string _RequestedDate = string.Empty;
            string _ProjectEndDate = string.Empty;           
            string _CompanyCode = string.Empty;           
            string _BU = string.Empty;
            string _AcceptanceCriteria = string.Empty;
            string _AssignTo = string.Empty;
            string _ActionByEmpNum = string.Empty;
            string _ActionByRole = string.Empty;
            int _ErrorCode = 0;
            string _ErrorMessage = string.Empty;


            public string PReqNo
            {

                get { return _PReqNo; }

                set { _PReqNo = value; }

            }

            public string Capex_Revenue
            {

                get { return _Capex_Revenue; }

                set { _Capex_Revenue = value; }

            }

            public string BorneBy
            {

                get { return _BorneBy; }

                set { _BorneBy = value; }

            }

            public string ProjectID
            {

                get { return _ProjectID; }

                set { _ProjectID = value; }

            }

            public double TotalUSDValue
            {
                get { return _TotalUSDValue; }

                set { _TotalUSDValue = value; }
            }

            public string PReqRemarks
            {

                get { return _PReqRemarks; }

                set { _PReqRemarks = value; }

            }

            public string RequestedBy
            {

                get { return _RequestedBy; }

                set { _RequestedBy = value; }

            }

            public string RequestedDate
            {

                get { return _RequestedDate; }

                set { _RequestedDate = value; }

            }

            public string ProjectEndDate
            {

                get { return _ProjectEndDate; }

                set { _ProjectEndDate = value; }

            }

            public string CompanyCode
            {

                get { return _CompanyCode; }

                set { _CompanyCode = value; }

            }

            public string BU
            {
                get { return _BU; }

                set { _BU = value; }
            }

            public string AcceptanceCriteria
            {

                get { return _AcceptanceCriteria; }

                set { _AcceptanceCriteria = value; }

            }

            public string AssignTo
            {

                get { return _AssignTo; }

                set { _AssignTo = value; }

            }

            public String ActionByEmpNum
            {

                get { return _ActionByEmpNum; }

                set { _ActionByEmpNum = value; }

            }

            public String ActionByRole
            {

                get { return _ActionByRole; }

                set { _ActionByRole = value; }

            }

            public int ErrorCode
            {

                get { return _ErrorCode; }

                set { _ErrorCode = value; }

            }

            public string ErrorMessage
            {

                get { return _ErrorMessage; }

                set { _ErrorMessage = value; }

            }


        }

        public class itemDetails
        {
            
            public itemDetails()
            {
                // TODO: Add constructor logic here 
            }

            int _ItemTransactionId = 0;
            string _ItemDescription = string.Empty;
            string _ItemSpecification = string.Empty;
            int _Quantity = 0;
            string _UOM = string.Empty;
            double _UnitRate = 0;
            double _TotalCost = 0;
            double _TotalCost_USD = 0;
            DateTime _RequiredFromDate = DateTime.MinValue;
            DateTime _RequiredTillDate = DateTime.MinValue;
            string _Plant = string.Empty;
            string _CostCenter = string.Empty;
            string _GLAccount = string.Empty;


            public int ItemTransactionId
            {

                get { return _ItemTransactionId; }

                set { _ItemTransactionId = value; }

            }
            public string ItemDescription
            {

                get { return _ItemDescription; }

                set { _ItemDescription = value; }

            }

            public string ItemSpecification
            {

                get { return _ItemSpecification; }

                set { _ItemSpecification = value; }

            }

            public int Quantity
            {

                get { return _Quantity; }

                set { _Quantity = value; }

            }

            public string UOM
            {

                get { return _UOM; }

                set { _UOM = value; }

            }

            public double UnitRate
            {

                get { return _UnitRate; }

                set { _UnitRate = value; }

            }

            public double TotalCost
            {

                get { return _TotalCost; }

                set { _TotalCost = value; }

            }

            public double TotalCost_USD
            {

                get { return _TotalCost_USD; }

                set { _TotalCost_USD = value; }

            }

            public DateTime RequiredFromDate
            {

                get { return _RequiredFromDate; }

                set { _RequiredFromDate = value; }

            }


            public DateTime RequiredTillDate
            {

                get { return _RequiredTillDate; }

                set { _RequiredTillDate = value; }

            }

            public string Plant
            {

                get { return _Plant; }

                set { _Plant = value; }

            }

            public string CostCenter
            {

                get { return _CostCenter; }

                set { _CostCenter = value; }

            }

            public string GLAccount
            {

                get { return _GLAccount; }

                set { _GLAccount = value; }

            }

        }

        public class purchaseGroupMembers
        {
            public purchaseGroupMembers()
            {
                // TODO: Add constructor logic here 
            }

            double _EmpID = 0;
            string _EmpName = string.Empty;

            public double EmpID
            {

                get { return _EmpID; }

                set { _EmpID = value; }

            }

            public string EmpName
            {

                get { return _EmpName; }

                set { _EmpName = value; }

            }

        }

        public class historyDetails
        {
            public historyDetails()
            {
                // TODO: Add constructor logic here 
            }

            string _EmpID = string.Empty;
            string _EmpName = string.Empty;
            string _Role = string.Empty;
            string _UpdatedDate = string.Empty;
            string _ActionPerformed = string.Empty;
            string _Remarks = string.Empty;

            public string EmpID
            {

                get { return _EmpID; }

                set { _EmpID = value; }

            }

            public string EmpName
            {

                get { return _EmpName; }

                set { _EmpName = value; }

            }

            public string Role
            {

                get { return _Role; }

                set { _Role = value; }

            }

            public string UpdatedDate
            {

                get { return _UpdatedDate; }

                set { _UpdatedDate = value; }

            }

            public string ActionPerformed
            {

                get { return _ActionPerformed; }

                set { _ActionPerformed = value; }

            }

            public string Remarks
            {

                get { return _Remarks; }

                set { _Remarks = value; }

            }

        }

        public class SendForClarification
        {
            public SendForClarification()
            {
                // TODO: Add constructor logic here 
            }

            string _RoleCode = string.Empty;
            string _RoleDescription = string.Empty;

            public string RoleCode
            {

                get { return _RoleCode; }

                set { _RoleCode = value; }

            }

            public string RoleDescription
            {

                get { return _RoleDescription; }

                set { _RoleDescription = value; }

            }

        }

    }
 
}
