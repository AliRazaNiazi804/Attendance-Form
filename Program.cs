using System;
using System.Drawing;
using System.Windows.Forms;

namespace AttendanceSystem
{
    public class AttendanceForm : Form
    {
        // ── Controls ──────────────────────────────────────────────
        private Label lblTitle;
        private Label lblSemesterText;
        private ComboBox cmbSemester;
        private Label lblColName;
        private Label lblColDate;
        private Label lblColPresent;
        private Button btnSubmit;

        // Per-student controls (built dynamically)
        private string[] studentNames = { "Sadaf", "Faiza", "Tania", "Ahmed", "Sara" };
        private Label[] nameLabels;
        private Label[] dateLabels;
        private CheckBox[] presentBoxes;

        // ── Constructor ───────────────────────────────────────────
        public AttendanceForm()
        {
            BuildUI();
        }

        // ── UI construction ───────────────────────────────────────
        private void BuildUI()
        {
            /* Form */
            Text = "Attendance for Final Examination";
            Size = new Size(720, 540);
            MinimumSize = new Size(720, 400);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9f);

            /* Title */
            lblTitle = MakeLabel("Attendance for Final Examination",
                                 20, 15, 500, 30,
                                 new Font("Segoe UI", 13f, FontStyle.Bold),
                                 Color.FromArgb(30, 100, 160));

            AddSeparator(20, 52, 660);

            /* Semester row */
            lblSemesterText = MakeLabel("Semester:", 20, 68, 80, 25);

            cmbSemester = new ComboBox
            {
                Location = new Point(105, 66),
                Size = new Size(220, 25),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbSemester.Items.AddRange(new object[] {
                "Semester 1","Semester 2","Semester 3","Semester 4",
                "Semester 5","Semester 6","Semester 7","Semester 8"
            });
            cmbSemester.SelectedIndex = 7;   // default: Semester 8

            AddSeparator(20, 105, 660);

            /* Column headers */
            lblColName = MakeLabel("Student Name", 20, 112, 200, 25,
                                      new Font("Segoe UI", 9f, FontStyle.Bold));
            lblColDate = MakeLabel("Date", 260, 112, 250, 25,
                                      new Font("Segoe UI", 9f, FontStyle.Bold));
            lblColPresent = MakeLabel("Present", 590, 112, 80, 25,
                                      new Font("Segoe UI", 9f, FontStyle.Bold));

            AddSeparator(20, 140, 660);

            /* Student rows */
            int n = studentNames.Length;
            nameLabels = new Label[n];
            dateLabels = new Label[n];
            presentBoxes = new CheckBox[n];
            string now = DateTime.Now.ToString("dd-MMM-yy h:mm:ss tt");

            for (int i = 0; i < n; i++)
            {
                int y = 148 + i * 38;

                nameLabels[i] = MakeLabel(studentNames[i], 20, y + 5, 200, 25);

                dateLabels[i] = MakeLabel(now, 260, y + 5, 280, 25,
                                          color: Color.FromArgb(80, 80, 80));

                presentBoxes[i] = new CheckBox
                {
                    Location = new Point(600, y + 5),
                    Size = new Size(20, 20),
                    Checked = true
                };

                Controls.Add(nameLabels[i]);
                Controls.Add(dateLabels[i]);
                Controls.Add(presentBoxes[i]);

                if (i < n - 1)
                    AddSeparator(20, y + 35, 660, Color.FromArgb(230, 235, 240));
            }

            /* Submit button */
            int btnY = 148 + n * 38 + 12;
            btnSubmit = new Button
            {
                Text = "Submit",
                Size = new Size(90, 32),
                Location = new Point(590, btnY),
                BackColor = Color.FromArgb(52, 120, 180),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnSubmit.FlatAppearance.BorderSize = 0;
            btnSubmit.Click += OnSubmit;

            /* Add controls to form */
            Controls.AddRange(new Control[] {
                lblTitle, lblSemesterText, cmbSemester,
                lblColName, lblColDate, lblColPresent,
                btnSubmit
            });

            Height = btnY + 80;
        }

        // ── Submit handler ────────────────────────────────────────
        private void OnSubmit(object sender, EventArgs e)
        {
            string msg = $"Attendance recorded for {cmbSemester.SelectedItem}\n\n"
                       + "Student\t\tStatus\n"
                       + new string('─', 30) + "\n";

            for (int i = 0; i < studentNames.Length; i++)
                msg += $"{studentNames[i],-16}{(presentBoxes[i].Checked ? "Present" : "Absent")}\n";

            MessageBox.Show(msg, "Attendance Summary",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ── Helpers ───────────────────────────────────────────────
        private Label MakeLabel(string text, int x, int y, int w, int h,
                                Font font = null, Color? color = null)
        {
            var lbl = new Label
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(w, h),
                Font = font ?? this.Font,
                ForeColor = color ?? Color.Black
            };
            return lbl;
        }

        private void AddSeparator(int x, int y, int w,
                                  Color? color = null)
        {
            var p = new Panel
            {
                BackColor = color ?? Color.FromArgb(200, 215, 230),
                Location = new Point(x, y),
                Size = new Size(w, 1)
            };
            Controls.Add(p);
        }
    }

    // ── Program entry point ───────────────────────────────────────
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Show login form first
            using (LoginForm loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // If login successful, show attendance form
                    Application.Run(new AttendanceForm());
                }
            }
        }
    }
}