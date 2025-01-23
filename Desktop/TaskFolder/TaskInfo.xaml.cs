using Desktop.Repository;
using Entities;
using System.Windows;
using System.Windows.Controls;

namespace Desktop.TaskFolder
{
    public partial class TaskInfo : UserControl
    {
        public delegate void DeleteTaskItemDel(TaskDictionary task);
        public event DeleteTaskItemDel? DeleteTaskItem;

        private TaskDictionary? Task;

        public TaskInfo()
        {
            InitializeComponent();
        }

        public void InfoLoad(TaskDictionary? Task)
        {
            this.Task = Task;
            InitializeTaskInfo();
        }

        private void InitializeTaskInfo()
        {
            if (Task == null) return;

            TitleItem.Text = Task.Name;
            TimeItem.Text = Task.Time;
            DateItem.Text = Task.Date;
            SubtitleItem.Text = Task.Description;
            DoneB.Content = Task.IsCompleted ? "Не готово" : "Готово";
        }

        private void DoneB_Click(object sender, RoutedEventArgs e)
        {
            if (Task != null)
            {
                Task.IsCompleted = !Task.IsCompleted;
                TaskRepository.GetTaskRepository().RefreshTaskItems();
                DoneB.Content = Task.IsCompleted ? "Не готово" : "Готово";
                DeleteTaskItem?.Invoke(Task);
            }
        }

        private void DeleteB_Click(object sender, RoutedEventArgs e)
        {
            if (Task != null)
            {
                TaskRepository.RemoveTaskDictionary(Task);
                DeleteTaskItem?.Invoke(Task);
            }
        }
    }
}
