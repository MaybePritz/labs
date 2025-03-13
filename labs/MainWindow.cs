using System.Windows.Forms;

namespace labs
{
    public partial class MainWindow : Form
    {
        private ToolStrip toolStrip;
        private Panel contentPanel;

        public MainWindow()
        {
            InitializeComponent();
            InitializeHorizontalMenu();

            var aboutButton = toolStrip.Items[0] as ToolStripButton;
            if (aboutButton != null)
            {
                MenuItem_Click(aboutButton, EventArgs.Empty);
            }
        }

        private void InitializeHorizontalMenu()
        {
            contentPanel = new Panel();
            contentPanel.Dock = DockStyle.Fill;

            toolStrip = new ToolStrip();
            toolStrip.Dock = DockStyle.Top;
            toolStrip.GripStyle = ToolStripGripStyle.Hidden;

            string[] menuItems = { "� ���������", "�������", "�����" };
            foreach (var item in menuItems)
            {
                var toolStripButton = new ToolStripButton(item);
                toolStripButton.Tag = item;
                toolStripButton.Click += MenuItem_Click;
                toolStrip.Items.Add(toolStripButton);
            }

            Controls.Add(contentPanel);
            Controls.Add(toolStrip);
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            var clickedItem = sender as ToolStripButton;
            if (clickedItem == null) return;

            foreach (ToolStripItem item in toolStrip.Items)
            {
                item.BackColor = SystemColors.Control;
                item.Font = new Font(item.Font, FontStyle.Regular);
            }

            clickedItem.BackColor = SystemColors.ButtonHighlight;
            clickedItem.Font = new Font(clickedItem.Font, FontStyle.Bold);

            switch (clickedItem.Text)
            {
                case "� ���������":
                    LoadAboutContent();
                    break;
                case "�������":
                    LoadTaskContent();
                    break;
                case "�����":
                    ExitApplication();
                    break;
            }
        }

        private void ExitApplication()
        {
            DialogResult result = MessageBox.Show("�� ������������� ������ �����?",
                "������������� ������",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes) Application.Exit();
        }

        private void LoadAboutContent()
        {
            contentPanel.Controls.Clear();
            Panel aboutPanel = new Panel();
            aboutPanel.Dock = DockStyle.Fill;
            aboutPanel.AutoScroll = true;

            Label titleLabel = new Label();
            titleLabel.Text = "� ���������";
            titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(20, 20);

            Label versionLabel = new Label();
            versionLabel.Text = "������: ���� 1";
            versionLabel.Font = new Font("Segoe UI", 12F);
            versionLabel.AutoSize = true;
            versionLabel.Location = new Point(20, 60);

            Label authorLabel = new Label();
            authorLabel.Text = "�����: ���� ����";
            authorLabel.Font = new Font("Segoe UI", 12F);
            authorLabel.AutoSize = true;
            authorLabel.Location = new Point(20, 90);

            Label descriptionLabel = new Label();
            descriptionLabel.Text = "�������� ��������� � � ����������������:\n\n" +
                                    "��������� ������������� ������� ��������� ��� ������ �������,\n����� ������ � ����������� �����������.\n\n" +
                                    "�������� �����������:\n" +
                                    "- ���������� � ��������� � ������.\n" +
                                    "- ����� ������� �� ������.\n" +
                                    "- ���� ������ ��� ���������� ����������.\n" +
                                    "- ����������� ����������� ����������.\n\n" +
                                    "��� ������������ ����������:\n" +
                                    "1. �������� ������ \"�������\" � ���� �����.\n" +
                                    "2. �������� ������ ������� �� ������.\n" +
                                    "3. ������� ����������� ������ � ���� �����.\n" +
                                    "4. ������� ������ \"���������\" ��� ��������� ����������.\n" +
                                    "5. ��� �������� � ���� ������� \"��������� � ������ �������\".\n\n";
            descriptionLabel.Font = new Font("Segoe UI", 12F);
            descriptionLabel.AutoSize = true;
            descriptionLabel.Location = new Point(20, 130);

            aboutPanel.Controls.Add(titleLabel);
            aboutPanel.Controls.Add(versionLabel);
            aboutPanel.Controls.Add(authorLabel);
            aboutPanel.Controls.Add(descriptionLabel);

            contentPanel.Controls.Add(aboutPanel);
        }

        private void LoadTaskContent()
        {
            Tasks tasks = new Tasks();
            contentPanel.Controls.Clear();
            Panel taskPanel = new Panel();
            taskPanel.Dock = DockStyle.Fill;

            Label titleLabel = new Label();
            titleLabel.Text = "����� �������";
            titleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(20, 20);
            taskPanel.Controls.Add(titleLabel);

            string[][] tasksObj = tasks.TaskObj;
            int buttonWidth = 200;
            int buttonHeight = 30;
            int verticalPosition = 60;

            for (int i = 0; i < tasksObj.Length; i++)
            {
                if (tasksObj[i].Length < 2)
                    continue;

                Button taskButton = new Button();
                taskButton.Text = tasksObj[i][1];
                taskButton.Name = "btnTask" + i;
                taskButton.Size = new Size(buttonWidth, buttonHeight);
                taskButton.Location = new Point(20, verticalPosition);
                taskButton.Tag = Convert.ToInt32(tasksObj[i][0]);
                taskButton.Click += TaskButton_Click;
                taskButton.UseVisualStyleBackColor = true;
                taskPanel.Controls.Add(taskButton);

                verticalPosition += buttonHeight + 10;
            }

            contentPanel.Controls.Add(taskPanel);
        }

        private void TaskButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton == null || clickedButton.Tag == null) return;

            string taskName = clickedButton.Name.ToString();
            int taskId = Convert.ToInt32(clickedButton.Tag);
            LoadSpecificTask(taskName, taskId);
        }

        private void LoadSpecificTask(string taskName, int taskId)
        {
            Tasks tasks = new Tasks();
            contentPanel.Controls.Clear();

            Panel specificTaskPanel = new Panel();
            specificTaskPanel.Dock = DockStyle.Fill;

            Label taskTitleLabel = new Label();
            taskTitleLabel.Text = tasks.GetTaskName(taskId);
            taskTitleLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            taskTitleLabel.AutoSize = true;
            taskTitleLabel.Location = new Point(20, 20);
            specificTaskPanel.Controls.Add(taskTitleLabel);

            Label inputLabel1 = new Label();
            inputLabel1.Text = "������� ������ �����:";
            inputLabel1.Font = new Font("Segoe UI", 12F);
            inputLabel1.AutoSize = true;
            inputLabel1.Location = new Point(20, 70);
            specificTaskPanel.Controls.Add(inputLabel1);

            TextBox inputBox1 = new TextBox();
            inputBox1.Name = "txtInput1";
            inputBox1.Size = new Size(150, 25);
            inputBox1.Location = new Point(20, 95);
            specificTaskPanel.Controls.Add(inputBox1);

            Label inputLabel2 = new Label();
            inputLabel2.Text = "������� ������ �����:";
            inputLabel2.Font = new Font("Segoe UI", 12F);
            inputLabel2.AutoSize = true;
            inputLabel2.Location = new Point(20, 130);
            specificTaskPanel.Controls.Add(inputLabel2);

            TextBox inputBox2 = new TextBox();
            inputBox2.Name = "txtInput2";
            inputBox2.Size = new Size(150, 25);
            inputBox2.Location = new Point(20, 155);
            specificTaskPanel.Controls.Add(inputBox2);

            Label resultLabel = new Label();
            resultLabel.Text = "���������:";
            resultLabel.Font = new Font("Segoe UI", 12F);
            resultLabel.AutoSize = true;
            resultLabel.Location = new Point(20, 195);
            specificTaskPanel.Controls.Add(resultLabel);

            Label resultValueLabel = new Label();
            resultValueLabel.Name = "lblResult";
            resultValueLabel.Text = "";
            resultValueLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            resultValueLabel.AutoSize = true;
            resultValueLabel.Location = new Point(100, 195);
            specificTaskPanel.Controls.Add(resultValueLabel);

            Button executeButton = new Button();
            executeButton.Text = "���������";
            executeButton.Size = new Size(150, 30);
            executeButton.Location = new Point(20, 235);
            executeButton.Click += (s, args) =>
            {
                if (taskId == 0)
                {
                    if (int.TryParse(inputBox1.Text, out int m) && int.TryParse(inputBox2.Text, out int n))
                    {
                        int result = tasks.Reqursion(m, n);
                        resultValueLabel.Text = result.ToString();
                    }
                    else
                    {
                        MessageBox.Show("������� ����� ����� � ��� ����.", "������",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };
            specificTaskPanel.Controls.Add(executeButton);

            Button backButton = new Button();
            backButton.Text = "��������� � ������ �������";
            backButton.Size = new Size(200, 30);
            backButton.Location = new Point(20, 280);
            backButton.Click += (s, args) => LoadTaskContent();
            specificTaskPanel.Controls.Add(backButton);

            contentPanel.Controls.Add(specificTaskPanel);
        }
    }
}