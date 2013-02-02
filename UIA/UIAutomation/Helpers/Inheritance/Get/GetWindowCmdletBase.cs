﻿/*
 * Created by SharpDevelop.
 * User: Alexander Petrovskiy
 * Date: 19/01/2012
 * Time: 11:32 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

namespace UIAutomation
{
    using System;
    using System.Management.Automation;
    using System.Diagnostics;

    /// <summary>
    /// Description of GetWindowCmdletBase.
    /// </summary>
    public class GetWindowCmdletBase : GetCmdletBase
    {
        #region Constructor
        public GetWindowCmdletBase()
        {
            //WriteVerbose(this, "constructor");
            
            // 20120824
            //this.ProcessName = String.Empty;
            // Title = String.Empty;
            // 20120824
            //this.Name = String.Empty;
            // 20120824
            //this.ProcessId = 0;
            // 20120824
            //this.InputObject = null;
        }
        #endregion Constructor
        
        #region Parameters
        [Parameter(Mandatory = false,
                   //ParameterSetName = "Window",
                   HelpMessage="Accepts the name of a process")]
        [Alias("pn")]
        public string[] ProcessName { get; set; }
        
        
        [Parameter(Mandatory = false,
                   //ParameterSetName = "Window",
                   HelpMessage="Accepts the name (title) of a window")]
        [Alias("Title")]
        public string[] Name { get; set; }
        
        
        [Parameter(Mandatory = false,
                   ValueFromPipeline = true,
                   HelpMessage="Accepts a process")]
        [Alias("Process", "p")]
        public new Process[] InputObject { get; set; }
        
        
        [Parameter(Mandatory = false,
                   //ParameterSetName = "Window",
                   HelpMessage="Accepts the Id of a process (PID)")]
        [Alias("pid")]
        public int[] ProcessId { get; set; }
        
        
        [Parameter(Mandatory = false,
                   HelpMessage="The parameter is not used and for the compatibility purpose")]
        [Alias("temporary", "fake")]
        // 20120206 public new string AutomationId { get; set; }
        public string AutomationId { get; set; }
        
        
        [Parameter(Mandatory = false,
                   HelpMessage="The parameter is not used and for the compatibility purpose")]
        [Alias("oneMoreFakeParameter")]
        public string Class { get; set; }
        
//        [Parameter(Mandatory = false)]
//        internal new System.Windows.Automation.AutomationElement InputObject { get; set; }
        #endregion Parameters
    }
}