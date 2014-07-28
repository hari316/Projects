using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCT_BL.SCT
{
    public class SCT_Entity
    {

        public SCT_Entity()
        {
            // TODO: Add constructor logic here 
        }

        int _StatusFlag = 0;
        string _StatusMsg = string.Empty;

        string _EmpId = string.Empty;
        string _CurrBUId = string.Empty;
        string _CurrSBUId = string.Empty;
        string _CurrHorizontalId = string.Empty;
        string _CurrOrgId = string.Empty;
        string _CurrCostId = string.Empty;
        string _CurrPersonalId = string.Empty;
        string _CurrSubAreaId = string.Empty;
        string _CurrCDeployId = string.Empty;
        string _CurrBLocId = string.Empty;

        string _CurrentBU = string.Empty;
        string _CurrentSBU = string.Empty;
        string _CurrentHorizontal = string.Empty;
        string _CurrentOrg = string.Empty;
        string _CurrentCost = string.Empty;
        string _CurrentPersonal = string.Empty;
        string _CurrentSubArea = string.Empty;
        string _CurrentGrade = string.Empty;
        string _CurrCDeploy = string.Empty;
        string _CurrBLocation = string.Empty;


        public int StatusFlag
        {

            get { return _StatusFlag; }

            set { _StatusFlag = value; }

        }

        public string StatusMsg
        {

            get { return _StatusMsg; }

            set { _StatusMsg = value; }

        }

        public string EmpId
        {

            get { return _EmpId; }

            set { _EmpId = value; }

        }

        public string CurrBUId
        {

            get { return _CurrBUId; }

            set { _CurrBUId = value; }

        }


        public string CurrentBU
        {

            get { return _CurrentBU; }

            set { _CurrentBU = value; }

        }

        public string CurrSBUId
        {

            get { return _CurrSBUId; }

            set { _CurrSBUId = value; }

        }

        public string CurrentSBU
        {

            get { return _CurrentSBU; }

            set { _CurrentSBU = value; }

        }

        public string CurrHorizontalId
        {

            get { return _CurrHorizontalId; }

            set { _CurrHorizontalId = value; }

        }

        public string CurrentHorizontal
        {

            get { return _CurrentHorizontal; }

            set { _CurrentHorizontal = value; }

        }

        public string CurrOrgId
        {

            get { return _CurrOrgId; }

            set { _CurrOrgId = value; }

        }

        public string CurrentOrg
        {

            get { return _CurrentOrg; }

            set { _CurrentOrg = value; }

        }

        public string CurrCostId
        {

            get { return _CurrCostId; }

            set { _CurrCostId = value; }

        }

        public string CurrentCost
        {

            get { return _CurrentCost; }

            set { _CurrentCost = value; }

        }

        public string CurrPersonalId
        {

            get { return _CurrPersonalId; }

            set { _CurrPersonalId = value; }

        }

        public string CurrentPersonal
        {

            get { return _CurrentPersonal; }

            set { _CurrentPersonal = value; }

        }

        public string CurrSubAreaId
        {

            get { return _CurrSubAreaId; }

            set { _CurrSubAreaId = value; }

        }

        public string CurrentSubArea
        {

            get { return _CurrentSubArea; }

            set { _CurrentSubArea = value; }

        }

        public string CurrentGrade
        {

            get { return _CurrentGrade; }

            set { _CurrentGrade = value; }

        }

        public string CurrBLocId
        {

            get { return _CurrBLocId; }

            set { _CurrBLocId = value; }

        }

        public string CurrBLocation
        {

            get { return _CurrBLocation; }

            set { _CurrBLocation = value; }

        }

        public string CurrCDeployId
        {

            get { return _CurrCDeployId; }

            set { _CurrCDeployId = value; }

        }

        public string CurrCDeploy
        {

            get { return _CurrCDeploy; }

            set { _CurrCDeploy = value; }

        }    


    }
 
}
