namespace CCrystal {
    public class Processor : CrystalDecisions.Windows.Forms.CrystalReportViewer {
        Report _handler;
        public Processor(Report handler) {
            this.ReportRefresh += Processor_ReportRefresh;
            _handler = handler;
        }

        private void Processor_ReportRefresh(object source, CrystalDecisions.Windows.Forms.ViewerEventArgs e) {
            if (_handler != null) {
                if (_handler.OnExternlaHandling("refresh", null, CModels.Enums.ActionTypeList.Result))
                    e.Handled = true;
            }
        }
    }
}
