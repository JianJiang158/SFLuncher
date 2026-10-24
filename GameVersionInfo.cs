using System.Windows.Media;

namespace LauncherApp
{
    // 用于 Download Tab 的数据模型
    public class GameVersionInfo
    {
        // 赋予初始空字符串，消除 CS8618 警告
        public string Version { get; set; } = "";
        public string Type { get; set; } = "";
        public string Status { get; set; } = "";
        
        // 计算属性，根据 Status 返回不同的颜色 Brush
        public Brush StatusColor 
        { 
            get 
            {
                if (Status == "已安装") return Brushes.LightGreen;
                if (Status == "未安装") return Brushes.OrangeRed;
                if (Status == "下载中...") return Brushes.Yellow;
                return Brushes.Gray;
            } 
        }
    }
}