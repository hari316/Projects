﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.1.
// 
#pragma warning disable 1591

namespace TRT_BL.TaxiEmpDetails {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ServiceSoap", Namespace="http://tempuri.org/")]
    public partial class TRTService : System.Web.Services.Protocols.SoapHttpClientProtocol
    {
        
        private System.Threading.SendOrPostCallback GetAllChargeCodesOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetAllocatedChargeCodesOperationCompleted;
        
        private System.Threading.SendOrPostCallback GetEmployeeDetailsOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public TRTService() {
            this.Url = global::TRT_BL.Properties.Settings.Default.TRT_BL_TaxiEmpDetails_Service;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event GetAllChargeCodesCompletedEventHandler GetAllChargeCodesCompleted;
        
        /// <remarks/>
        public event GetAllocatedChargeCodesCompletedEventHandler GetAllocatedChargeCodesCompleted;
        
        /// <remarks/>
        public event GetEmployeeDetailsCompletedEventHandler GetEmployeeDetailsCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetAllChargeCodes", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetAllChargeCodes(string ProjectID) {
            object[] results = this.Invoke("GetAllChargeCodes", new object[] {
                        ProjectID});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetAllChargeCodesAsync(string ProjectID) {
            this.GetAllChargeCodesAsync(ProjectID, null);
        }
        
        /// <remarks/>
        public void GetAllChargeCodesAsync(string ProjectID, object userState) {
            if ((this.GetAllChargeCodesOperationCompleted == null)) {
                this.GetAllChargeCodesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAllChargeCodesOperationCompleted);
            }
            this.InvokeAsync("GetAllChargeCodes", new object[] {
                        ProjectID}, this.GetAllChargeCodesOperationCompleted, userState);
        }
        
        private void OnGetAllChargeCodesOperationCompleted(object arg) {
            if ((this.GetAllChargeCodesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAllChargeCodesCompleted(this, new GetAllChargeCodesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetAllocatedChargeCodes", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetAllocatedChargeCodes(string EmployeeNumber) {
            object[] results = this.Invoke("GetAllocatedChargeCodes", new object[] {
                        EmployeeNumber});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetAllocatedChargeCodesAsync(string EmployeeNumber) {
            this.GetAllocatedChargeCodesAsync(EmployeeNumber, null);
        }
        
        /// <remarks/>
        public void GetAllocatedChargeCodesAsync(string EmployeeNumber, object userState) {
            if ((this.GetAllocatedChargeCodesOperationCompleted == null)) {
                this.GetAllocatedChargeCodesOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetAllocatedChargeCodesOperationCompleted);
            }
            this.InvokeAsync("GetAllocatedChargeCodes", new object[] {
                        EmployeeNumber}, this.GetAllocatedChargeCodesOperationCompleted, userState);
        }
        
        private void OnGetAllocatedChargeCodesOperationCompleted(object arg) {
            if ((this.GetAllocatedChargeCodesCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetAllocatedChargeCodesCompleted(this, new GetAllocatedChargeCodesCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetEmployeeDetails", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string GetEmployeeDetails(string EmployeeNumber) {
            object[] results = this.Invoke("GetEmployeeDetails", new object[] {
                        EmployeeNumber});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void GetEmployeeDetailsAsync(string EmployeeNumber) {
            this.GetEmployeeDetailsAsync(EmployeeNumber, null);
        }
        
        /// <remarks/>
        public void GetEmployeeDetailsAsync(string EmployeeNumber, object userState) {
            if ((this.GetEmployeeDetailsOperationCompleted == null)) {
                this.GetEmployeeDetailsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetEmployeeDetailsOperationCompleted);
            }
            this.InvokeAsync("GetEmployeeDetails", new object[] {
                        EmployeeNumber}, this.GetEmployeeDetailsOperationCompleted, userState);
        }
        
        private void OnGetEmployeeDetailsOperationCompleted(object arg) {
            if ((this.GetEmployeeDetailsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetEmployeeDetailsCompleted(this, new GetEmployeeDetailsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetAllChargeCodesCompletedEventHandler(object sender, GetAllChargeCodesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAllChargeCodesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAllChargeCodesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetAllocatedChargeCodesCompletedEventHandler(object sender, GetAllocatedChargeCodesCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetAllocatedChargeCodesCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetAllocatedChargeCodesCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void GetEmployeeDetailsCompletedEventHandler(object sender, GetEmployeeDetailsCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetEmployeeDetailsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal GetEmployeeDetailsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591