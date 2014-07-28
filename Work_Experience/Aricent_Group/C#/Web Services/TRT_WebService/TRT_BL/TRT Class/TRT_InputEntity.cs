using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRT_BL
{
    public class TRT_InputEntity
    {

        public TRT_InputEntity()
        {
            // TODO: Add constructor logic here 
        }

        string _empNo = string.Empty;  
        string _location = string.Empty;
        string _plot = string.Empty;
        string _purpose = string.Empty;
        string _vehicleType = string.Empty;
        string _chargeCode = string.Empty;
        string _fromPlace = string.Empty;
        string _toPlace = string.Empty;
        string _landmark = string.Empty;
        string _contactNum = string.Empty;
        string _flightDetails = string.Empty;
        string _comments = string.Empty;
        string _reqDate = string.Empty;
        int _hhTime = 0;
        int _mmTime = 0;
        int _duration = 0;

        public string empNo
        {

            get { return _empNo; }

            set { _empNo = value; }

        }

        public string location
        {

            get { return _location; }

            set { _location = value; }

        }

        public string plot
        {

            get { return _plot; }

            set { _plot = value; }

        }

        public string purpose
        {

            get { return _purpose; }

            set { _purpose = value; }

        }

        public string vehicleType
        {

            get { return _vehicleType; }

            set { _vehicleType = value; }

        }

        public string chargeCode
        {

            get { return _chargeCode; }

            set { _chargeCode = value; }

        }

        public string fromPlace
        {

            get { return _fromPlace; }

            set { _fromPlace = value; }

        }

        public string toPlace
        {

            get { return _toPlace; }

            set { _toPlace = value; }

        }

        public string landmark
        {

            get { return _landmark; }

            set { _landmark = value; }

        }

        public string contactNum
        {

            get { return _contactNum; }

            set { _contactNum = value; }

        }

        public string flightDetails
        {

            get { return _flightDetails; }

            set { _flightDetails = value; }

        }

        public string comments
        {

            get { return _comments; }

            set { _comments = value; }

        }

        public string reqDate
        {

            get { return _reqDate; }

            set { _reqDate = value; }

        }

        public int hhTime
        {

            get { return _hhTime; }

            set { _hhTime = value; }

        }

        public int mmTime
        {

            get { return _mmTime; }

            set { _mmTime = value; }

        }

        public int duration
        {

            get { return _duration; }

            set { _duration = value; }

        } 

    }

}
