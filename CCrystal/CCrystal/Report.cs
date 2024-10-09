using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using CModels;

namespace CCrystal {
    public class Report : IDisposable, ICMReport {
        ReportDocument _objReport = null;

        public event System.EventHandler ExternlaHandling;

        protected bool _blnConnectionsSet = false;
        protected bool _blnParametersSet = false;

        #region Properties
        public ReportDocument Document {
            get { return _objReport; }
            set { _objReport = value; }
        }

        public long LoadingTime { get; set; }
        public string Path { get; set; }
        public bool IsEmpty { get; set; }
        public bool DoNotRefresh { get; set; }
        public bool UseExternalSource { get; set; }

        public string Server { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public bool UseIntegratedSecurity { get; set; }
        public string TempFolder { get; set; }
        public bool IsInitialized {
            get { return (_objReport != null); }
        }

        public string Company {
            get { return ""; }
        }

        public string Version {
            get { return "1.0.0"; }
        }

        #endregion

        internal bool OnExternlaHandling(string action, object data = null, Enums.ActionTypeList type = Enums.ActionTypeList.Regular) {
            bool blnRet = false;
            try {
                if (this.ExternlaHandling != null) {
                    if (data == null) data = _objReport;
                    DataEventArgs ea = new DataEventArgs() { ActionType = type, Action = action, Data = data };
                    this.ExternlaHandling(this, ea);
                    switch (type) {
                        case Enums.ActionTypeList.Result:
                            if (ea.Data is bool)
                                blnRet = (bool)ea.Data;
                            break;
                        default:
                            blnRet = true;
                            break;
                    }
                }
            } catch (Exception) {
                throw;
            }

            return blnRet;
        }

        public void Init(string path) {
            try {
                OnExternlaHandling("reset");
                var watch = System.Diagnostics.Stopwatch.StartNew();
                this.Path = path;
                LoadReportFile(path, OpenReportMethod.OpenReportByTempCopy);
                this.LoadingTime = watch.ElapsedMilliseconds;
            } catch (Exception) {
                throw;
            }
        }

        public string Execute(Enums.ExportTypeList type) {
            string strRet = string.Empty;
            try {
                if (_objReport == null) {
                    Init(this.Path);
                }

                if (!this.DoNotRefresh) {
                    //set connection data
                    if (!this.UseExternalSource) {
                        if (!_blnConnectionsSet) {
                            if (!OnExternlaHandling("setconnection", null, Enums.ActionTypeList.Result)) {
                                SetConnection(ref _objReport, String.Empty, String.Empty, this.Server, this.Database, this.User, this.Password, this.UseIntegratedSecurity);
                            }
                            _blnConnectionsSet = true;
                        }
                    }

                    if (!_blnParametersSet) {
                        _blnParametersSet = OnExternlaHandling("updateparameters", null, Enums.ActionTypeList.Result);
                    }

                    string strTempFile = GetTempFile(type);
                    _objReport.ExportToDisk((ExportFormatType)type, strTempFile);


                    if (System.IO.File.Exists(strTempFile))
                        strRet = strTempFile;
                }
            } catch (System.Exception) {
                throw;
            }

            return strRet;
        }

        private string GetTempFile(Enums.ExportTypeList type) {
            string strRet = string.Empty;
            try {
                string strFileName = Guid.NewGuid().ToString("N");
                string strFolderName = string.Empty;
                if (string.IsNullOrEmpty(this.TempFolder))
                    strFolderName = System.IO.Path.GetTempPath();
                else
                    strFolderName = this.TempFolder;

                string strExtension = string.Empty;

                switch (type) {
                    case Enums.ExportTypeList.CrystalReport:
                        strExtension = "rpt";
                        break;
                    case Enums.ExportTypeList.EditableRTF:
                    case Enums.ExportTypeList.RichText:
                        strExtension = "rtf";
                        break;
                    case Enums.ExportTypeList.WordForWindows:
                        strExtension = "doc";
                        break;
                    case Enums.ExportTypeList.Excel:
                        strExtension = "xls";
                        break;
                    case Enums.ExportTypeList.PortableDocFormat:
                        strExtension = "pdf";
                        break;
                    case Enums.ExportTypeList.HTML40:
                    case Enums.ExportTypeList.HTML32:
                        strExtension = "html";
                        break;
                    case Enums.ExportTypeList.ExcelRecord:
                        strExtension = "xls";
                        break;
                    case Enums.ExportTypeList.Text:
                        strExtension = "txt";
                        break;
                    case Enums.ExportTypeList.CharacterSeparatedValues:
                        strExtension = "csv";
                        break;
                    case Enums.ExportTypeList.TabSeperatedText:
                        strExtension = "tab";
                        break;
                    case Enums.ExportTypeList.Xml:
                        strExtension = "xml";
                        break;
                    case Enums.ExportTypeList.RPTR:
                        strExtension = "rptr";
                        break;
                    case Enums.ExportTypeList.ExcelWorkbook:
                        strExtension = "xlsx";
                        break;
                    default:
                        strExtension = "pdf";
                        break;
                }

                strRet = System.IO.Path.Combine(strFolderName, string.Format("{0}.{1}", strFileName, strExtension));
            } catch (Exception) {
                throw;
            }
            return strRet;
        }

        private void SetConnection(ref ReportDocument reportDoc, string subreport, string tableName, string server, string database, string user, string password, bool useIntegratedSecurity) {
            try {
                foreach (CrystalDecisions.CrystalReports.Engine.Table table in reportDoc.Database.Tables) {
                    if (string.IsNullOrEmpty(tableName) || string.CompareOrdinal(table.Name, tableName) == 0) {
                        TableLogOnInfo logonInfo = table.LogOnInfo;
                        ConnectionInfo cnOrigInfo = logonInfo.ConnectionInfo;

                        if (!string.IsNullOrEmpty(server)) logonInfo.ConnectionInfo.ServerName = server;
                        if (!string.IsNullOrEmpty(database)) logonInfo.ConnectionInfo.DatabaseName = database;
                        if (!string.IsNullOrEmpty(user)) logonInfo.ConnectionInfo.UserID = user;
                        if (!string.IsNullOrEmpty(password)) logonInfo.ConnectionInfo.Password = password;
                        logonInfo.ConnectionInfo.IntegratedSecurity = useIntegratedSecurity;
                        table.ApplyLogOnInfo(logonInfo);
                    }
                }

                if (string.IsNullOrEmpty(subreport)) {
                    foreach (Section sect in reportDoc.ReportDefinition.Sections) {
                        foreach (ReportObject objReport in sect.ReportObjects) {
                            if (objReport.Kind == ReportObjectKind.SubreportObject) {
                                SubreportObject objSubReport = (SubreportObject)objReport;
                                ReportDocument subReportDocument = objSubReport.OpenSubreport(objSubReport.SubreportName);
                                SetConnection(ref subReportDocument, objSubReport.SubreportName, tableName, server, database, user, password, useIntegratedSecurity);
                            }
                        }
                    }
                }
            } catch (Exception) {
                throw;
            }
        }

        protected bool LoadReportFile(string path, CrystalDecisions.Shared.OpenReportMethod useTempCopy) {
            bool blnRet = false;
            try {

                OnExternlaHandling("beforeloading");
                _objReport = new ReportDocument();
                _objReport.Load(path, useTempCopy);
                _objReport.NoData += _objReport_NoData;
                OnExternlaHandling("loaded");

                blnRet = (_objReport != null);
            } catch (Exception ex) {
                OnExternlaHandling("loadreportfile", ex, Enums.ActionTypeList.Error);
                throw;
            }
            return blnRet;
        }

        private void _objReport_NoData(FormatPageEventArgs e) {
            OnExternlaHandling("empty");
        }

        public void Dispose() {
            try {
                if (_objReport != null) {
                    _objReport.NoData -= _objReport_NoData;
                    OnExternlaHandling("disposing");
                    _objReport = null;
                }
            } catch (Exception) {
                throw;
            }
        }

        public void Clear() {
            try {
                if (_objReport != null) {
                    _objReport.NoData -= _objReport_NoData;
                    _objReport = null;
                }
            } catch (Exception) {
                throw;
            }

        }
        public dynamic Process(string code, string key, object data) {
            switch (code) {
                case "dispose":
                    Dispose();
                    return true;
                case "clear":
                    Clear();
                    return true;
                case "reset":
                    OnExternlaHandling("reset");
                    return true;
                case "range":
                    return new ParameterRangeValue();
                case "value":
                    return new ParameterDiscreteValue();
                case "processor":
                    return new Processor(this);
                case "data":
                    return CProcessing.Processor.GetValue(_objReport, key, data);
                default:
                    throw new Exception("Crystal reports plugin unknown request.");
            }
        }
    }
}
