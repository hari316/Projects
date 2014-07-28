using System;
using System.Configuration.Install;
using System.ServiceProcess;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Collections;

namespace ICOMSProvisioningService
{
   [RunInstaller(true)]
   public class ICOMSServiceInstaller : Installer
    {

       public ICOMSServiceInstaller()
      {
        var processInstaller = new ServiceProcessInstaller();
        var serviceInstaller = new ServiceInstaller();

        //set the privileges
        processInstaller.Account = ServiceAccount.LocalSystem;

        serviceInstaller.DisplayName = "ICOMS Provisioning Service";
        serviceInstaller.StartType = ServiceStartMode.Automatic;
        serviceInstaller.Description = "Translates ICOMS Message, Invokes CRM 4c REST Interface, Sends Response to ICOMS";
        //must be the same as what was set in Program's constructor
        serviceInstaller.ServiceName = "ICOMS Provisioning Service";

        this.Installers.Add(processInstaller);
        this.Installers.Add(serviceInstaller);
        this.AfterInstall += new InstallEventHandler(ServiceInstaller_AfterInstall);
      }
              

       void ServiceInstaller_AfterInstall(object sender, InstallEventArgs e)
       {
           string strFndSilentMode = this.Context.Parameters["UILevel"];
           // if the value is <= 3 then it it silent instalaltion 
           // In case of silent mode don't show the Custom GUI
           if (Convert.ToInt16(strFndSilentMode) > 3)
           {
                frmICOMSEditConfig frmEdtCnfg = new frmICOMSEditConfig();
                frmEdtCnfg.ShowDialog();
                frmEdtCnfg.Dispose();                         
           }

           ServiceController sc = new ServiceController("ICOMS Provisioning Service");
           sc.Start();  
       }

       protected override void OnAfterUninstall(IDictionary savedState)
       {
           base.OnAfterUninstall(savedState);
           // Must be passed in as a parameter  
           string targetDir = Context.Parameters["TargetDir"];
           if (targetDir.EndsWith("|"))
           {
               targetDir = targetDir.TrimEnd('|');
           }

           if (!targetDir.EndsWith("\\"))
           {
               targetDir += "\\";
           }

           string targetDirLogs = targetDir + "\\logs";
           if (!Directory.Exists(targetDir))
           {
               return;
           }

           string[] files = new[] { "ICOMSProvisioningService.InstallLog", "InstallUtil.InstallLog", "ICOMSProvisioningService.log" };
           string[] dirs = new[] { "logs" };

           foreach (string f in files)
           {
               string path = Path.Combine(targetDirLogs, f);
               if (File.Exists(path))
               {
                   File.Delete(path);
               }
           }

           foreach (string f in files)
           {
               string path = Path.Combine(targetDirLogs, f);
               if (File.Exists(path))
               {
                   File.Delete(path);
               }
           }

           foreach (string d in dirs)
           {
               string path = Path.Combine(targetDir, d);
               if (Directory.Exists(path))
               {
                   Directory.Delete(path, true);
               }
           }

           // At this point, all generated files and directories must be deleted.   
           // The installation folder will be removed automatically. 
       }
        

    }
}
