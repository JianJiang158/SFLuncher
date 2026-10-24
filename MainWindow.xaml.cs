using System;
using System.Diagnostics; // 用于打开浏览器
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation; // 用于 Hyperlink 事件

namespace LauncherApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // --- 窗口拖动逻辑 ---
        private void DragWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        // --- 最小化逻辑 ---
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // --- 关闭逻辑 ---
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // --- 启动游戏逻辑 (演示用) ---
        private async void OnLaunchClick(object sender, RoutedEventArgs e)
        {
            // 安全获取选中项
            var selectedItem = VersionBox.SelectedItem as ComboBoxItem;
            string version = selectedItem?.Content?.ToString() ?? "未知版本";
            
            StatusLabel.Text = $"正在检查 Minecraft {version} ...";
            await Task.Delay(1000); 
            
            StatusLabel.Text = "✅ 资源校验通过，准备启动 Java 进程...";
            await Task.Delay(1000);

            MessageBox.Show($"成功启动 {version}!\n(此为演示模式，未实际运行游戏)", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            
            StatusLabel.Text = "🟢 游戏运行中...";
        }

        // --- 下载按钮点击逻辑 ---
        private async void OnDownloadClick(object sender, RoutedEventArgs e)
        {
            // 获取触发事件的 Button
            var btn = sender as Button;
            // 获取该 Button 所在的 ListViewItem 中的数据上下文
            var dataContext = btn?.DataContext as GameVersionInfo;
            
            if (dataContext != null && !string.IsNullOrEmpty(dataContext.Version))
            {
                // 简单修改 UI 状态示意正在下载
                // 注意：在非 MVVM 模式下，直接修改对象属性不会自动刷新界面文字
                // 这里我们主要展示交互流程，实际项目中需要绑定或手动刷新
                
                MessageBox.Show($"开始下载 {dataContext.Version} ...\n(模拟耗时 2秒)", "下载任务");
                
                await Task.Delay(2000);
                
                // 更新状态
                dataContext.Status = "已安装";
                
                // 强制刷新 ListView (Code-Behind 小技巧)
                // 由于没有 INotifyPropertyChanged，我们需要重新赋值 ItemsSource 或者重启程序才能看到变化
                // 对于 Demo，我们可以弹窗告知用户“状态已更新，请重启查看”
                MessageBox.Show($"{dataContext.Version} 下载完成！\n(注：因未使用MVVM，列表显示需重启程序才更新)", "完成");
            }
        }

        // --- 超链接点击逻辑 ---
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // 使用系统默认浏览器打开链接
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true });
            e.Handled = true;
        }
    }
}