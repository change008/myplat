using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myplat.UIWap.Models
{
    public class ReportModels
    {
        public string reportId
        {
            get; set;
        }

        public string reportContent
        {
            get; set;
        }

        public bool ifHasChild
        {
            get; set;
        }

        public string hrefUrl
        {
            get; set;
        }

        public ReportModels()
        {

        }

        private static ReportModels InitReportModels(string reportId, string reportContent, bool ifHasChild, string hrefUrl)
        {
            ReportModels model = new ReportModels();
            model.reportId = reportId;
            model.reportContent = reportContent;
            model.ifHasChild = ifHasChild;
            model.hrefUrl = hrefUrl;
            return model;
        }

        public static List<ReportModels> GetReportList()
        {
            List<ReportModels> list = new List<ReportModels>();
            list.Add(InitReportModels("01", "欺诈内容", false, "/home/reportresult?"));
            list.Add(InitReportModels("02", "色情内容", false, "/home/reportresult?"));
            list.Add(InitReportModels("07", "违规声明原创", false, "/home/reportresult?"));
            list.Add(InitReportModels("08", "未经授权的文章内容", false, "/home/reportresult?"));
            list.Add(InitReportModels("09", "侵权（冒充他人、侵犯名誉等）", false, "/home/reportresult?"));
            list.Add(InitReportModels("04", "存在不实信息", true, "/Home/ReportPage?rid=04&"));
            list.Add(InitReportModels("0401", "政治类不实信息", false, "/home/reportresult?"));
            list.Add(InitReportModels("0402", "医疗健康类不实信息", false, "/home/reportresult?"));
            list.Add(InitReportModels("0403", "恐怖荒谬类不实信息", false, "/home/reportresult?"));
            list.Add(InitReportModels("0404", "社会事件类不实信息", false, "/home/reportresult?"));
            list.Add(InitReportModels("03", "存在诱导行为", true, "/Home/ReportPage?rid=03&"));
            list.Add(InitReportModels("0301", "存在诱导关注行为", false, "/home/reportresult?"));
            list.Add(InitReportModels("0302", "存在诱导分享行为", false, "/home/reportresult?"));
            list.Add(InitReportModels("05", "涉嫌违法犯罪", false, "/home/reportresult?"));
            list.Add(InitReportModels("06", "存在骚扰行为", false, "/home/reportresult?"));
            list.Add(InitReportModels("99", "其他理由", false, "/home/reportelse?"));
            return list;
        }
    }
}
